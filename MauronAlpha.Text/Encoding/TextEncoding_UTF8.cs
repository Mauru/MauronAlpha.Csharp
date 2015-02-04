using MauronAlpha.Text.Interfaces;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Encoding {
	
	public class TextEncoding_UTF8:TextEncoding {
		
		public override string Name { get { return "UTF8"; } }

		public static MauronCode_dataList<char> CHAR_whiteSpace = new MauronCode_dataList<char>() { '\u0020', '\u00A0', '\u1680', '\u180E', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200A', '\u200B', '\u200C', '\u200D', '\u202F', '\u205F', '\u2060', '\u3000', '\u3000', '\uFEFF' };
		public static MauronCode_dataList<char> CHAR_tab = new MauronCode_dataList<char>() { '\u0009' };
		public static MauronCode_dataList<char> CHAR_paragraph = new MauronCode_dataList<char>() { '\u2029' };
		public static MauronCode_dataList<char> CHAR_newLine=new MauronCode_dataList<char>() { '\u000A' };
		public static MauronCode_dataList<char> CHAR_wordBreak = new MauronCode_dataList<char>() { '-', '+', '*', '-', '/', '=', '<', '>', '@', '[', ']', '{', '}', '_', '|', '~', '«', '»', '|', '(', ')' };
		public static MauronCode_dataList<char> CHAR_lineBreak = new MauronCode_dataList<char>() { '\u0058', '\u000B', '\u000C', '\u000D', '\u0085', '\u2028', '\u2029' };
		public static MauronCode_dataList<char> CHAR_wordEnder = new MauronCode_dataList<char>() { '.', ',', ':', ';', '!', '?' };
		public static MauronCode_dataList<char> CHAR_zeroWidth = new MauronCode_dataList<char>() { '\u200B' };  //zero width character
		public static MauronCode_dataList<char> CHAR_null = new MauronCode_dataList<char>() { '\u0000' };  //null


		public override char EmptyCharacter {
			get {
				return CHAR_null.FirstElement;
			}
		}

		public override char WhiteSpace {
			get {
				return CHAR_whiteSpace.FirstElement;
			}
		}

		public override char Tab {
			get {
				return CHAR_tab.FirstElement;
			}
		}

		public override char Paragraph {
			get { 
				return CHAR_paragraph.FirstElement;
			}
		}

		public override char NewLine {
			get { 
				return CHAR_newLine.FirstElement; 
			}
		}

		public override char ZeroWidth {
			get {
				return CHAR_zeroWidth.FirstElement;
			}
		}
	}
}
