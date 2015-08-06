﻿using MauronAlpha.HandlingData;
using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;
using MauronAlpha.Text.Collections;

namespace MauronAlpha.Text.Encoding {
	
	public abstract class TextEncoding:MauronCode_textComponent, 
	I_textEncoding {

		public abstract string Name { get; }

		public TextUnit_text StringAsUnit (string text) {
			TextUnit_text unit = new TextUnit_text();
			return StringToUnit(text, unit);
		}
		public TextUnit_text StringToUnit(string text, TextUnit_text unit) {
			unit.Clear();
			unit.SetContext(new TextContext(-1,-1,-1,-1));

			TextUnit_paragraph paragraph = new TextUnit_paragraph(unit);
			TextUnit_line line = new TextUnit_line(paragraph);
			TextUnit_word word = new TextUnit_word(line);
			TextUnit_character character;

			foreach(char c in text){
				character = new TextUnit_character(word);
				character.SetChar(c, false);

				//word.InsertChildAtIndex(word.ChildCount,character, true);
				if(EndsParagraph(character))
					paragraph = new TextUnit_paragraph(unit);
				if( EndsLine(character) )
					line = new TextUnit_line(paragraph);
				if(EndsWord(character))
					word = new TextUnit_word(line);
			}
			return unit;		
		}

		public virtual MauronCode_dataList<TextUnit_character> StringToCharacters(string str) {
			MauronCode_dataList<TextUnit_character> result = new MauronCode_dataList<TextUnit_character>();
			foreach (char c in str)
				result.Add(new TextUnit_character(c));
			return result;
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
		public virtual bool IsRealCharacter(TextUnit_character unit) {
			if (IsZeroWidth(unit))
				return false;
			if (IsNewLine(unit))
				return false;
			if (IsParagraph(unit))
				return false;
			if (IsTab(unit))
				return true;
			if (IsWhiteSpace(unit))
				return true;
			if (IsEmptyCharacter(unit))
				return false;
			return true;
		}

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

			return TextUnitType.Order.IndexOf(candidate.UnitType) >= TextUnitType.Order.IndexOf(other.UnitType);

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
