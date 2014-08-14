using System;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Units {

	//A complete text
	public class TextComponent_text:TextComponent {

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
			DATA_words=words;
			return this;
		}	
		public TextComponent_text AddWord(TextComponent_word word){
			if(LastLine.IsComplete){
				AddLine(new TextComponent_line());
			}
			Words.AddValue(word);
			LastLine.AddWord(word);
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
