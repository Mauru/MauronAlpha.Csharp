using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using System.Collections.Generic;
using System;

namespace MauronAlpha.Text.Units {

	//A word
	public class TextComponent_word:TextComponent {
		
		//constructor
		public TextComponent_word(TextComponent_line parent, TextContext context) {}
	
		private MauronCode_dataList<TextComponent_character> DATA_characters;
		public MauronCode_dataList<TextComponent_character> Characters {
			get {
				if(DATA_characters==null){
					DATA_characters = new MauronCode_dataList<TextComponent_character>();
				}
				return DATA_characters;
			}
		}
		private TextComponent_word SetCharacters(MauronCode_dataList<TextComponent_character> characters){
			DATA_characters=characters;
			return this;
		}

		private TextComponent_line TXT_parent;
		public TextComponent_line Parent {
			get {
				if(TXT_parent==null){
					NullError("Parent can not be null!",this,typeof(TextComponent_line));
				}
				return TXT_parent;
			}
		}
		private TextComponent_word SetParent(TextComponent_line line){
			TXT_parent=line;
			return this;
		}
	
		private TextContext TXT_context;
		public TextContext Context {
			get {
				if(TXT_context==null){
					NullError("Context can not be null",this,typeof(TextContext));
				}
				return TXT_context;
			}
		}
		private TextComponent_word SetContext(TextContext context){
			TXT_context=context;
			return this;
		}
	
		public bool EndsLine {
			get {
				if(Characters.Count<1){
					return false;
				}
				return Characters.LastElement.EndsLine;
			}
		}	
	}
}