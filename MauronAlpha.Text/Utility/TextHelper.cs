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

		public static TextComponent_text ParseString(string str) {
			TextComponent_text text=new TextComponent_text();
			MauronCode_dataList<char> characters = new MauronCode_dataList<char>(str.ToCharArray());
			
			TextComponent_line line = new TextComponent_line(text,new TextContext(1));
			text.AddLine(line);

			TextComponent_word word = new TextComponent_word(line,new TextContext(1,1));
			line.AddWord(word);

			for(int i=0; i<characters.Count;i++){
				line = text.LastLine;
				char c = characters.Value(i);
				word = line.LastWord;

				#region last word ended line, start new

				if (word.EndsLine) {

					line = new TextComponent_line (
						text,
						new TextContext (
							text.LineCount,
							0,
							0
						)
					);
					text.AddLine(line);

					word = new TextComponent_word (
						line,
						new TextContext(
							text.LineCount-1,
							line.WordCount,
							0
						)
					);
					line.AddWord(word);
				}
				#endregion

				#region last word is complete start new

				if(word.IsComplete){
					word=new TextComponent_word(
						line,
						new TextContext(
							text.LineCount-1,
							line.WordCount,
							0
						)
					);
					line.AddWord(word);
				}

				#endregion

				TextComponent_character ch = new TextComponent_character (
					                             word,
					                             new TextContext (
													text.LineCount-1,
													line.WordCount-1,
													word.CharacterCount
					                             ),
												c
				                             );
				word.AddCharacter(ch);
			}

			return text;			
		}
		public static bool IsLineBreak(char c) {
			return LineBreaks.ContainsValue(c);
		}
		public static bool IsWordEnd(char c) {
			return WordEnders.ContainsValue(c);
		}
	
	}
}
