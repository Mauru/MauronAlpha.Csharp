using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

namespace MauronAlpha.TextProcessing.DataObjects {

	
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

			}

			else if (c.IsLineBreak) {

				buffer.SetCharacters(c.Parent.SplitAt(c.Index + 1));
				buffer.SetWords(c.Line.SplitAt(c.Parent.Index + 1));

			}

			else if (c.IsUtility) {

				buffer.SetCharacters(c.Parent.SplitAt(c.Index + 1));

			}

			buffer = AssembleAll(buffer);

			


		}

		public static bool ByWordNext(Word w) {

			C



		}

		public static ReIndexer Result_NothingFound {
			 get {
				return new ReIndexer();
			}
		}

		//Assemble TextUnits to a number of parents
		public static TextSelector AssembleAll(TextSelector data) {

			TextSelector result = data.Copy();

			//Characters
			result = ReIndexCharactersUp(result);
			


			return result;

		}
		
		
		public static TextSelector AssembleCharacters(TextSelector buffer) {

			if (!buffer.HasCharacters)
				return buffer;

			Words result = new Words(buffer.Characters);
			buffer.Words.InsertValuesAt(0,result);

			return new TextSelector(buffer.Words);

		}
		public static TextSelector AssembleWords(TextSelector buffer) {
			if (!buffer.HasWords)
				return buffer;

			Lines result = new Lines(buffer.Words);
			buffer.Lines.InsertValuesAt(0, result);

			return new TextSelector(buffer.Lines);

		}
		public static TextSelector AssembleLines(TextSelector buffer) {
			if (!buffer.HasLines)
				return buffer;

			Paragraphs result = new Paragraphs(buffer.Paragraphs);
			buffer.Paragraphs.InsertValuesAt(0, result);

			return new TextSelector(buffer.Paragraphs);

		}


		public static TextSelector AssmbleIntoLines(Words ww, Characters cc) {
			//Empty
			if (cc.IsEmpty && ww.IsEmpty)
				return TextSelector.Empty;

			//Nothing to add
			if (cc.IsEmpty) {
				Lines l = new Lines(ww);
				return new TextSelector(l);
			}


			if (cc.HasUtility) {
				Words w = new Words(cc);
			}

			//returns lines

		}

		//Mergers
		public static TextSelector ReIndexCharactersUp(TextSelector data) {
			if (data.IsEmpty)
				return data;


			Paragraphs p = new Paragraphs(data.Characters);

			TextUnitType tu = data.HighestMultipleUnitType;

			//There is no highest unit type - everything is contained in one character
			if (tu.Equals(TextUnitTypes.None))
				return data;

			//The highest unit type is a text - we assemble all lower unit types into the first paragraph
			if (tu.Equals(TextUnitTypes.Paragraph)) {

				TextSelector step = AssembleIntoLines(data.Words, data.Characters);

			}
		}

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

		public static TextSelector PrependLines(Lines target, Lines candidate) {
			if (candidate.IsEmpty)
				return new TextSelector(target);

			
		}

		

	}
}
