using MauronAlpha.HandlingData;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Encoding {
	
	public abstract class TextEncoding:MauronCode_textComponent, 
	I_textEncoding {

		public abstract string Name { get; }

		public TextUnit_text StringAsTextUnit (string text) {
			TextUnit_text unit = new TextUnit_text();
			unit.SetContext(new TextContext(-1,-1,-1,-1));

			TextUnit_paragraph paragraph = new TextUnit_paragraph(unit, true);
			TextUnit_line line = new TextUnit_line(paragraph, true);
			TextUnit_word word = new TextUnit_word(line,true);
			TextUnit_character character;

			foreach(char c in text){
				character = new TextUnit_character( word, false );
				character.SetChar(c, false);

				word.InsertChildAtIndex(word.ChildCount,character, false, true);
				if(EndsParagraph(character)) {
					paragraph = new TextUnit_paragraph(unit,true);
				}
				if( EndsLine(character) ) {
					line = new TextUnit_line(paragraph,true);
				}
				if(EndsWord(character)) {
					word = new TextUnit_word(line,true);
				}
			}
			return unit;
		}

		public I_textEncoding StringToTextUnit( string text, I_textUnit unit, bool updateParent, bool updateChild ) {
			TextUnit_text stuff = StringAsTextUnit(text);

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
			return Name == other.Name;
		}
		public virtual bool EndsParagraph(TextUnit_character unit) {
			return IsParagraph(unit);
		}
		public virtual bool EndsParagraph(TextUnit_word unit) {
			return EndsParagraph( unit.LastCharacter );
		}
		public virtual bool EndsParagraph(TextUnit_line unit) {
			return EndsParagraph( unit.LastCharacter );
		}
		public virtual bool EndsWord(TextUnit_character unit) {
			return IsWhiteSpace(unit) || IsTab(unit);
		}
		public virtual bool EndsLine(TextUnit_character unit) {
			return IsNewLine(unit) || IsParagraph(unit);
		}
		public virtual bool EndsLine( TextUnit_word unit ) {
			foreach( TextUnit_character ch in unit.Children )
				if( EndsLine( ch ) )
					return true;
			return false;
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
		
		protected MauronCode_dataList<I_textUnitType> TypeOrder = new MauronCode_dataList<I_textUnitType>() { 
			TextUnitType_character.Instance,
			TextUnitType_word.Instance,
			TextUnitType_line.Instance,
			TextUnitType_paragraph.Instance,
			TextUnitType_text.Instance
		};

		public virtual bool UnitEndsOther(I_textUnit candidate, I_textUnit other) {
			
			if( candidate.UnitType.Equals(TextUnitType_character.Instance) ) {
				TextUnit_character unit = (TextUnit_character) candidate;
				return UnitEndsOther( unit, other );
			}

			if( candidate.UnitType.Equals( TextUnitType_word.Instance) ) {
				TextUnit_word unit = ( TextUnit_word ) candidate;
				return UnitEndsOther( unit, other );	
			}

			if( candidate.UnitType.Equals( TextUnitType_line.Instance ) ) {
				TextUnit_line unit = (TextUnit_line) candidate;
				return UnitEndsOther( unit, other );
			}

			return TypeOrder.IndexOf(candidate.UnitType) >= TypeOrder.IndexOf(other.UnitType);

		}
		public virtual bool UnitEndsOther (TextUnit_character unit, I_textUnit other) {
			I_textUnitType otherType=other.UnitType;

			if( otherType.Equals(TextUnitType_character.Instance) )
				return true;

			if( otherType.Equals(TextUnitType_word.Instance)
			&&(EndsLine(unit)||EndsParagraph(unit)||EndsWord(unit)) )
				return true;

			if( otherType.Equals(TextUnitType_line.Instance)
			&&(EndsParagraph(unit)||EndsWord(unit)) )
				return true;

			return false;
		
		}
		public virtual bool UnitEndsOther( TextUnit_word unit, I_textUnit other ) {
			I_textUnitType otherType=other.UnitType;

			if( otherType.Equals( TextUnitType_character.Instance ) 
			|| otherType.Equals( TextUnitType_word.Instance ) )
				return true;

			if( otherType.Equals( TextUnitType_line.Instance) )
				return EndsLine( unit ) || EndsParagraph( unit );
			
			if( otherType.Equals( TextUnitType_paragraph.Instance ) )
				return EndsParagraph( unit );

			return false;

		}
		public virtual bool UnitEndsOther(TextUnit_line unit, I_textUnit other) {
			I_textUnitType otherType = other.UnitType;

			if( otherType.Equals(TextUnitType_character.Instance)
			||otherType.Equals(TextUnitType_word.Instance)
			||otherType.Equals(TextUnitType_line.Instance))
				return true;

			if( otherType.Equals(TextUnitType_paragraph.Instance) )
				return EndsParagraph(unit);

			return false;
		}

	}

}
