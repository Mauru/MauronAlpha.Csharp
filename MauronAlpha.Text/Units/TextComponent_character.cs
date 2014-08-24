using MauronAlpha.Text.Utility;

using System;


namespace MauronAlpha.Text.Units {
	
	//A character in a text
	public class TextComponent_character:TextComponent,IEquatable<char>,IEquatable<string>, I_textComponent<TextComponent_character> {

		//constructor
		public TextComponent_character(TextComponent_word parent, TextContext context, char c){
			SetChar(c);
			SetParent (parent);
			SetContext (context);
		}

		#region Get The TextComponent_text of this Object
		public TextComponent_text Source {
			get {
				return Parent.Source;
			}
		}
		#endregion

		#region Instance
		public TextComponent_character Instance {
			get {
				return new TextComponent_character(Parent,Context.Instance,Char);
			}
		}
		#endregion

		#region The Context
		private TextContext TXT_context;
		public TextContext Context {
			get { 
				return TXT_context;
			}
		}
		public bool HasContext {
			get {
				return TXT_context==null;
			}
		}
		public TextComponent_character SetContext(TextContext context){
			TXT_context=context;
			return this;
		}
		public TextComponent_character OffsetContext (TextContext context) {
			Context.Add(context);
			return this;
		}
		#endregion

		#region The Parent
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
		#endregion

		#region The Character (char)
		private char CHAR_txt=TextHelper.Empty;
		public char Char {
			get { 
				return CHAR_txt;
			}
		}
		public TextComponent_character SetChar(char c){
			CHAR_txt=c;
			return this;
		}
		#endregion

		#region Boolean States
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
		public bool IsEmpty {
			get {
				return CHAR_txt==TextHelper.Empty;
			}
		}
		public bool IsLineBreak {
			get {	return TextHelper.IsLineBreak(Char); }
		}
		public bool IsWhiteSpace {
			get {
				return TextHelper.IsWhiteSpace(Char);
			}
		}
		public bool IsWordBreak {
			get {
				return TextHelper.IsWordBreak(Char);
			}
		}
		#endregion

		//Output as string
		public string AsString {
			get {
				if(IsEmpty){
					return "";
				}
				return ""+Char;
			}
		}

		#region IEquatable<char>
		bool IEquatable<char>.Equals (char other) {
			return other.Equals(Char);
		}
		#endregion
		#region IEquatable<string>
		bool IEquatable<string>.Equals (string other) {
			return AsString.Equals(other);
		}
		#endregion
		#region I_textComponent
		string I_textComponent<TextComponent_character>.AsString {
			get { return AsString; }
		}
		bool I_textComponent<TextComponent_character>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textComponent<TextComponent_character>.IsComplete {
			get { return IsEmpty; }
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
			get { return Source; }
		}
		TextComponent_character I_textComponent<TextComponent_character>.Instance {
			get { return Instance; }
		}
		#endregion


	}

}