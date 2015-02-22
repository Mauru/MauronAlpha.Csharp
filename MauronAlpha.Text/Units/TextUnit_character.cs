using MauronAlpha.HandlingErrors;

using MauronAlpha.Text.Interfaces;
using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {
	
	public class TextUnit_character:TextComponent_unit {

		//Constructors
		public TextUnit_character():base(TextUnitType_character.Instance) {}
		public TextUnit_character (TextUnit_word parent, bool updateDependencies)
			: this() {
			UNIT_parent = parent;			
		}
		public TextUnit_character ( char character ):this() {
			SetChar( character );
		}

		//Statics
		public static TextUnit_character Empty {
			get {
				return new TextUnit_character();
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
		public TextUnit_character SetChar(char code) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetChar)", this, ErrorType_protected.Instance);
			TXT_char = code;
			return this;
		}

	}

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
	}

}
