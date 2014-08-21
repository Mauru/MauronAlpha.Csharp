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
			TXT_context=context;
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
			return this;
		}
	
	}

}