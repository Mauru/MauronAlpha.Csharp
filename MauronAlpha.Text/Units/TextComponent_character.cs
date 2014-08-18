using MauronAlpha.Text.Utility;

using System;


namespace MauronAlpha.Text.Units {
	
	//A character in a text
	public class TextComponent_character:TextComponent, I_textComponent<TextComponent_character>,IEquatable<TextComponent_character> {

		//constructor
		public TextComponent_character() {}
		public TextComponent_character(char c){
			SetCharacter(c);
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
		public static TextComponent_character New {
			get {
				return new TextComponent_character();
			}
		}

		// the character
		private char CHAR_character;
		public char Character { get {
			return CHAR_character; 
		} }
		public TextComponent_character SetCharacter(char c){
			CHAR_character=c;
			AnalyzeCharacter();
			return this;
		}
		public TextComponent_character AnalyzeCharacter() {
			SetIsEmpty(Character==null);
			if( TextHelper.LineBreaks.ContainsValue(Character) ) {
				SetIsLineBreak(true);
			}
			if( TextHelper.WordBreaks.ContainsValue(Character) ) {
				SetIsWordBreak(true);
			}
			if( TextHelper.WhiteSpaces.ContainsValue(Character) ) {
				SetIsWhiteSpace(true);
			}
			return this;
		}

		//context
		private TextContext DATA_context;
		public TextContext Context {
			get {
				if(DATA_context==null){
					SetContext(new TextContext());
				}
				return DATA_context;
			}
		}
		public TextComponent_character SetContext(TextContext context){
			DATA_context=context;
			return this;
		}
		public bool HasContext {
			get { return DATA_context==null; }
		}

		#region Text, forming the text property

		//Get the line text
		private string STR_text;
		public string AsString {
			get {
				return STR_text;
			}
		}
		private TextComponent_character SetString (string txt) {
			STR_text=txt;
			return this;
		}
		private TextComponent_character ConstructString ( ) {
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

		#region I_TextComponent
		string I_textComponent<TextComponent_character>.AsString {
			get { return AsString; }
		}
		bool I_textComponent<TextComponent_character>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textComponent<TextComponent_character>.IsComplete {
			get { return !IsEmpty; }
		}
		bool I_textComponent<TextComponent_character>.HasWhiteSpace {
			get { return IsWhiteSpace; }
		}
		bool I_textComponent<TextComponent_character>.HasWordBreak {
			get { return IsWordBreak; }
		}
		bool I_textComponent<TextComponent_character>.HasLineBreak {
			get { return IsLineBreak; }
		}
		bool I_textComponent<TextComponent_character>.HasContext {
			get { return HasContext; }
		}
		TextComponent_character I_textComponent<TextComponent_character>.SetContext (TextContext context) {
			return SetContext(context);
		}
		TextContext I_textComponent<TextComponent_character>.Context {
			get { return Context; }
		}

		TextComponent_text I_textComponent<TextComponent_character>.Source {
			get { return Context.Source; }
		}

		TextComponent_character I_textComponent<TextComponent_character>.Instance {
			get { return Instance; }
		}
		#endregion
	
		#region IEquatable<TextComponent_character>
		bool IEquatable<TextComponent_character>.Equals (TextComponent_character other) {
			if(other.HasContext&&HasContext){
				return Character==other.Character && Context==other.Context;
			}
			else if(
				!other.HasContext&&HasContext
				|| !HasContext&&other.HasContext
			){
				return false;
			}
			return Character==other.Character;
		}
		#endregion




	}

}