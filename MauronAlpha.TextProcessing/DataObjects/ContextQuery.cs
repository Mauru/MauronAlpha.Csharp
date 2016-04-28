using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;

namespace MauronAlpha.TextProcessing.DataObjects {
	
	public class ContextQuery:TextComponent {

		public readonly Text Text;
		private TextContext D_context;
		public TextContext Context {
			get {
				if (D_context == null)
					D_context = new TextContext();
				return D_context;
			}
		}

		public readonly Paragraph Paragraph;
		public readonly Word Word;
		public readonly Line Line;
		public readonly Character Character;

		//Solves strictly
		public ContextQuery(Text text, TextContext context) {
			Text = text;
			D_context = context;

			Paragraph p = null;
			if (!text.TryIndex(context.Paragraph, ref p))
				return;
			Paragraph = p;
			Line l = null;
			if (!Paragraph.TryIndex(context.Paragraph, ref l))
				return;
			Line = l;
			Word w = null;
			if (!Line.TryIndex(context.Line, ref w))
				return;
			Word = w;
			Character c = null;
			if (!Word.TryIndex(context.Character, ref c))
				return;
			Character = c;
		}
		
		//Used for instancing
		private ContextQuery(Text text, TextContext context, Paragraph p, Line l, Word w, Character c) {
			Text = text;
			D_context = context;
			Paragraph = p;
			Line = l;
			Word = w;
			Character = c;
		}
		public ContextQuery Copy {
			get {
				return new ContextQuery(Text, Context, Paragraph, Line, Word, Character);
			}
		}

		//Solves to best possible state down to word
		public static ContextQuery SolveToEdit(Text text, TextContext context) {

			//Paragraph
			Paragraph paragraph = null;
			if (!text.TryIndex(context.Paragraph, ref paragraph)) {
				if (context.Paragraph <= 0) {
					paragraph = text.FirstChild;
					context = paragraph.Start.Copy;
				}
				else {
					paragraph = text.LastChild;
					context = paragraph.Edit.Copy;
				}
			}

			//Line
			Line line = null;
			if (!paragraph.TryIndex(context.Line, ref line)) {
				if (context.Line <= 0) {
					line = paragraph.FirstChild;
					context = line.Start.Copy;
				}
				else {
					line = paragraph.LastChild;
					context = line.Edit.Copy;
				}
			}

			//Word
			Word word = null;
			if (!line.TryIndex(context.Word, ref word)) {
				if (context.Word <= 0) {
					word = line.FirstChild;
					context = word.Start.Copy;
				}
				else {
					word = line.LastChild;
					context = word.Edit.Copy;
				}
			}

			//Character
			Character character = null;
			if (!word.TryIndex(context.Word, ref character)) {
				if (context.Character <= 0) {
					character = word.FirstChild;
					context = word.Start.Copy;
				}
				else {
					character = word.LastChild;
					context = character.Edit.Copy;
				}
			}
			return new ContextQuery(text, context);
		}

		//Force the text to return a valid paragraph, if not availiable create one
		public Paragraph ForceParagraph() {
			if (Paragraph != null)
				return Paragraph;

			return Text.LastChild;
		}
		public Paragraph ForceParagraph(bool toEdit) {
			if (!toEdit)
				return ForceParagraph();
			//make sure context isnt negative
			Context.SetMin(0, 0, 0, 0);
			Paragraph p = null;

			//If the paragraph does not exist create a new one
			if (!Text.TryIndex(Context.Paragraph, ref p))
				p = Text.NewChild;
			return p;
		}

		//Force the text to return a valid Line, if not create one
		public Line ForceLine() {
			if (Line != null)
				return Line;

			if (Paragraph != null)
				return Paragraph.LastChild;

			return Text.LastLine;
		}
		public Line ForceLine(bool toEdit) {
			if (!toEdit)
				return ForceLine();

			//make sure context isnt negative
			Context.SetMin(0, 0, 0, 0);

			Paragraph p = ForceParagraph(true);

			Line l = null;

			//Line found
			if (p.TryIndex(Context.Line, ref l))
				return l;

			//Paragraph has a Pbreak
			if (p.HasParagraphBreak) {
				p = p.Next;
				D_context = p.Context;
			}

			//Paragraph is empty
			if (p.IsEmpty) {
				l = p.NewChild;
				D_context = l.Context;
				return l;
			}

			//Line is higher than linecount
			if (D_context.Line >= p.Count) {
				l = p.NewChild;
				D_context = l.Context;
				return l;
			}
			l = p.LastChild;
			D_context = l.Context;

			return l;
		}

		//Force the text to return a valid Word, if not create one
		public Word ForceWord() {
			if (Word != null)
				return Word;

			if (Line != null)
				return Line.LastChild;

			if (Paragraph != null)
				return Paragraph.LastWord;

			return Text.LastWord;
		}
		public Word ForceWord(bool toEdit) {
			if (!toEdit)
				return ForceWord();

			//make sure context isnt negative
			Context.SetMin(0, 0, 0, 0);

			//get the line
			Line l = ForceLine(true);
			Word w = null;

			//line has pbreak
			if (l.IsParagraphBreak) {
				l = l.Parent.Next.FirstChild;
				D_context = l.Context;
				return l.FirstChild;
			}

			//attempt direct word
			if (l.TryIndex(Context.Word, ref w)) {
				return w;
			}

			//line is empty
			if (l.IsEmpty) {
				w = l.NewChild;
				D_context = w.Context;
				return w;
			}

			//line has linebreak
			if (l.HasLineBreak) {
				w = l.LastChild;
				D_context = w.Context;
				return w;
			}

			w = l.NewChild;
			D_context = w.Context;
			return w;
		}

		//Force the text to return a valid Character, if not create one
		public Character ForceCharacter() {
			if (Character != null)
				return Character;

			if (Word != null)
				return Word.LastChild;

			if (Line != null)
				return Line.LastCharacter;

			if (Paragraph != null)
				return Paragraph.LastCharacter;

			return Text.LastCharacter;
		}
		public Character ForceCharacter(bool toEdit) {
			if (!toEdit)
				return ForceCharacter();

			//avoid negative context
			Context.SetMin(0, 0, 0, 0);

			Word w = ForceWord(true);

			//word is empty
			if(w.IsEmpty)
				return w.NewChild;

			//word is a Pbreak
			if (w.IsParagraphBreak) {
				Paragraph p = w.Paragraph.Next;
				w = p.FirstWord;
				return w.FirstChild;
			}

			//word is a linebreak
			if (w.IsLineBreak) {

			}


			Character c = null;
			if (!w.TryIndex(Context.Character, ref c))
				c = w.NewChild;
			return c;
		}
	}
}
