﻿using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Text.Units {

	//A line of text
	public class TextComponent_line : TextComponent {

		//constructor
		public TextComponent_line (TextComponent_text parent, TextContext context) {}

		//The words in this line
		private MauronCode_dataList<TextComponent_word> Words = new MauronCode_dataList<TextComponent_word>();

		#region The Text this line belongs to
		private TextComponent_text TXT_parent;
		public TextComponent_text Parent {
			get {
				if(TXT_parent==null) {
					NullError("Parent can't be null", this, typeof(TextComponent_text));
				}
				return TXT_parent;
			}
		}
		private TextComponent_line SetParent(TextComponent_text parent){
			TXT_parent=parent;
			return this;	
		}
		#endregion

		#region The Context of the line
		private TextContext TXT_context;
		public TextContext Context {
			get {
				if(TXT_context==null){
					NullError("Context can't be null", this, typeof(TextContext));
				}
				return TXT_context;
			}
		}
		private TextComponent_line SetContext(TextContext context){
			TXT_context=context;
			return this;
		}
		public TextComponent_line OffsetContext(TextContext context){
			Context.Add(context);
			foreach(TextComponent_word word in Words){
				word.OffsetContext(context);
			}
			return this;
		}
		#endregion

		public TextComponent_line AddWord(TextComponent_word word){

			if(Words.Count>0&&Words.LastElement.EndsLine){

				//Add to new line
				TextContext context = Context.Instance;
				context.SetLineOffset (context.LineOffset + 1)
				.SetWordOffset (0)
				.SetCharacterOffset(0);

				Parent.AddLineAtContext (context);
				return Parent.LineByContext(context).AddWord (word);

			}
			Words.AddValue (word);
			return this;
		}
		
		#region Getting the content of this TextComponent

		#region Words
		public TextComponent_word FirstWord {
			get {
				if( Words.Count<1 ) {
					Error("Invalid Index {FirstWord}", this, ErrorType_index.Instance);
				}
				return Words.FirstElement;
			}
		}
		public TextComponent_word WordByContext(TextContext context) {
			if(!Words.ContainsKey(context.WordOffset)){
				Error("Invalid Index {"+context.WordOffset+"}", this, ErrorType_index.Instance);
			}
			return Words.Value(context.WordOffset);
		}
		public TextComponent_word LastWord {
			get { 
				if (Words.Count < 1) {
					Error ("Invalid Index {LastWord}", this, ErrorType_index.Instance);
				}
				return Words.LastElement;
			}
		}
		#endregion

		#region Characters
		public TextComponent_character FirstCharacter {
			get {
				return FirstWord.FirstCharacter;
			}
		}
		public TextComponent_character CharacterByContext(TextContext context) {
			TextComponent_word word = WordByContext(context);
			return word.CharacterByContext(context);
		}
		public TextComponent_character LastCharacter {
			get {
				return LastWord.LastCharacter;
			}
		}
		#endregion

		#endregion

		#region Counting the contents
		public int WordCount {
			get { 
				return Words.Count;
			}
		}
		public int CharacterCount {
			get {
				int result=0;
				foreach(TextComponent_word word in Words){
					result+=word.CharacterCount;
				}
				return result;
			}
		}
		#endregion

		//Output as string
		public string AsString {
			get {
				string result="";
				foreach(TextComponent_word word in Words){
					result+=word.AsString;
				}
				return result;
			}
		}

	}
}