using System.Collections.Generic;
using MauronAlpha.Text.Units;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Utility {
	
	//A class that helps with Text Operations
	public class TextHelper:MauronCode_utility {
		
		public static MauronCode_dataList<char> WhiteSpaces= new MauronCode_dataList<char>(){'\u0020','\u00A0','\u1680','\u180E','\u2000','\u2001','\u2002','\u2003','\u2004','\u2005','\u2006','\u2007','\u2008','\u2009','\u200A','\u200B','\u200C','\u200D','\u202F','\u205F','\u2060','\u3000','\u3000','\uFEFF'};
		public static MauronCode_dataList<char> Tabs = new MauronCode_dataList<char>(){'\u0009'};
		public static MauronCode_dataList<char> WordBreaks=new MauronCode_dataList<char>() { '-', '+', '*', '-','/','=','<','>','@','[',']','{','}','_','|','~','«','»','|','(',')' };
		public static MauronCode_dataList<char> LineBreaks=new MauronCode_dataList<char>() {'\u0058','\u000B','\u000C','\u000D','\u0085','\u2028','\u2029'};
		public static MauronCode_dataList<char> WordEnders=new MauronCode_dataList<char>(){'.',',',':',';','!','?'};

		//parse a string into a text
		public static TextComponent_text ParseString(string str) {
			TextComponent_text text=new TextComponent_text();
			return text.AddString(str);			
		}
		
		public static bool IsLineBreak(char c) {
			return LineBreaks.ContainsValue(c);
		}
		public static bool IsWordEnd(char c) {
			return WordEnders.ContainsValue(c);
		}
	}
}
