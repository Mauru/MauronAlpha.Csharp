using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Utility;

using System.Collections.Generic;
using System;

namespace MauronAlpha.Text.Units {

	//A word
	public class TextComponent_word:TextComponent, I_textComponent<TextComponent_word>,IEquatable<string> {
		
		//constructor
		public TextComponent_word(TextComponent_line parent, TextContext context) {}
	
		#region Instance (creates clone)
		public TextComponent_word Instance {
			get {
				TextComponent_word result = new TextComponent_word(Parent,Context.Instance);
				foreach(TextComponent_character c in Characters){
					result.AddCharacter(c.Instance);
				}
				return this;
			}
		}
		#endregion

		#region The Characters in this Word
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
			#region ReadOnly Check
			if( IsReadOnly ) {
				Error("Is protected!,(SetCharacters)", this, ErrorType_protected.Instance);
			}
			#endregion
			DATA_characters=characters;
			return this;
		}
		#endregion

		#region ReadOnly
		private bool B_isReadOnly=false;
		public bool IsReadOnly {
			get { return B_isReadOnly; }
		}
		public TextComponent_word SetReadOnly (bool status) {
			B_isReadOnly=status;
			return this;
		}
		#endregion

		//Add a character, remove zerowidth (TextHelper.Empty)
		public TextComponent_word AddCharacter(TextComponent_character c){
			#region ReadOnly Check
			if( IsReadOnly ) {
				Error("Is protected!,(AddCharacter)", this, ErrorType_protected.Instance);
			}
			#endregion
			if(CharacterCount>0&&LastCharacter.IsEmpty){
				RemoveLastCharacter();
			}
			Characters.AddValue (c);
			return this;
		}

		//Remove the last character
		public TextComponent_word RemoveLastCharacter() {
			#region ReadOnly Check
			if( IsReadOnly ) {
				Error("Is protected!,(RemoveLastCharacter)", this, ErrorType_protected.Instance);
			}
			#endregion
			#region Error Check
			if(CharacterCount==0){
				Error("Characters is empty (RemoveLastCharacter)",this, ErrorType_index.Instance);
			}
			#endregion
			Characters.RemoveLastElement();
			return this;
		}

		#region The TextComponent_line this element belongs to
		private TextComponent_line TXT_parent;
		public TextComponent_line Parent {
			get {
				#region Error Check
				if(TXT_parent==null){
					NullError("Parent can not be null!,(Parent)",this,typeof(TextComponent_line));
				}
				#endregion
				return TXT_parent;
			}
		}
		private TextComponent_word SetParent(TextComponent_line line){
			TXT_parent=line;
			return this;
		}
		#endregion
		#region The TextComponent_text this element belongs to
		public TextComponent_text Source {
			get {
				return Parent.Source;
			}
		}
		#endregion

		#region The Context of this TextComponent
		private TextContext TXT_context;
		public TextContext Context {
			get {
				#region Error Check
				if(TXT_context==null){
					NullError("Context can not be null!,(Context)",this,typeof(TextContext));
				}
				#endregion
				return TXT_context;
			}
		}
		public bool HasContext {
			get {
				return TXT_context==null;
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

		#region The Content
		public TextComponent_character FirstCharacter {
			get {
				#region Error Check
				if( CharacterCount<1 ) {
					Error("Character Index out of bounds!,(FirstCharacter)", this, ErrorType_index.Instance);
				}
				#endregion
				return Characters.FirstElement;
			}
		}
		public TextComponent_character CharacterByContext(TextContext context) {
			#region Error Check
			if(!Characters.ContainsKey(context.CharacterOffset)){
				Error("Character Index out of bounds!,{"+context.CharacterOffset+"},(CharacterByContext)", this, ErrorType_index.Instance);
			}
			#endregion
			return Characters.Value(context.CharacterOffset);
		}
		public TextComponent_character LastCharacter {
			get {
				#region Error Check
				if(CharacterCount<1){
					Error("Character Index out of bounds!,(LastCharacter)",this,ErrorType_index.Instance);
				}
				#endregion
				return Characters.LastElement;
			}
		}
		#endregion

		public int CharacterCount {
			get { 
				return Characters.Count;
			}
		}

		#region Boolean States
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
		//this is important! empty can mean that charactercount is 0 or that the lastcharacter is TextHelper.Empty (zerowidth)!
		public bool IsEmpty {
			get {
				if(CharacterCount<1) return true;
				if(LastCharacter.Equals(TextHelper.Empty)) return true;
				return false;
			}
		}
		public bool HasLineBreak {
			get {
				foreach(TextComponent_character c in Characters){
					if(c.IsLineBreak){ return true; }
				}
				return false;
			}
		}
		public bool HasWhiteSpace {
			get {
				foreach( TextComponent_character c in Characters ) {
					if( c.IsWhiteSpace ) { return true; }
				}
				return false;	
			}
		}
		public bool HasWordBreak {
			get {
				foreach(TextComponent_character c in Characters){
					if(c.IsWordBreak){ return true; }
				}
				return false;	
			}
		}
		#endregion

		//Output as string, ignore TextHelper.Empty
		public string AsString {
			get {
				string result="";
				foreach( TextComponent_character c in Characters ) {
					if(!c.IsEmpty){
						result+=c.AsString;
					}
				}
				return result;
			}
		}

		#region I_textComponent
		string I_textComponent<TextComponent_word>.AsString {
			get { return AsString; }
		}
		bool I_textComponent<TextComponent_word>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textComponent<TextComponent_word>.IsComplete {
			get { return IsComplete; }
		}
		bool I_textComponent<TextComponent_word>.HasWhiteSpace {
			get { return HasWhiteSpace; }
		}
		bool I_textComponent<TextComponent_word>.HasWordBreak {
			get { return HasWordBreak; }
		}
		bool I_textComponent<TextComponent_word>.HasLineBreak {
			get { return HasLineBreak; }
		}
		bool I_textComponent<TextComponent_word>.HasContext {
			get { return HasContext; }
		}
		TextComponent_word I_textComponent<TextComponent_word>.SetContext (TextContext context) {
			return SetContext(context);
		}
		TextContext I_textComponent<TextComponent_word>.Context {
			get { return Context; }
		}
		TextComponent_text I_textComponent<TextComponent_word>.Source {
			get { return Source; }
		}
		TextComponent_word I_textComponent<TextComponent_word>.Instance {
			get { return Instance; }
		}
		#endregion
		#region IEquatable<string>
		bool IEquatable<string>.Equals (string other) {
			return other.Equals(other);
		}
		#endregion

	}
}