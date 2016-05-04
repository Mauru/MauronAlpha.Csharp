namespace MauronAlpha.TextProcessing.DataObjects {
	public class TextContext:TextComponent {

		public int Paragraph = 0;
		public int Line = 0;
		public int Word = 0;
		public int Character = 0;

		public TextContext SetParagraph(int index) {
			Paragraph = index;
			return this;
		}
		public TextContext SetLine(int index) {
			Line = index;
			return this;
		}
		public TextContext SetWord(int index) {
			Word = index;
			return this;
		}
		public TextContext SetCharacter(int index) {
			Character = index;
			return this;
		}

		public TextContext() : base() { }
		public TextContext(int paragraph, int line, int word, int character) : this() {
			Paragraph = paragraph;
			Line = line;
			Word = word;
			Character = character;
		}

		public TextContext Copy {
			get {
				return new TextContext(Paragraph, Line, Word, Character);
			}
		}

		public TextContext Add(int paragraph, int line, int word, int character) {
			Paragraph += paragraph;
			Line += line;
			Word += word;
			Character += character;
			return this;
		}
		public TextContext Set(int paragraph, int line, int word, int character) {
			Paragraph = paragraph;
			Line = line;
			Word = word;
			Character = character;
			return this;
		}
		public TextContext Set(TextContext ctx) {
			Paragraph = ctx.Paragraph;
			Line = ctx.Line;
			Word = ctx.Word;
			Character = ctx.Character;
			return this;
		}
		public TextContext SetMin(int paragraph, int line, int word, int character) {
			if (Paragraph < paragraph)
				Paragraph = paragraph;
			if (Line < line)
				Line = line;
			if (Word < word)
				Word = word;
			if (Character < character)
				Character = character;
			return this;
		}
		public TextContext SetMax(int paragraph, int line, int word, int character) {
			if (Paragraph > paragraph)
				Paragraph = paragraph;
			if (Line > line)
				Line = line;
			if (Word > word)
				Word = word;
			if (Character > character)
				Character = character;
			return this;
		}

		public string AsString {
			get {
				return "{" + Paragraph + ":" + Line + ":" + Word + ":" + Character + "}";
			}
		}
	
	}
}
