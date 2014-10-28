﻿using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Units {

	//A line of text
	public class TextUnit_line : TextComponent_unit,I_textUnit<TextUnit_line> {

		//constructor
		public TextUnit_line (TextUnit_paragraph parent, TextContext context):base() {
			TXT_parent=parent;
			TXT_context=context;
		}

		#region Instance (clone)
		public TextUnit_line Instance {
			get { 
				TextUnit_line line = new TextUnit_line (Parent, Context.Instance);
				foreach (TextUnit_word word in Words) {
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
		public TextUnit_line SetReadOnly (bool status) {
			B_isReadOnly=status;
			return this;
		}
		#endregion

		//The words in this line
		private MauronCode_dataList<TextUnit_word> Words = new MauronCode_dataList<TextUnit_word>();

		#region PARENT - The TextUnit_paragraph this line belongs to
		private TextUnit_paragraph TXT_parent;
		public TextUnit_paragraph Parent {
			get {
				if(TXT_parent==null) {
					NullError("Parent can't be null", this, typeof(TextUnit_text));
				}
				return TXT_parent;
			}
		}
		private TextUnit_paragraph SetParent (TextUnit_paragraph parent) {
			TXT_parent=parent;
			return this;	
		}
		#endregion

		#region Same as above (as Source)
		public TextUnit_text Source {
			get { 
				return Parent.Parent;
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
		private TextUnit_line SetContext(TextContext context){
			TXT_context=context;
			return this;
		}
		public TextUnit_line OffsetContext(TextContext context){
			Context.Add(context);
			foreach(TextUnit_word word in Words){
				word.OffsetContext(context);
			}
			return this;
		}
		public TextUnit_line OffsetContext(int line, int word, int character) {
			Context.Add(line,word,character);
			foreach( TextUnit_word w in Words ) {
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
		public TextUnit_line AddWord(TextUnit_word word){
			#region ReadOnly Check
			if( IsReadOnly ) {
				Error("Is protected!,(AddWord)", this, ErrorType_protected.Instance);
			}
			#endregion

			return InsertWordAtIndex(WordCount, word);
			
		}
		#endregion

		#region Get the Next Line, if it does not exist, create it
		public TextUnit_line NextLine {
			get {
				if(!Parent.ContainsLineIndex(Context.LineOffset+1)){
					Exception("Invalid lineIndex!,{"+(Context.LineOffset+1)+"},(NextLine)",this,ErrorResolution.Create);
					TextUnit_line line = Parent.NewLine;
				}
				return Parent.LineByIndex(Context.LineOffset+1);
			}
		}
		#endregion
		#region Get the Previous Line, if it does not exist, create it
		public TextUnit_line PreviousLine {
			get {
				if( !Parent.ContainsLineIndex(Context.LineOffset-1) ) {
					Exception("Invalid lineIndex!,{"+(Context.LineOffset-1)+"},(PreviousLine)", this, ErrorResolution.Create);
					TextUnit_line line=Parent.NewLine;
				}
				return Parent.LineByIndex(Context.LineOffset-1);
			}
		}
		#endregion

		public TextUnit_line InsertWordAtIndex(int index, TextUnit_word word){
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
				
				foreach(TextUnit_word oldWord in Words){
					//move all words to next line
					if(IsLastLine){
						TextUnit_line line=Parent.NewLine;
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
			MauronCode_dataList<TextUnit_word> words = Words.Range(index);
			
			//insert word
			Words.InsertValueAt(index,word);

			//advance all following words
			foreach(TextUnit_word w in words) {
				w.OffsetContext(0,1,0);
			}

			//set new context of word
			word.Context.SetOffset(Context.LineOffset,index,0);

			return this;
		}

		#region Words
		public TextUnit_word FirstWord {
			get {
				if( Words.Count<1 ) {
					Error("Invalid Index {FirstWord}", this, ErrorType_index.Instance);
				}
				return Words.FirstElement;
			}
		}
		public TextUnit_word WordByContext(TextContext context) {
			if(!Words.ContainsKey(context.WordOffset)){
				Error("Invalid Index {"+context.WordOffset+"}", this, ErrorType_index.Instance);
			}
			return Words.Value(context.WordOffset);
		}
		public TextUnit_word WordByIndex(int n){
			#region ErrorCheck bounds
			if(n<0||n>=CharacterCount){
				Error("WordIndex out of bounds!,{"+n+"},(WordbyIndex)",this,ErrorType_bounds.Instance);
			}
			#endregion
			return Words.Value(n);
		}
		public TextUnit_word LastWord {
			get { 
				if (Words.Count < 1) {
					Error ("Invalid Index {LastWord}", this, ErrorType_index.Instance);
				}
				return Words.LastElement;
			}
		}
		#endregion
		#region Characters
		public TextUnit_character FirstCharacter {
			get {
				return FirstWord.FirstCharacter;
			}
		}
		public TextUnit_character CharacterByContext(TextContext context) {
			TextUnit_word word = WordByContext(context);
			return word.CharacterByContext(context);
		}
		public TextUnit_character CharacterByIndex (int n) {
			if( CharacterCount<n ) {
				Error("CharacterIndex out of bounds {"+n+"}", this, ErrorType_index.Instance);
			}
			TextUnit_character character=FirstCharacter;
			int characterOffset=0;
			foreach( TextUnit_word word in Words ) {
				character=word.LastCharacter;
				if( characterOffset+word.CharacterCount>n ) {
					character=word.CharacterByContext(new TextContext(word.Context.LineOffset, word.Context.WordOffset,n-characterOffset));
					break;
				}
				characterOffset+=word.CharacterCount;
			}
			return character;
		}		
		public TextUnit_character LastCharacter {
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
				foreach(TextUnit_word word in Words){
					result+=word.CharacterCount;
				}
				return result;
			}
		}
		#endregion

		#region Boolean Behavior Switches
		/// <summary>Can this line be ended by things other than a linebreak?</summary>
		public bool CanBreak {
			get {
				return false;
			}
		}
		
		//We are talking relative offset!
		public bool IsIndexOffset(int offset) {
			int result = Index+offset;
			if(result == Index ) {
				return false;
			}
			if(result<0) {
				return false;
			}
			if(result>Parent.ChildCount){
				return false;
			}
			return true;
		}
		public bool IsOffset(TextContext context){
			return context.IsLineOffset(this);
		}
		public bool IsOffset(int line, int word, int character){
			return IsOffset(Context.New(line,word,character));
		}

		public bool IsCharacterOffset(TextContext context) {
			return context.IsLineOffset(this);
		}
		public bool IsCharacterOffset (int line, int word, int character) {
			return Context.New(line, word, character).IsCharacterOffset(this);
		}

		public bool IsLineOffset(int line, int word, int character){
			return Context.New(line,word,character).IsLineOffset(this);
		}
		public bool IsLineOffset (TextContext context) {
			return context.IsLineOffset(this);
		}

		public bool IsIndexNeighbor(int index) {
			if(Index == index) {
				return false;	
			}
			if(index>Parent.LineCount) {
				return false;
			}
			if(index<0) {
				return false;
			}
			return true;
		}


		/* Incomplete
		/// <summary>
		/// Checks If another Line exists at the offset position (0 is ignored, word and character are solved)
		/// </summary>
		public bool IsLineOffset (int line, int word, int character) {
			return Context.IsLineOffset(this, line, word, character);

			//the context, modified with line,word,character
			TextContext query=Context.Instance.Add(line, word, character);

			#region query could be 0,0,0 : we default to returning false!
			if( query.Equals(0, 0, 0) ) {
				//OffsetNeighbor is this
				return false;
			}
			#endregion

			#region Do we have a line offset? @?Return: false
			if( line!=0 ) {

				//negative, no previous lines
				if( query.LineOffset<0&&Index==0 ) { return false; }

				//queried line does not exist (Ask Parent)
				if( !Parent.ContainsLineIndex(query.LineOffset) ) { return false; }

			}
			#endregion

			//we now know that the line does exist

			//get the line
			TextUnit_line candidate_line=Parent.LineByIndex(query.LineOffset);

			//we are looking for the word...

			#region Do we have a word offset?
			if( word!=0 ) {

				TextUnit_line candidate_line_sub;

				//is word an offset for the line?
				if( candidate_line.IsLineOffset(0, word, 0) ) {

					//we get a superbasic numerical offset: negative
					if( WordCount+word<0 ) {
						//ask parent if previous line exists
						if( !Parent.ContainsLineIndex(Index-1) ) {
							return false;
						}
						//we know a previousLine exists
						candidate_line_sub=Parent.LineByIndex(Index-1);

						//ask the previous line if the new word/char is an offset !We actually still need to check if word or char reverts it
						if( !candidate_line_sub.IsOffset(0, word+WordCount, character) ) {
							return true;
						}

					}
					//positive offset
					if( word>WordCount ) {
						//ask parent if next line exists
						if( !Parent.ContainsLineIndex(Index+1) ) {
							return false;
						}
						//we know a next line exists
						TextUnit_line candidate_line_next=Parent.LineByIndex(Index+1);

						//ask the next line if the new word/char is an offset
						return candidate_line_next.OffsetNeightbor(0, word-WordCount, character);
					}

				}

				//the queried word does not exist


			}

			if( query.IsWordOffset(l) ) {
				return true;
			}
			if( query.IsWordOffset(this) ) { }
		}
		*/


		public bool IsEmpty {
			get { 
				foreach(TextUnit_word word in Words){
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
				foreach (TextUnit_word word in Words) {
					if (word.HasWhiteSpace) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasWordBreak {
			get { 
				foreach (TextUnit_word word in Words) {
					if (word.HasWordBreak) {
						return true;
					}
				}
				return false;
			}
		}
		public bool HasLineBreak { 
			get { 
				foreach (TextUnit_word word in Words) {
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
				foreach(TextUnit_word word in Words){
					result+=word.AsString;
				}
				return result;
			}
		}

		#region I_textUnit
		string I_textUnit<TextUnit_line>.AsString {
			get { return AsString; }
		}
		bool I_textUnit<TextUnit_line>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textUnit<TextUnit_line>.IsComplete {
			get { return IsComplete; }
		}
		bool I_textUnit<TextUnit_line>.HasWhiteSpace {
			get { return HasWhiteSpace; }
		}
		bool I_textUnit<TextUnit_line>.HasWordBreak {
			get { return HasWordBreak; }
		}
		bool I_textUnit<TextUnit_line>.HasLineBreak {
			get { return HasLineBreak; }
		}
		bool I_textUnit<TextUnit_line>.HasContext {
			get { return HasContext; }
		}
		TextUnit_line I_textUnit<TextUnit_line>.SetContext (TextContext context) {
			return SetContext(context);
		}
		TextContext I_textUnit<TextUnit_line>.Context {
			get { return Context; }
		}
		TextUnit_text I_textUnit<TextUnit_line>.Source {
			get { return Source; }
		}
		TextUnit_line I_textUnit<TextUnit_line>.Instance {
			get { return Instance; }
		}
		#endregion
	
		
	}
}