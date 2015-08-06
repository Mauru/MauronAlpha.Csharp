using MauronAlpha.HandlingErrors;

using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {
	
	//A character in a word
	public class TextUnit_character:TextComponent_unit {

		//Constructors
		public TextUnit_character():base(TextUnitType_character.Instance) {}
		public TextUnit_character (TextUnit_word parent) : this() {
			parent.InsertChildAtIndex(parent.ChildCount, this, false);
		}
		public TextUnit_character ( char character ):this() {
			SetChar( character , false );
		}

		//Statics
		public static TextUnit_character Empty {
			get {
				return new TextUnit_character();
			}
		}

		public TextUnit_character Copy {
			get {
				TextUnit_character copy = new TextUnit_character();
				copy.SetChar(Character,false);
				return copy;
			}
		}

		//Boolean
		public bool IsAtEndOfWord {
			get {
				if (!IsChild)
					return true;
				if (Index + 1 >= Parent.ChildCount)
					return true;
				return false;
			}
		}
		public bool IsRealCharacter {
			get {
				return Encoding.IsRealCharacter(this);
			}
		}

		//As String
		public override string AsString {
			get {
				return ""+Character;
			}
		}

		//Count
		public override TextContext CountAsContext {
			get { 
				return new TextContext();
			}
		}
		public override int Index { 
			get {
				return Context.Character;
			}
		}

		//The character
		private char TXT_char;
		public char Character {
			get {
				return TXT_char;
			}
		}

		//Booleans
		public override bool Equals (I_textUnit other) {
			if(!UnitType.Equals(other.UnitType))
				return false;
			if(IsEmpty!=other.IsEmpty)
				return false;
			return (Character == other.LastCharacter.Character);
		}
		public override bool IsEmpty {
			get {
				return Encoding.IsEmptyCharacter(this);
			}
		}

		//Methods
		public TextUnit_character SetChar(char code, bool updateParent) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetChar)", this, ErrorType_protected.Instance);

			TXT_char = code;

			if( updateParent 
				&& IsChild 
				&& Encoding.UnitEndsOther(this,Parent) )
			Parent.HandleEnds();

			return this;
		}
		
	}

	//Description of the unitType
	public class TextUnitType_character : TextUnitType {
		
		public override string Name {
			get {
				return "character";
			}
		}

		public static TextUnitType_character Instance {
			get {
				return new TextUnitType_character();
			}
		}

		public override bool CanHaveChildren {
			get {
				return false;
			}
		}

		public override I_textUnit New {
			get {
				return new TextUnit_character();
			}
		}
	
		public override I_textUnitType ParentType {
			get {
				return TextUnitType_word.Instance;
			}
		}
		public override I_textUnitType ChildType {
			get {
				return Instance;
			}
		}
	
	}

}
