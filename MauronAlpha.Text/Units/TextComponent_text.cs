using System.Collections.Generic;
using MauronAlpha.HandlingData;

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
			ResetTextIndex (false);
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
		private TextComponent_text ResetTextIndex(bool reIndex){
			SetText (null, false);

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
		private TextComponent_text BuildTextIndex(bool reIndex)	{
			ResetTextIndex (false);
			if (CharacterCount > 0) {
				return BuildTextIndexFromCharacters (reIndex);
			}
			if (WordCount > 0) {
				return BuildTextIndexFromWords (reIndex);
			}
			if (LineCount > 0) {
				return BuildTextIndexFromLines (reIndex);
			}
			return SetText ("",reIndex);
		}
		private TextComponent_text BuildTextIndexFromCharacters (bool reIndex) {
			string txt="";
			foreach( TextComponent_character c in Characters ) {
				txt+=c.Text;
			}
			SetText (txt, false);

			if (reIndex) {
				ResetLineIndex (false);
				ResetWordIndex (false);
				SetIsEmpty (Text.Length == 0,false);
			}

			return this;
		}
		private TextComponent_text BuildTextIndexFromWords (bool reIndex) {
			string txt="";
			foreach( TextComponent_word c in Words ) {
				txt+=c.Text;
			}
			SetText (txt, false);

			if (reIndex) {
				ResetLineIndex (false);
				ResetCharacterIndex (false);
				SetIsEmpty (Text.Length == 0);
			}

			return this;
		}
		private TextComponent_text BuildTextIndexFromLines (bool reIndex) {
			string txt="";
			foreach( TextComponent_line c in Lines ) {
				txt+=c.Text;
			}
			SetText (txt, false);

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
		public string Text {
			get {
				if (STR_text == null) {
					BuildTextIndex (false);
				}
				return STR_text;
			}
		}
		#endregion

		#region Setting the text property
		public TextComponent_text SetText (string txt) {
			return SetText(txt, true);
		}
		private TextComponent_text SetText(string text, bool reIndex){
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
		public TextComponent_text AddText(string text){
			return AddText (text, true);
		}
		private TextComponent_text AddText(string text, bool reIndex){
			if (STR_text == null) {
				SetText ("", false);
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
		public TextComponent_text AddTextbyContext(string text, TextContext context){
			return AddTextByContext (text, context, true);
		}
		private TextComponent_text AddTextbyContext(string text, TextContext context, bool reIndex){
			if (context.IsEmpty) {
				return AddText (text, reIndex);
			}

			//line
			if (context.IsLineOnly) {
			}

			if (context.IsLine && context.IsWord) {
			}
			if (context.IsLine && context.IsWord && context.IsCharacter) {
			}
			if (context.IsLine && context.IsCharacter) {
			}

			//word
			if (context.IsWordOnly) {
			}

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
		private TextComponent_text BuildLineIndex<TextComponent_word>(){
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
		private TextComponent_text BuildLineIndex<TextComponent_character>(){
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
