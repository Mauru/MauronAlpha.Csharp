using System.Collections.Generic;
using MauronAlpha.HandlingData;
using MauronAlpha.Text.Utility;

namespace MauronAlpha.Text.Units {

	//A complete text
	public class TextComponent_text:TextComponent, I_textComponent<TextComponent_text> {

		//constructor
		public TextComponent_text(){}

		//Instance
		public TextComponent_text Instance {
			get {
				TextComponent_text instance=new TextComponent_text();
				instance.SetWords(Words);
				return instance;
			}
		}

		//Clear
		public TextComponent_text Clear() {
			ResetLineIndex (false);
			ResetWordIndex (false);
			ResetCharacterIndex (false);
			ResetStringIndex (false);
			SetIsEmpty(true,false);
			return this;
		}

		//Is the text empty?
		private bool B_isEmpty=true;
		public bool IsEmpty {
			get { return B_isEmpty; }
		}
		private TextComponent_text SetIsEmpty (bool status, bool reIndex) {
			B_isEmpty=status;

			if (IsEmpty && reIndex) {
				return Clear ();
			}

			return this;
		}

		#region Text as string

		#region Set text property to null
		private TextComponent_text ResetStringIndex(bool reIndex){
			SetString (null, false);

			//re-index
			if (reIndex) {
				ResetLineIndex (false);
				ResetWordIndex (false);
				ResetCharacterIndex (false);
				SetIsEmpty (true,false);
			}
		}
		#endregion

		#region Constructing the text property
		private TextComponent_text BuildStringIndex(bool reIndex)	{
			ResetTextIndex (false);
			if (CharacterCount > 0) {
				return BuildStringIndexFromCharacters (reIndex);
			}
			if (WordCount > 0) {
				return BuildStringIndexFromWords (reIndex);
			}
			if (LineCount > 0) {
				return BuildStringIndexFromLines (reIndex);
			}
			return SetString ("",reIndex);
		}
		private TextComponent_text BuildStringndexFromCharacters (bool reIndex) {
			string txt="";
			foreach( TextComponent_character c in Characters ) {
				txt+=c.Text;
			}
			SetString (txt, false);

			if (reIndex) {
				ResetLineIndex (false);
				ResetWordIndex (false);
				SetIsEmpty (Text.Length == 0,false);
			}

			return this;
		}
		private TextComponent_text BuildStringIndexFromWords (bool reIndex) {
			string txt="";
			foreach( TextComponent_word c in Words ) {
				txt+=c.Text;
			}
			SetString (txt, false);

			if (reIndex) {
				ResetLineIndex (false);
				ResetCharacterIndex (false);
				SetIsEmpty (Text.Length == 0);
			}

			return this;
		}
		private TextComponent_text BuildStringIndexFromLines (bool reIndex) {
			string txt="";
			foreach( TextComponent_line c in Lines ) {
				txt+=c.Text;
			}
			SetString(txt, false);

			if (reIndex) {
				ResetWordIndex (false);
				ResetCharacterIndex (false);
				SetIsEmpty (Text.Length == 0);
			}

			return this;
		}
		#endregion

		#region Returning the text property
		private string STR_text;
		public string String {
			get {
				if (STR_text == null) {
					BuildStringIndex (false);
				}
				return STR_text;
			}
		}
		#endregion

		#region Setting the text property
		public TextComponent_text SetString (string txt) {
			return SetText(txt, true);
		}
		private TextComponent_text SetString(string text, bool reIndex) {
			STR_text = text;

			//reset indexes
			if (reIndex) {
				SetIsEmpty (Text == null, false);
				ResetLineIndex (false);
				ResetWordIndex (false);
				ResetCharacterIndex (false);
			}
		}
		#endregion

