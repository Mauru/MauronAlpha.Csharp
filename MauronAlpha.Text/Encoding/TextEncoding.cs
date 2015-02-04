using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Encoding {
	
	public abstract class TextEncoding:MauronCode_textComponent, 
	I_textEncoding {

		public abstract string Name { get; }

		public I_textEncoding SetTextToString(TextUnit_text unit, string text) {
			unit.Clear();
			TextUnit_paragraph paragraph = new TextUnit_paragraph(unit,false);
			TextUnit_line line = new TextUnit_line(paragraph, false);
			TextUnit_word word = new TextUnit_word(line, false);
			TextUnit_character character = new TextUnit_character(word, false);
			
			return this;
		}

		//Special Characters
		public abstract char EmptyCharacter { get; }
		public abstract char WhiteSpace { get; }
		public abstract char Tab { get; }
		public abstract char Paragraph { get; }
		public abstract char NewLine { get; }
		public abstract char ZeroWidth { get; }

		//Booleans
		public bool Equals (I_textEncoding other) {
			return Name==other.Name;
		}
		public virtual bool IsEmptyCharacter (TextUnit_character unit) {
			return unit.Character == EmptyCharacter;
		}
		public virtual bool IsWhiteSpace (TextUnit_character unit) {
			return unit.Character == WhiteSpace;
		}
		public virtual bool IsTab (TextUnit_character unit) {
			return unit.Character == Tab;
		}
		public virtual bool IsParagraph (TextUnit_character unit) {
			return unit.Character == Paragraph;
		}
		public virtual bool IsNewLine (TextUnit_character unit) {
			return unit.Character == NewLine;
		}
		public virtual bool IsZeroWidth(TextUnit_character unit) {
			return unit.Character == ZeroWidth;
		}
	
	}

}
