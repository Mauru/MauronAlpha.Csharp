using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

namespace MauronAlpha.TextProcessing.DataObjects {
	
	//Helps with creating/maintaining indexes
	public class ReIndexer:TextComponent {

		public static TextRange ReIndexWordAtOffset(Word w, int offset) {

			if(offset<0)
				offset = 0;

			Character c = null;
			if(!w.TryIndex(offset, ref c))
				return new TextRange(w.Text,w.End,w.End);

			TextSelector buffer = new TextSelector();

			if (c.IsParagraphBreak) {

				buffer.SetCharacters(c.Parent.SplitAt(c.Index + 1));
				buffer.SetWords(c.Line.SplitAt(c.Parent.Index + 1));
				buffer.SetLines(c.Paragraph.SplitAt(c.Line.Index + 1));

				if (buffer.IsEmpty)
					return new TextRange(c.Text, c.Parent.End, c.Parent.End);

				Lines lines = ReIndexer.AssembleIntoLines(buffer).Lines;
				if (lines.HasParagraphBreak) {
					Paragraphs pp = new Paragraphs(lines);
					c.Text.Insert(pp, c.Paragraph.Index + 1);
					return new TextRange(c.Text, c.Parent.End, pp.LastElement.LastWord.End);
				}
				c.Paragraph.Next.Insert(lines, 0);
				return new TextRange(c.Text, c.Paragraph.End, lines.LastElement.End);
			}
			else if (c.IsLineBreak) {

				buffer.SetCharacters(c.Parent.SplitAt(c.Index + 1));
				buffer.SetWords(c.Line.SplitAt(c.Parent.Index + 1));

				if (buffer.IsEmpty)
					return new TextRange(c.Text, c.Context, c.Context);

				Lines ll = c.Line.LookRight;
				if (ll.IsEmpty)
					c.Paragraph.TryAdd(new Lines(buffer.Words));
				return new TextRange(c.Text)


			}



			else if (c.IsUtility) {

				buffer.SetCharacters(c.Parent.SplitAt(c.Index + 1));

			}



			


		}


		public static ReIndexer Result_NothingFound {
			 get {
				return new ReIndexer();
			}
		}

		public static TextSelector AssembleCharactersIntoWords(TextSelector buffer) {

			if (!buffer.HasCharacters)
				return buffer;

			Words result = new Words(buffer.Characters);
			buffer.Words.InsertValuesAt(0,result);

			return new TextSelector(buffer.Words);

		}
		public static TextSelector AssembleWordsIntoLines(TextSelector buffer) {
			if (!buffer.HasWords)
				return buffer;

			Lines result = new Lines(buffer.Words);
			buffer.Lines.InsertValuesAt(0, result);

			return new TextSelector(buffer.Lines);

		}
		public static TextSelector AssembleLinesIntoParagraphs(TextSelector buffer) {
			if (!buffer.HasLines)
				return buffer;

			Paragraphs result = new Paragraphs(buffer.Paragraphs);
			buffer.Paragraphs.InsertValuesAt(0, result);

			return new TextSelector(buffer.Paragraphs);

		}


		public static TextSelector AssembleIntoWords(Characters cc) {
			//Assemble characters into words
			Words wwc = new Words();

			//Characters are words
			if (cc.HasUtility)
				wwc = new Words(cc);
			//Characters is a single word
			else
				wwc = new Words(new Word(cc));

			return new TextSelector(wwc);
		}

		public static TextSelector AssembleIntoLines(Words ww) {
			if(ww.IsEmpty)
				return TextSelector.Empty;

			Lines lines = new Lines(ww);
			return new TextSelector(lines);
		}
		public static TextSelector AssembleIntoLines(TextSelector buffer) {

			Characters cc = buffer.Characters;

			TextSelector assembler = new TextSelector();
			assembler.SetWords(ReIndexer.AssembleIntoWords(cc).Words);
			assembler.AddWords(buffer.Words);

			assembler.InsertLines(buffer.Lines,0);

			return new TextSelector(assembler.Lines);
		}

		//Mergers
		//return a contextQuery with the start of the insert operation on the next Line and the end of the insert operation
		public static TextRange MergeIntoNextLine(Line origin, Words words) {

			if (words.IsEmpty)
				return new TextRange(origin.Text, origin.End, origin.End);

			//get next line
			Line next = null;
			if (!origin.TryNext(ref next)) { 
				Paragraph p = origin.Parent;

				//Test if origin ends the paragraph
				if (origin.IsParagraphBreak)
					next = p.Next.FirstChild;
				else
					next = p.NewChild;
			}

			next.Insert(words, 0);

			Word w = words.LastElement;
			return new TextRange(origin.Text, origin.End, w.End);

		}

		public static TextRange PrependIntoLines(Lines target, Words words) {
			if (target.IsEmpty)
				return TextRange.None;

			Line l = target.FirstElement;

			if (words.IsEmpty)
				return new TextRange(l.Text, l.Start, l.Start);

			Lines ll = new Lines(words);
			if (ll.Count > 1) {
				Lines prependSimple = ll.Range(1); 
				l.Parent.Insert(prependSimple, l.Index);
				ReIndexer.MergeIntoNextLine(l,ll.FirstElement.Words);
				return new TextRange(l.Text, l.End, prependSimple.LastElement.End);
			}

			ReIndexer.MergeIntoNextLine(l, ll.FirstElement.Words);
			return new TextRange(l.Text, l.End, ll.LastElement.End);
		}

		public static TextRange MergeByAppend(Paragraph origin, Paragraph data) {

			if (origin.IsEmpty && data.IsEmpty)
				return TextRange.None;

			if (origin.HasParagraphBreak)
				return new TextRange(origin.Parent, origin.End, data.End);

			Lines ll;
			Line l;

			if (origin.IsEmpty) {
				if (data.Parent.Equals(origin.Parent))
					origin.Parent.Remove(data.Index);

				ll = data.Lines;
				l = ll.LastElement;

				origin.TryAdd(ll);
				return new TextRange(origin.Parent, origin.End, l.End);
			}

			if (data.IsEmpty)
				return new TextRange(origin.Parent, origin.End, origin.End);

			if (data.Parent.Equals(origin.Parent))
				origin.Parent.Remove(data.Index);

			ll = data.Lines;
			l = ll.LastElement;

			origin.TryAdd(ll);
			return new TextRange(origin.Parent, origin.End, l.End);
		}
		public static TextRange MergeNext(Line origin) {
			Line l = null;
			if (!origin.TryNext(ref l)) {
				Paragraph p = null;
				if (origin.IsParagraphBreak)
					return new TextRange(origin.Text, origin.End, origin.End);
				if(!origin.Parent.TryNext(ref p))
					return new TextRange(origin.Text, origin.End, origin.End);
				Lines ll = p.LinesUntilParagraphBreak;


			}
		}
	}
}
