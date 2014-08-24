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

		public TextComponent_word AddCharacter(TextComponent_character c){
			Characters.AddValue (c);
			return this;
		}

		#region The TextComponent this element belongs to
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
		#endregion

		#region The context of this TextComponent
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
		public TextComponent_word OffsetContext (TextContext context) {
			Context.Add(context);
			foreach( TextComponent_character c in Characters ) {
				c.OffsetContext(context);
			}
			return this;
		}
		#endregion

		#region the content
		public TextComponent_character FirstCharacter {
			get {
				if( Characters.Count<1 ) {
					Error("Character Index out of bounds #{F:FirstCharacter}", this, ErrorType_index.Instance);
				}
				return Characters.FirstElement;
			}
		}
		public TextComponent_character CharacterByContext(TextContext context) {
			if(!Characters.ContainsKey(context.CharacterOffset)){
				Error("Character Index out of bounds #{"+context.CharacterOffset+"}", this, ErrorType_index.Instance);
			}
			return Characters.Value(context.CharacterOffset);

		}
		public TextComponent_character LastCharacter {
			get {
				if(Characters.Count<1){
					Error("Character Index out of bounds #{F:LastCharacter}",this,ErrorType_index.Instance);
				}
				return Characters.LastElement;
			}
		}
		#endregion

		public int CharacterCount {
			get { 
				return Characters.Count;
			}
		}

		public bool EndsLine {
			get {
				if( Characters.Count<1 ) {
					return false;
				}
				return Characters.LastElement.EndsLine;
			}
		}		
		public bool IsComplete {
			get {
				return (Characters.Count>0&&LastCharacter.EndsWord);
			}
		}

		//Output as string
		public string AsString {
			get {
				string result="";
				foreach( TextComponent_character c in Characters ) {
					result+=c.AsString;
				}
				return result;
			}
		}


	}
}