using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;

namespace MauronAlpha.TextProcessing.DataObjects {
	public class TextRange:TextComponent  {

		//constructor
		public TextRange():base() {}
		public TextRange(Text text, TextContext start, TextContext end) : base() {
			D_text = text;
			D_start = start;
			D_end = end;
		}

		//Values
		private Text D_text;
		public Text Text {
			get {
				return D_text;
			}
		}
		
		private TextContext D_start;
		public TextContext Start {
			get {
				return D_start;
			}
		}

		private TextContext D_end;
		public TextContext End {
			get {
				return D_end;
			}
		}


	}
}
