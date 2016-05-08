using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

namespace MauronAlpha.TextProcessing.DataObjects {
	
	//Helps with creating/maintaining indexes
	public class ReIndexer:TextComponent {

		public static TextRange ReIndexWordAtOffset(Word w, int offset) {
			Character c = null;
			if (!w.TryIndex(offset, ref c))
				c = w.LastChild;
			TextOperation result = TextOperation.ReIndexAhead(c);

			return TextOperation.CompleteOperations(result);
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
		public static TextSelector AssembleCharactersIntoWords(Characters cc) {

			Words result = new Words(cc);
			return new TextSelector(result);

		}
		public static TextSelector AssembleWordsIntoLines(TextSelector buffer) {
			if (!buffer.HasWords)
				return buffer;

			Lines result = new Lines(buffer.Words);
			buffer.Lines.InsertValuesAt(0, result);

			return new TextSelector(buffer.Lines);

		}
		public static TextSelector AssembleWordsIntoLines(Words ww) {
			Lines result = new Lines(ww);
			return new TextSelector(result);
		}
		public static TextSelector AssembleLinesIntoParagraphs(TextSelector buffer) {
			if (!buffer.HasLines)
				return buffer;

			Paragraphs result = new Paragraphs(buffer.Paragraphs);
			buffer.Paragraphs.InsertValuesAt(0, result);

			return new TextSelector(buffer.Paragraphs);

		}
		public static TextSelector AssembleLinesIntoParagraphs(Lines ll) {

			Paragraphs result = new Paragraphs(ll);
			return new TextSelector(result);

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

		public static TextSelector AssembleIntoParagraphs(TextSelector buffer) {

			TextSelector step = ReIndexer.AssembleCharactersIntoWords(buffer.Characters);
			step.AddWords(step.Words);
			step.AddWords(buffer.Words);
			step.SetLines(ReIndexer.AssembleWordsIntoLines(step.Words).Lines);
			step.AddLines(buffer.Lines);

			step.SetParagraphs(ReIndexer.AssembleLinesIntoParagraphs(step.Lines).Paragraphs);
			step.AddParagraphs(buffer.Paragraphs);
			return new TextSelector(step.Paragraphs);

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

		public static TextOperation MergeNext(Line o) {
			//Find the next global line
			Line n = null;
			if (!o.TryAhead(ref n))
				return new TextOperation(o.Parent, TextOperation.MergeAheadParagraph);

			Words ww;
			if (o.IsEmpty || !o.HasLineOrParagraphBreak) {
				ww = n.WordsUntilLineBreak;
				o.TryAdd(ww);
				if (n.IsEmpty)
					n.Parent.Remove(n.Index);
				return new TextOperation(o.Parent, TextOperation.MergeAheadLine);
			}

			if (n.IsEmpty) {
				n.Parent.Remove(n.Index);
				return new TextOperation(o, TextOperation.MergeAheadLine);
			}

			if (o.IsParagraphBreak)
				return new TextOperation(o, TextOperation.SplitAheadParagraph);

			return new TextOperation(n, TextOperation.MergeAheadLine);			
		}
		public static TextOperation MergeNext(Word o) {
			//Find next global word
			Word n = null;
			if (!o.TryAhead(ref n))
				return new TextOperation(o.Parent, TextOperation.MergeAheadLine);

			if (o.IsEmpty || !o.IsUtility) {
				Characters cc = n.CharactersUntilUtility;
				o.TryAdd(cc);
				if (n.IsEmpty)
					n.Parent.Remove(n.Index);
				return new TextOperation(o, TextOperation.MergeAheadWord);
			}

			if (n.IsEmpty) {
				n.Parent.Remove(n.Index);
				return new TextOperation(o, TextOperation.MergeAheadWord);
			}

			if (o.IsParagraphBreak)
				return new TextOperation(o, TextOperation.SplitAheadParagraph);

			if (o.IsLineBreak)
				return new TextOperation(o, TextOperation.SplitAheadLine);

			return new TextOperation(n, TextOperation.MergeAheadWord);
		}
		public static TextOperation MergeNext(Paragraph o) {
			//Find the next global paragraph
			Paragraph  n = null;
			if (!o.TryNext(ref n))
				return new TextOperation(o.Parent, TextOperation.Nothing);

			Lines ll;
			if (o.IsEmpty || !o.HasParagraphBreak) {
				ll = n.LinesUntilParagraphBreak;
				o.TryAdd(ll);
				if (n.IsEmpty)
					n.Parent.Remove(n.Index);
				return new TextOperation(o.Parent, TextOperation.MergeAheadParagraph);
			}

			if (n.IsEmpty) {
				n.Parent.Remove(n.Index);
				return new TextOperation(o, TextOperation.MergeAheadParagraph);
			}

			if(o.HasParagraphBreak)
				return new TextOperation(o, TextOperation.SplitAheadParagraph);

			return new TextOperation(n, TextOperation.MergeAheadParagraph);
		}

	}
}
