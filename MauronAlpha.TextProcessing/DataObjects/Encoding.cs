using MauronAlpha.HandlingData;
using MauronAlpha.TextProcessing.Units;
namespace MauronAlpha.TextProcessing.DataObjects {
	
	//Defines control characters in strings
	public class Encoding:TextComponent {

		public static Character ToCharacter(char character) {
			Character unit = new Character();

			if(IsWhiteSpace(character)) {
				unit.IsWhiteSpace = true;
				return unit;
			}

			if(IsTab(character)) {
				unit.IsTab = true;
				return unit;
			}
			
			if(IsLineBreak(character)) {
				unit.IsLineBreak = true;
				return unit;
			}

			if (IsParagraphBreak(character)) {
				unit.IsParagraphBreak = true;
				return unit;
			}

			unit.Symbol = character;
			return unit;
		}

		public bool TreatTabAsWhiteSpaces = true;
		public int TabAsWhiteSpaceNo = 3;
		
		public string TabAsWhiteSpaces {
			get {
				string result = "";
				for (int n = 0; n < TabAsWhiteSpaceNo; n++)
					result += WhiteSpace;
				return result;
			}
		}
		public string TabAsVisualWhiteSpaces {
			get {
				string result = "";
				for (int n = 0; n < TabAsWhiteSpaceNo; n++)
					result += VisualWhiteSpace;
				return result;
			}
		}
		
		public static char Tab {
			get {
				return '\u0009';
			}
		}

		public static char WhiteSpace {
			get {
				return '\u0020';
			}
		}
		public static string VisualWhiteSpace {
			get {
				return "·";
			}
		}

		public static char LineBreak {
			get {
				return '\u000D';
			}
		}
		public static string VisualLineBreak {
			get {
				return "{LB}";
			}
		}

		public static char ParagraphBreak {
			get {
				return '\u2029';
			}
		}
		public static string VisualParagraphBreak {
			get {
				return "{PB}";
			}
		}

		public static bool IsTab(char ch) {
			return ch.Equals('\u0009');
		}
		public static bool IsWhiteSpace(char ch) {
			return WhiteSpaces.ContainsValue(ch);
		}
		public static bool IsLineBreak(char ch) {
			return LineBreaks.ContainsValue(ch);
		}
		public static bool IsParagraphBreak(char ch) {
			return ch.Equals('\u2029');
		}

		static MauronCode_dataList<char> LineBreaks = new MauronCode_dataList<char>(){
			'\u000A','\u000B','\u000C','\u000D','\u0085','\u2028'
		};

		static MauronCode_dataList<char> WhiteSpaces = new MauronCode_dataList<char>() {
			'\u0020','\u00A0',' '
		};

		public static Encoding Instance {
			get {
				return new Encoding();
			}
		}
	
	}

}
