using MauronAlpha.HandlingData;
using MauronAlpha.ErrorHandling;

namespace MauronAlpha.Text.Units {

	//A line of text
	public class TextComponent_line : TextComponent, I_textComponent<TextComponent_line> {

		//constructor
		public TextComponent_line () {}
		
		//instance
		public TextComponent_line Instance {
			get {
				TextComponent_line result = new TextComponent_line();
				foreach(TextComponent_word word in Words){
					result.AddWord(word);
				}
				return result;
			}
		}

		#region Text, forming the text property

		//Get the line text
		private string STR_text;
		public string Text { get {
			return STR_text;
		} }
		private TextComponent_line SetText (string txt) {
			STR_text=txt;
			return this;
		}
		private TextComponent_line ConstructText () {
			string txt="";
			foreach(TextComponent_word word in Words){
				txt+=word.Text;
			}
			STR_text=txt;
			return this;			
		}

		#endregion

		#region Words

		//get any related words
		private MauronCode_dataList<TextComponent_word> DATA_words;
		private MauronCode_dataList<TextComponent_word> Words {
			get {
				if(DATA_words==null){
					SetWords(new MauronCode_dataList<TextComponent_word>());
				}
				return DATA_words;
			}
		}
		private TextComponent_line SetWords(MauronCode_dataList<TextComponent_word> words) {
			DATA_words=words;
			if(Words.Count==0){
				SetIsEmpty(true);
			}else{
				SetIsEmpty(false);
			}
			ConstructText();
			return this;
		}

		//Words
		public TextComponent_line RemoveWordByIndex(int n){
			if(n<0||n>=Words.Count){
				return this;
			}
			Words.RemoveByKey(n);

			//reconstruct
			if(Words.Count==0){
				SetIsEmpty(true);
			}else{
				SetIsEmpty(false);
			}
			ConstructText();

			return this;
		}
		public TextComponent_line AddWord(TextComponent_word word){
			Words.AddValue(word);

			//reconstruct
			SetIsEmpty(false);
			ConstructText();
			return this;
		}
		public TextComponent_word LastWord {
			get {
				if(Words.Count==0){
					AddWord(new TextComponent_word());
				}
				return Words.LastElement;
			}
		}
		public TextComponent_word FirstWord {
			get {
				if(Words.Count==0){
					return LastWord;
				}
				return Words.FirstElement;
			}
		}
		public TextComponent_word WordByIndex(int n){
			if(n<=0){
				return FirstWord;
			}
			if(n>=WordCount){
				return LastWord;
			}
			return Words.Value(n);
		}
		public int WordCount {
			get {
				return Words.Count;
			}
		}

		#endregion

		#region Characters

		//Characters
		public TextComponent_line RemoveCharacterByIndex(int n){
			int targetIndex=0;
			int wordStartIndex=0;
			foreach(TextComponent_word word in Words){
				targetIndex+= word.CharacterCount;
				if(targetIndex>=n){
					word.RemoveCharacterByIndex(n-wordStartIndex);
					return this;
				}
				wordStartIndex+=word.CharacterCount;
			}
			return this;
		}
		public TextComponent_line AddCharacter(TextComponent_character c){
			
			if(LastWord.EndsLine){
				Error("Last word ends line! Create a new line to solve.",this);
			}
			LastWord.AddCharacter(c);
			
			//reconstruct
			ConstructText();
			SetIsEmpty(false);
			
			return this;
		}
		public TextComponent_character LastCharacter {
			get {
				return LastWord.LastCharacter;
			}
		}
		public TextComponent_character FirstCharacter {
			get {
				return FirstWord.FirstCharacter;
			}
		}
		public TextComponent_character CharacterByIndex(int n){
			if(n==0){
				return FirstCharacter;
			}
			int targetIndex=0;
			int wordStartIndex=0;
			foreach( TextComponent_word word in Words ) {
				targetIndex+=word.CharacterCount;
				if( targetIndex>=n ) {
					return word.CharacterByIndex(n-wordStartIndex);
				}
				wordStartIndex+=word.CharacterCount;
			}
			return LastCharacter;
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

		//is the line terminated by a linebreak
		public bool IsComplete {
			get {
				return LastWord.EndsLine;
			}
		}

		//is the line empty (unset)
		private bool B_isEmpty=true;
		public bool IsEmpty {
			get {
				return B_isEmpty;
			}
		}
		private TextComponent_line SetIsEmpty (bool status) {
			B_isEmpty=status;
			return this;
		}

		//Clear the text
		public TextComponent_line Clear ( ) {
			Words.Clear();
			SetIsEmpty(true);
			SetText(null);
			return this;
		}

		public static TextComponent_line New () {
			return new TextComponent_line();
		}
	}
}