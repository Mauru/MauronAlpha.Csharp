using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;

namespace MauronAlpha.TextProcessing.DataObjects {

	//Defines 
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
				if (D_start == null) {

					if (D_text != null)
						return D_text.Start;

					return TextContext.None;
				}
				return D_start;
			}
		}

		private TextContext D_end;
		public TextContext End {
			get {
				if (D_end == null) { 
					if (D_start != null)
						return D_start;

					if (D_text != null)
						return D_text.End;

					return TextContext.None;
				}
				return D_end;
			}
		}

		public static TextRange None {
			get {
				return new TextRange();
			}
		}

	}
}