		#region Adding to the TextProperty
		public TextComponent_text AddString(string text){
			return AddString (text, true);
		}
		private TextComponent_text AddString (string text, bool reIndex) {
			if (STR_text == null) {
				SetString("", false);
			}
			STR_text += text;

			if (reIndex) {
				ResetLineIndex (false);
				ResetWordIndex (false);
				ResetCharacterIndex (false);
				SetIsEmpty (StringLength == 0, false);
			}

			return this;
		}
		//context is an offset!
		public TextComponent_text AddStringByContext (string text, TextContext context) {
			return AddStringByContext (text, context, true);
		}
		private TextComponent_text AddStringByContext(string text, TextContext context, bool reIndex){
			TextComponent_text txt = TextHelper.ParseString(text);
			
			//no context, we add to End
			if (context.IsEmpty) {
				return AddString (text, reIndex);
			}

			//negative offsets
			if(context.IsLine&&context.LineOffset<0){
				int newIndex = LineCount-context.LineOffset;
				if(newIndex<0){ newIndex=0;	}
				context.SetLineOffset(newIndex);
				return AddStringByContext(text,context,reIndex);
			}
			if(context.IsWord&&context.WordOffset<0){
				int newIndex = WordCount-context.WordOffset;
				if(newIndex<0){ context.SetLineOffset(context.LineOffset-1); }
				context.SetWordOffset(newIndex);
				return AddStringByContext(text,context,reIndex);
			}
			if(context.IsCharacter&&context.CharacterOffset<0){
				int newIndex = CharacterCount-context.CharacterOffset;
				if(newIndex<0){ context.SetWordOffset(context.WordOffset-1); }
				context.SetCharacterOffset(newIndex);
				return AddStringByContext(text,context,reIndex);
			}

			

			TextComponent_line line;
			TextComponent_word word;
			TextComponent_character character;

			#region add at line
			if (context.IsLineOnly) {
				//firstline
				if(context.LineOffset==0){
					line=FirstLine;
				}
				//lastline
				else if(context.LineOffset>=LineCount){
					line=LastLine;
				}
				//Specific line
				else{
					line=LineByIndex(context.LineOffset);
				}

			}
			#endregion

			//line,word
			else if (context.IsLine && context.IsWord) {
				if(context.LineOffset<0){
					
				}				
			}


			//line, word, character
			if (context.IsLine && context.IsWord && context.IsCharacter) {
			}
			//line, character
			if (context.IsLine && context.IsCharacter) {
			}

			//word
			if (context.IsWordOnly) {
			}

			//word, character
			if (context.IsWord&&context.IsCharacter) {
			}

			//character
			if (context.IsCharacterOnly) {
			}

		}
		#endregion

		#region Getting the length of the property
		private int StringLength { 
			get { 
				if (STR_text == null) {
					return 0;
				}
				return Text.Length;
			}
		}
		#endregion

		#endregion

		#region Text as Characters

		#region Setting Characters to null
		private TextComponent_text ResetCharacterIndex(bool reIndex){
			SetCharacters (null, false);

			//re-index
			if (reIndex) {
				ResetLineIndex (false);
				ResetWordIndex (false);
				ResetTextIndex (false);
				SetIsEmpty (true, false);
			}
			return this;
		}
		#endregion

		#region Constructing the Characters property
		private TextComponent_text BuildCharacterIndex(bool reIndex){
			ResetCharacterIndex (false);
			if (StringLength > 0) {
				return BuildCharacterIndexFromText (reIndex);
			}
			if (WordCount > 0) {
				return BuildCharacterIndexFromWords (reIndex);
			}
			if (LineCount > 0) {
				return BuildCharacterIndexFromLines (reIndex);
			}

			return SetCharacters (new MauronCode_dataList<TextComponent_character> (),reIndex);

		}
		private TextComponent_text BuildCharacterIndexFromText (bool reIndex) {
			ResetCharacterIndex (false);

			ICollection<char> characters = Text.ToCharArray ();
			foreach (char c in characters) {
				TextComponent_character n = new TextComponent_character (c);
				AddCharacter (c, false);
			}

			if (reIndex) {
				ResetLineIndex (false);
				ResetWordIndex (false);
				SetIsEmpty (CharacterCount==0,false);
			}

			return this;
		}
		private TextComponent_text BuildCharacterIndexFromWords (bool reIndex) {
			ResetCharacterIndex (false);
			foreach( TextComponent_word word in Words ) {
				foreach(TextComponent_character c in word.Characters){
					AddCharacter(c,false);
				}
			}

			if (reIndex) {
				ResetTextIndex (false);
				ResetLineIndex (false);
				SetIsEmpty (CharacterCount==0,false);
			}

			return this;
		}
		private TextComponent_text BuildCharacterIndexFromLines (bool reIndex) {
			ResetCharacterIndex (false);
			foreach( TextComponent_line line in Lines ) {
				foreach (TextComponent_character character in line.Characters) {
					AddCharacter (character, false);
				}
			}

			if (reIndex) {
				ResetTextIndex (false);
				ResetWordIndex (false);
				SetIsEmpty (CharacterCount == 0,false);
			}

			return this;
		}
		#endregion

		#region Returning the Characters Property
		private MauronCode_dataList<TextComponent_character> DATA_characters;
		public MauronCode_dataList<TextComponent_character> Characters {
			get {
				if (DATA_characters == null) {
					BuildCharacterIndex (false);
				}
				return DATA_characters;
			}
		}
		#endregion

		#region Setting the Characters property
		public TextComponent_text SetCharacters (MauronCode_dataList<TextComponent_character> characters) {
			return SetCharacters(characters, true);
		}
		private TextComponent_text SetCharacters(MauronCode_dataList<TextComponent_character> characters, bool reIndex){
			DATA_characters = characters;

			//reset indexes
			if (reIndex) {
				SetIsEmpty (CharacterCount==0, false);
				ResetLineIndex (false);
				ResetWordIndex (false);
				ResetTextIndex (false);
			}
			return this;
		}
		#endregion

