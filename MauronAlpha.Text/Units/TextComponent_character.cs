using MauronAlpha.Text.Utility;

using System;


namespace MauronAlpha.Text.Units {
	
	//A character in a text
	public class TextComponent_character:TextComponent {

		//constructor
		public TextComponent_character(TextComponent_word parent, TextContext context, char c){
			SetChar(c);
			SetParent (parent);
			SetContext (context);
		}

		private TextContext TXT_context;
		public TextContext Context {
			get { 
				return TXT_context;
			}
		}
		public TextComponent_character SetContext(TextContext context){
			TXT_context=context;
			return this;
		}

		private TextComponent_word TXT_parent;
		public TextComponent_word Parent {
			get { 
				return TXT_parent;
			}
		}
		public TextComponent_character SetParent(TextComponent_word context){
			TXT_parent=context;
			return this;
		}

		private char CHAR_txt;
		public char Char {
			get { 
				return CHAR_txt;
			}
		}
		public TextComponent_character SetChar(char c){
			CHAR_txt=c;
			SetIsEmpty(false);
			return this;
		}
	
		public bool EndsLine {
			get {
				if(IsEmpty){
					return false;
				}
				return TextHelper.IsLineBreak(Char);
			}
		}
		public bool EndsWord {
			get {
				if(IsEmpty){
					return false;
				}
				return TextHelper.IsWordEnd(Char);
			}
		}

		private bool B_isEmpty=true;
		public bool IsEmpty {
			get {
				return B_isEmpty;
			}
		}
		private TextComponent_character SetIsEmpty(bool status){
			B_isEmpty=status;
			return this;
		}
	}

}