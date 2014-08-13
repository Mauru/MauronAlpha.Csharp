using System;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Units {

	//A complete text
	public class TextComponent_text:TextComponent {

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
			Words.AddValue(word);
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
		
		public TextComponent_text AddCharacter(TextComponent_character c){
			if(LastWord.IsComplete){
				AddWord(new TextComponent_word());
			}
			LastWord.AddCharacter(c);
			return this;
		}
	}

}
