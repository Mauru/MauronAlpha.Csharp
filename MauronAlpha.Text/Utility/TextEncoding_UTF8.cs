using MauronAlpha.Text.Units;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Utility {
	
	//A class that helps with Text Operations
	public class TextEncoding_UTF8 : TextUtility_encoding {
		
		public static MauronCode_dataList<char> WhiteSpaces= new MauronCode_dataList<char>(){'\u0020','\u00A0','\u1680','\u180E','\u2000','\u2001','\u2002','\u2003','\u2004','\u2005','\u2006','\u2007','\u2008','\u2009','\u200A','\u200B','\u200C','\u200D','\u202F','\u205F','\u2060','\u3000','\u3000','\uFEFF'};
		public static MauronCode_dataList<char> Tabs = new MauronCode_dataList<char>(){'\u0009'};
		public new static MauronCode_dataList<char> WordBreaks=new MauronCode_dataList<char>() { '-', '+', '*', '-','/','=','<','>','@','[',']','{','}','_','|','~','«','»','|','(',')' };
		public static MauronCode_dataList<char> LineBreaks=new MauronCode_dataList<char>() {'\u0058','\u000B','\u000C','\u000D','\u0085','\u2028','\u2029'};
		public static MauronCode_dataList<char> WordEnders=new MauronCode_dataList<char>(){'.',',',':',';','!','?'};
		public static char ZeroWidth='\u200B'; //zero width character
		public new static char Empty='\u0000'; //null

		private MauronCode_dataTree<string,char> Defaults = new MauronCode_dataTree<string,char>(EncodedCharacters);

		//parse a string into a text
		public TextUnit_text ParseString(string str) {
			TextUnit_text text = new TextUnit_text(new TextUtility_UTF8(this),this);
			return text;
		}
		
		public static bool IsLineBreak(char c) {
			return LineBreaks.ContainsValue(c);
		}
		public static bool IsWordEnd(char c) {
			return WordEnders.ContainsValue(c);
		}
		public static bool IsWhiteSpace(char c){
			return WhiteSpaces.ContainsValue(c);
		}
		public static bool IsWordBreak(char c) {
			return WordBreaks.ContainsValue(c);
		}

		public static string[] EncodedCharacters = new string[]{
			"whiteSpace",
			"tab",
			"wordBreak",
			"lineBreak",
			"wordEnd",
			"zeroWidth",
			"empty"
		};
		public static char[] VALUES_EncodedCharacters = new char[]{
			WhiteSpaces.FirstElement,
			Tabs.FirstElement,
			WordBreaks.FirstElement,
			LineBreaks.FirstElement,
			WordEnders.FirstElement,
			ZeroWidth,
			Empty
		};		
	}

	public class TextUtility_UTF8:TextUtility_text {
		
		private TextEncoding_UTF8 UTILITY_encoding;

		//constructor
		public TextUtility_UTF8(TextEncoding_UTF8 encoder) {
			UTILITY_encoding = encoder;
		}

	}
}
