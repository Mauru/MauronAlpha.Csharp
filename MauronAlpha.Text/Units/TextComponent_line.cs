using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Text.Units {

	//A line of text
	public class TextComponent_line : MauronCode_textComponent,I_textComponent<TextComponent_line> {

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

		#region ReadOnly
		private bool B_isReadOnly=false;
		public bool IsReadOnly {
			get { return B_isReadOnly; }
		}
		public TextComponent_line SetReadOnly (bool status) {
			B_isReadOnly=status;
			return this;
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
		public TextComponent_line OffsetContext(int line, int word, int character) {
			Context.Add(line,word,character);
			foreach( TextComponent_word w in Words ) {
				w.OffsetContext(line,word,character);
			}
			return this;
		}
		#endregion
		#region The Line Number (by context)
		public int Index {
			get {
				return Context.LineOffset;
			}
		}
		#endregion

		#region Checking if a line contains another TextComponent
		public bool ContainsWordIndex(int n) {
			if(n<0||n>=WordCount){
				return false;
			}
			return true;
		}
		#endregion

		#region Add a word to the Line
		public TextComponent_line AddWord(TextComponent_word word){
			#region ReadOnly Check
			if( IsReadOnly ) {
				Error("Is protected!,(AddWord)", this, ErrorType_protected.Instance);
			}
			#endregion

			return InsertWordAtIndex(WordCount, word);
			
		}
		#endregion

		#region Get the Next Line, if it does not exist, create it
		public TextComponent_line NextLine {
			get {
				if(!Parent.ContainsLineIndex(Context.LineOffset+1)){
					Exception("Invalid lineIndex!,{"+(Context.LineOffset+1)+"},(NextLine)",this,ErrorResolution.Create);
					TextComponent_line line = Parent.NewLine;
				}
				return Parent.LineByIndex(Context.LineOffset+1);
			}
		}
		#endregion
		#region Get the Previous Line, if it does not exist, create it
		public TextComponent_line PreviousLine {
			get {
				if( !Parent.ContainsLineIndex(Context.LineOffset-1) ) {
					Exception("Invalid lineIndex!,{"+(Context.LineOffset-1)+"},(PreviousLine)", this, ErrorResolution.Create);
					TextComponent_line line=Parent.NewLine;
				}
				return Parent.LineByIndex(Context.LineOffset-1);
			}
		}
		#endregion

		public TextComponent_line InsertWordAtIndex(int index, TextComponent_word word){
			#region ReadOnly Check
			if( IsReadOnly ) {
				Error("Is protected!,(AddWord)", this, ErrorType_protected.Instance);
			}
			#endregion
			#region ErrorCheck Empty
			if(IsEmpty&&index!=0){
				Error("Index Out of bounds!,{"+index+"},(InsertWordAtIndex)",this,ErrorType_bounds.Instance);
			}
			#endregion
			
			#region word ends the current line !R
			if(word.EndsLine) {
				
				foreach(TextComponent_word oldWord in Words){
					//move all words to next line
					if(IsLastLine){
						TextComponent_line line=Parent.NewLine;
					}
					NextLine.AddWord(oldWord);
				}
				//whipe words
				Words.Clear();
				
				//set new word context
				word.Context.SetOffset(Context.LineOffset,0,0);

				return this;

			}
			#endregion

			//collect all following words
			MauronCode_dataList<TextComponent_word> words = Words.Range(index);
			
			//insert word
			Words.InsertValueAt(index,word);

			//advance all following words
			foreach(TextComponent_word w in words) {
				w.OffsetContext(0,1,0);
			}

			//set new context of word
			word.Context.SetOffset(Context.LineOffset,index,0);

			return this;
		}

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
		public TextComponent_word WordByIndex(int n){
			#region ErrorCheck bounds
			if(n<0||n>=CharacterCount){
				Error("WordIndex out of bounds!,{"+n+"},(WordbyIndex)",this,ErrorType_bounds.Instance);
			}
			#endregion
			return Words.Value(n);
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

		#region Boolean Behavior Switches
		/// <summary>
		/// Can this line be ended by things other than a linebreak?
		/// </summary>
		public bool CanBreak {
			get {
				return false;
			}
		}
		#endregion

		#region Boolean states
		public bool HasOffsetNeighbor(int line, int word, int character){
			//the context
			TextContext query = Context.Instance.Add(line,word,character);

			//Starting with the line offset
			if(line!=0){
				//negative
				if(query.LineOffset<0){	return false; }
		
				//queried line does not exist
				if(!Parent.ContainsLineIndex(query.LineOffset)){ return false; }
			}

			//we now know that the line does exist

			//get the line
			TextComponent_line l = Parent.LineByIndex(query.LineOffset);
			
			//we are looking for the word...

			//starting with the word offset
			if(word!=0) {
				
				//is word an offset for the line?
				if(l.IsLineOffset(0,word,0)){
					
				}

				//the queried word does not exist


			}

			if(query.IsWordOffset(l)){
				return true;
			}
			if(query.IsWordOffset(this)){}
		}
		public bool IsOffset(TextContext context){
			return context.IsLineOffset(this);
		}
		public bool IsOffset(int line, int word, int character){
			return IsOffset(Context.New(line,word,character));
		}
		public bool IsLineOffset(TextContext context){
			return context.IsLineOffset(this);
		}
		public bool IsLineOffset(int line, int word, int character){
			return Context.New(line,word,character).IsLineOffset(this);
		}

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
		public bool IsAtTextEnd {
			get {
				return IsLastLine;
			}
		}
		public bool IsAtTextStart {
			get {
				return Context.LineOffset==0;
			}
		}
		public bool IsLastLine {
			get {
				return Parent.ContainsContext(Context.LineOffset+1,0,0);
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