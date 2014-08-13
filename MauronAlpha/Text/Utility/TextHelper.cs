using System.Collections.Generic;
using MauronAlpha.Text.Units;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Utility {
	
	//A class that helps with Text Operations
	public class TextHelper:MauronCode_utility {
		
		public static MauronCode_dataList<char> WhiteSpaces= new MauronCode_dataList<char>();
		public static MauronCode_dataList<char> WordBreaks=new MauronCode_dataList<char>();
		public static MauronCode_dataList<char> LineBreaks=new MauronCode_dataList<char>();

		public static TextComponent_text ParseString(string str) {
			TextComponent_text r=new TextComponent_text();
			ICollection<char> characters = str.ToCharArray();
			foreach(char c in characters){
				r.AddCharacter(new TextComponent_character(c));
			}
			return r;			
		}
	
	}
}