		#region Adding to the Characters property
		public TextComponent_text AddCharacter(TextComponent_character c){
			return AddCharacter (c, true);
		}
		private TextComponent_text AddCharacter(TextComponent_character c, bool reIndex){
			if (DATA_characters == null) {
				SetCharacters (new MauronCode_dataList<TextComponent_character> (), false);
			}

			if (reIndex) {
				ResetTextIndex (false);
				ResetWordIndex (false);
				ResetLineIndex (false);
				SetIsEmpty (CharacterCount == 0, false);
			}
			return this;
		}
		public TextComponent_text AddCharacterByContext(TextComponent_character c, TextContext context){
			return AddCharacterByContext (c, context, true);
		}
		private TextComponent_text AddCharacterByContext(TextComponent_character c, TextContext context, bool reIndex){

		}
		#endregion

		#endregion

		#region Text as Lines

		#region Set Lines to null
		private TextComponent_text ResetLineIndex() {
			SetLines (null,false);
			return this;
		}
		#endregion

		#region Constructing the Lines property
		private TextComponent_text BuildLineIndex(bool reIndex){
			ResetLineIndex(false);
			if (WordCount > 0) {
				BuildLineIndex<TextComponent_word> ();
			}else if (CharacterCount > 0) {
				BuildLineIndex<TextComponent_character> ();
			}else{
				SetLines (new MauronCode_dataList<TextComponent_line> ());
			}
			return this;
		}
		private TextComponent_text BuildLineIndexFromWords(){
			ResetLineIndex ();
			foreach (TextComponent_word word in Words) {
				TextComponent_line activeLine = LastLine;
				if (activeLine.IsComplete) {
					AddLine (new TextComponent_line (), false);
				}
				LastLine.AddWord (word);
			}
			return this;
		}
		private TextComponent_text BuildLineIndexFromCharacters(){
			ResetWordIndex ();
			BuildWordIndex<TextComponent_character> ();
			return BuildWordIndex<TextComponent_word> ();
		}
		#endregion

		//Lines
		private MauronCode_dataList<TextComponent_line> DATA_lines;
		private MauronCode_dataList<TextComponent_line> Lines {
			get {
				if( DATA_lines==null ) {
					BuildLineIndex ();
				}
				return DATA_lines;
			}
		}
		private TextComponent_text SetLines (MauronCode_dataList<TextComponent_line> lines) {
			DATA_lines=lines;
			return this;
		}
		
		private TextComponent_text AddLine (TextComponent_line line) {
			Lines.AddValue(line);
			return this;
		}
		private TextComponent_text RemoveLineByIndex(int n){
			//remove the words
			if(!Lines.ContainsKey(n)){
				return this;
			}
			int startIndex=0;
			int range=Lines.Value(n).WordCount;
			for(int i=0; i<=n;i++){
				TextComponent_line line = Lines.Value(i);
				startIndex+=line.WordCount;
			}
			Words.RemoveByKey(startIndex);			
			return this;			
		}
		
		public TextComponent_line LastLine {
			get {
				if( LineCount<1 ) {
					AddLine(new TextComponent_line());
				}
				return Lines.LastElement;
			}
		}
		public TextComponent_line FirstLine {
			get {
				if( LineCount<1 ) {
					AddLine(new TextComponent_line());
				}
				return Lines.FirstElement;
			}
		}
		public TextComponent_line LineByIndex(int n){
			if(n>=LineCount){
				return LastLine;
			}
			if(n<=0){
				return FirstLine;
			}
			return Lines.Value(n);
		}

		public int LineCount {
			get { 
				return Lines.Count;
			}
		}

		#endregion		

		#region Text as Words

		//Building the word Index
		private TextComponent_text ResetWordIndex(){

		}

		//words
		private MauronCode_dataList<TextComponent_word> DATA_words;
		private MauronCode_dataList<TextComponent_word> Words { get {
			if(DATA_words==null){
				DATA_words=new MauronCode_dataList<TextComponent_word>();
			}
			return DATA_words;
		}}
		public TextComponent_text SetWords(MauronCode_dataList<TextComponent_word> words){
			Clear();
			foreach(TextComponent_word word in words){
				AddWord(word);
			}
			return this;
		}	
		public TextComponent_text AddWord(TextComponent_word word){
			if(LastLine.IsComplete){
				AddLine(new TextComponent_line());
			}
			Words.AddValue(word);
			LastLine.AddWord(word);
			SetIsEmpty(false);
			ConstructText();
			return this;
		}
		
		public TextComponent_word LastWord { get {
			if(WordCount<1){
				AddWord(new TextComponent_word());
			}
			return Words.LastElement;
		} }
		public TextComponent_word FirstWord {
			get {
				if( WordCount<1 ) {
					AddWord(new TextComponent_word());
				}
				return Words.FirstElement;
			}
		}			
		public TextComponent_word WordByIndex(int n){
			if (n <= 0) {
				return FirstWord;
			}
			if (n >= WordCount) {
				return LastWord;
			}
			return Words.Value (n);
		}

		public int WordCount {
			get { 
				return Words.Count;
			}
		}

		#endregion


	}

}
