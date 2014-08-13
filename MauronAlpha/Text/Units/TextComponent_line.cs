using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Units {

	//A line of text
	public class TextComponent_line : TextComponent {

		//constructor
		public TextComponent_line (I_textDisplay source, int index) {
			SetIndex(index);
			SetDisplay(source);
		}
		public TextComponent_line (I_textDisplay source, int index, string text) {
			SetIndex(index);
			SetDisplay(source);
			SetText(text);
		}

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

		//the Text
		private string STR_text;
		public string Text { get { return STR_text; } }
		public TextComponent_line SetText (string text) {
			STR_text=text;
			SetIsEmpty(false);
			return this;
		}

		private I_textDisplay IT_display;
		public I_textDisplay Display {
			get {
				if( IT_display==null ) {
					MauronCode.Error("Invalid output source (currently hardcoded to console)", this);
				}
				return IT_display;
			}
		}
		public TextComponent_line SetDisplay (I_textDisplay m) {
			IT_display=m;
			return this;
		}

		public static TextComponent_line New (I_textDisplay display, string s) {
			return new TextComponent_line(display, display.TextBuffer.NextIndex, s);
		}
		public static TextComponent_line New (I_textDisplay display) {
			return new TextComponent_line(display, display.TextBuffer.NextIndex);
		}

		//the line number
		private int INT_index;
		public int Index { get { return INT_index; } }
		public TextComponent_line SetIndex (int n) {
			INT_index=n;
			return this;
		}
	}
}