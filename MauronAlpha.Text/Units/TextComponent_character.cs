using MauronAlpha.Text.Utility;

namespace MauronAlpha.Text.Units {
	
	//A character in a text
	public class TextComponent_character:TextComponent, I_textComponent<TextComponent_character> {

		//constructor
		public TextComponent_character() {}
		public TextComponent_character(char c){
			SetCharacter(c);
			if(TextHelper.LineBreaks.ContainsValue(c)){
				SetIsLineBreak(true);
			}
			if(TextHelper.WordBreaks.ContainsValue(c)){
				SetIsWordBreak(true);
			}
			if(TextHelper.WhiteSpaces.ContainsValue(c)){
				SetIsWhiteSpace(true);
			}
		}

		//Instance
		public TextComponent_character Instance {
			get {
				TextComponent_character c= new TextComponent_character();
				c.SetCharacter(Character);
				c.SetIsLineBreak(IsLineBreak);
				c.SetIsWhiteSpace(IsWhiteSpace);
				c.SetIsWordBreak(IsWordBreak);
				c.SetIsEmpty(IsEmpty);
				return c;
			}
		}

		// the character
		private char CHAR_character;
		public char Character { get {
			return CHAR_character; 
		} }
		public TextComponent_character SetCharacter(char c){
			CHAR_character=c;
			SetIsEmpty(false);
			return this;
		}

		#region Text, forming the text property

		//Get the line text
		private string STR_text;
		public string Text {
			get {
				return STR_text;
			}
		}
		private TextComponent_character SetText (string txt) {
			STR_text=txt;
			return this;
		}
		private TextComponent_character ConstructText ( ) {
			string txt=""+Character;
			STR_text=txt;
			return this;
		}

		#endregion

		#region State Flags (bool)

		//is the character empty?
		private bool B_isEmpty=true;
		public bool IsEmpty {
			get { return B_isEmpty; }
		}
		public TextComponent_character SetIsEmpty(bool status){
			B_isEmpty=status;
			return this;
		}
	
		//does the character terminate a word?
		public bool EndsWord {
			get {
				return IsLineBreak||IsWhiteSpace;
			}
		}

		//does the character End a line?
		public bool EndsLine {
			get {
				return IsLineBreak;
			}
		}

		private bool B_isLineBreak=false;
		public bool IsLineBreak { get {	return B_isLineBreak; } }
		public TextComponent_character SetIsLineBreak(bool status){
			B_isLineBreak=status;
			return this;
		}

		private bool B_isWordBreak=false;
		public bool IsWordBreak { get { return B_isWordBreak; } }
		public TextComponent_character SetIsWordBreak (bool status) {
			B_isWordBreak=status;
			return this;
		}

		private bool B_isWhiteSpace=false;
		public bool IsWhiteSpace { get { return B_isWhiteSpace; } }
		public TextComponent_character SetIsWhiteSpace (bool status) {
			B_isWhiteSpace=status;
			return this;
		}
	
		#endregion

	}

}