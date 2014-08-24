using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Text.Units {

	//A line of text
	public class TextComponent_line : TextComponent,I_textComponent<TextComponent_line> {

		//constructor
		public TextComponent_line (TextComponent_text parent, TextContext context) {}

		#region Instance (clone)
		public TextComponent_line Instance {
			get { 
				TextComponent_line line = new TextComponent_line (Parent, Context.Instance);
				foreach (TextComponent_word word in Words) {
					line.AddWord (word.Instance);
				}
				return line;
			}
		}
		#endregion

		//The words in this line
		private MauronCode_dataList<TextComponent_word> Words = new MauronCode_dataList<TextComponent_word>();

		#region The TextComponent_text this line belongs to
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
		#region Same as above (as Source)
		public TextComponent_text Source {
			get { 
				return Parent;
			}
		}
		#endregion

		#region The Context of the line
		private TextContext TXT_context;
		public TextContext Context {
			get {
				if(TXT_context==null){
					NullError("Context can't be null!,(Context)", this, typeof(TextContext));
				}
				return TXT_context;
			}
		}
		public bool HasContext {
			get { 
				return TXT_context == null;
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
		public TextComponent_character CharacterByIndex (int n) {
			if( CharacterCount<n ) {
				Error("CharacterIndex out of bounds {"+n+"}", this, ErrorType_index.Instance);
			}
			TextComponent_character character=FirstCharacter;
			int characterOffset=0;
			foreach( TextComponent_word word in Words ) {
				character=word.LastCharacter;
				if( characterOffset+word.CharacterCount>n ) {
					character=word.CharacterByContext(new TextContext(word.Context.LineOffset, word.Context.WordOffset,n-characterOffset));
					break;
				}
				characterOffset+=word.CharacterCount;
			}
			return character;
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

		#region Boolean states
		public bool IsEmpty {
			get { 
				foreach(TextComponent_word word in Words){
					if (!word.IsEmpty) {
						return false;
					}
				}
				return true;
			}
		}
		public bool IsComplete {
			get {
				return (!IsEmpty && LastWord.EndsLine);
			}
		}
		public bool EndsLine {
			get { return true; }
		}
		public bool HasWhiteSpace {
			get { 
				foreach (TextComponent_word word in Words) {
					if (word.HasWhiteSpace) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasWordBreak {
			get { 
				foreach (TextComponent_word word in Words) {
					if (word.HasWordBreak) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasLineBreak { 
			get { 
				foreach (TextComponent_word word in Words) {
					if (word.HasLineBreak) {
						return true;
					}
				}
				return false;
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

		#region I_textComponent
		string I_textComponent<TextComponent_line>.AsString {
			get { return AsString; }
		}
		bool I_textComponent<TextComponent_line>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textComponent<TextComponent_line>.IsComplete {
			get { return IsComplete; }
		}
		bool I_textComponent<TextComponent_line>.HasWhiteSpace {
			get { return HasWhiteSpace; }
		}
		bool I_textComponent<TextComponent_line>.HasWordBreak {
			get { return HasWordBreak; }
		}
		bool I_textComponent<TextComponent_line>.HasLineBreak {
			get { return HasLineBreak; }
		}
		bool I_textComponent<TextComponent_line>.HasContext {
			get { return HasContext; }
		}
		TextComponent_line I_textComponent<TextComponent_line>.SetContext (TextContext context) {
			return SetContext(context);
		}
		TextContext I_textComponent<TextComponent_line>.Context {
			get { return Context; }
		}
		TextComponent_text I_textComponent<TextComponent_line>.Source {
			get { return Source; }
		}
		TextComponent_line I_textComponent<TextComponent_line>.Instance {
			get { return Instance; }
		}
		#endregion
	}
}