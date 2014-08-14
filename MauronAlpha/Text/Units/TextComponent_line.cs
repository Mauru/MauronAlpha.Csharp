using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Units {

	//A line of text
	public class TextComponent_line : TextComponent {

		//constructor
		public TextComponent_line () {}		

		//get any related words
		private MauronCode_dataList<TextComponent_word> DATA_words;
		public MauronCode_dataList<TextComponent_word> Words {
			get {
				if(DATA_words==null){
					SetWords(new MauronCode_dataList<TextComponent_word>());
				}
				return DATA_words;
			}
		}
		public TextComponent_line SetWords(MauronCode_dataList<TextComponent_word> words) {
			DATA_words=words;
			return this;
		}

		//Words
		public TextComponent_line AddWord(TextComponent_word word){
			Words.AddValue(word);
			SetIsEmpty(false);
			return this;
		}
		public TextComponent_word LastWord {
			get {
				if(Words.Count==0){
					AddWord(new TextComponent_word(Display,this));
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
			SetText(null);
			SetIsEmpty(true);
			return this;
		}

		public static TextComponent_line New () {
			return new TextComponent_line();
		}
	}
}