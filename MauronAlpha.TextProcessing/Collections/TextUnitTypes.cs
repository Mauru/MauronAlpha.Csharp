using MauronAlpha.TextProcessing.Units;

namespace MauronAlpha.TextProcessing.Collections {
	
	public class TextUnitTypes:TextComponent {

		public static TextUnitType Text { get {
			return TextUnitType_text.Instance;
		} }

		public static TextUnitType Paragraph {
			get {
				return TextUnitType_paragraph.Instance;
			}
		}

		public static TextUnitType Line {
			get {
				return TextUnitType_line.Instance;
			}
		}

		public static TextUnitType Word {
			get {
				return TextUnitType_word.Instance;
			}
		}

		public static TextUnitType Character {
			get {
				return TextUnitType_character.Instance;
			}
		}

		public static TextUnitType None {
			get {
				return TextUnitType_none.Instance;
			}
		}

		//!! None == Null !!
		public static TextUnitType Null {
			get {
				return TextUnitType_none.Instance;
			}
		}
	}

}
