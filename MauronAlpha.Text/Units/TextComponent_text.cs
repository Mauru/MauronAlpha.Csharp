using System;
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

		#region Text, forming the text property

		//Get the line text
		private string STR_text;
		public string Text {
			get {
				return STR_text;
			}
		}
		private TextComponent_text SetText (string txt) {
			STR_text=txt;
			return this;
		}
		private TextComponent_text ConstructText ( ) {
			string txt="";
			foreach( TextComponent_character c in Characters ) {
				txt+=c.Text;
			}
			STR_text=txt;
			return this;
		}

		#endregion

		//Clear
		public TextComponent_text Clear() {
			SetWords(null);
			SetLines(null);
			SetCharacters(null);
			SetText(null);
			SetIsEmpty(true);
			return this;
		}

		//is the text empty?
		private bool B_isEmpty=true;
		public bool IsEmpty {
			get { return B_isEmpty; }
		}
		private TextComponent_text SetIsEmpty (bool status) {
			B_isEmpty=status;
			return this;
		}

		//Lines
		private MauronCode_dataList<TextComponent_line> DATA_lines;
		public MauronCode_dataList<TextComponent_line> Lines {
			get {
				if( DATA_lines==null ) {
					DATA_lines=new MauronCode_dataList<TextComponent_line>();
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
		public TextComponent_line LastLine {
			get {
				if( Lines.Count<1 ) {
					AddLine(new TextComponent_line());
				}
				return Lines.LastElement;
			}
		}
		public TextComponent_line FirstLine {
			get {
				if( Lines.Count<1 ) {
					AddLine(new TextComponent_line());
				}
				return Lines.FirstElement;
			}
		}			

		//words
		private MauronCode_dataList<TextComponent_word> DATA_words;
		public MauronCode_dataList<TextComponent_word> Words { get {
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
			if(Words.Count<1){
				AddWord(new TextComponent_word());
			}
			return Words.LastElement;
		} }
		public TextComponent_word FirstWord {
			get {
				if( Words.Count<1 ) {
					AddWord(new TextComponent_word());
				}
				return Words.FirstElement;
			}
		}			
		
		//Add a character
		public TextComponent_text AddCharacter(TextComponent_character c){
			if(LastWord.IsComplete){
				AddWord(new TextComponent_word());
			}
			LastWord.AddCharacter(c);
			return this;
		}
	}

}
