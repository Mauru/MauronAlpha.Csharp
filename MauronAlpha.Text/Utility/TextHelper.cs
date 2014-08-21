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
			TextComponent_text text=new TextComponent_text();
			ICollection<char> characters = str.ToCharArray();
			
			MauronCode_dataList<TextComponent_word> words=new MauronCode_dataList<TextComponent_word>();
			
			MauronCode_dataList<TextComponent_line> lines=new MauronCode_dataList<TextComponent_line>();

			TextComponent_line line = new TextComponent_line(text,new TextContext(1));
			lines.AddValue(line);

			TextComponent_word word = new TextComponent_word(line,new TextContext(1,1));
			line.AddWord(word);

			for(int i=0; i<characters.Count;i++){
				
			}

			return text;			
		}
		public static bool IsLineBreak(char c) {
			return LineBreaks.ContainsValue(c);
		}

	}
}
