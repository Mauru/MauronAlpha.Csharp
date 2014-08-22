using System.Collections.Generic;
using MauronAlpha.Text.Units;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Text.Utility {
	
	//A class that helps with Text Operations
	public class TextHelper:MauronCode_utility {
		
		public static MauronCode_dataList<char> WhiteSpaces= new MauronCode_dataList<char>(){' '};
		public static MauronCode_dataList<char> WordBreaks=new MauronCode_dataList<char>() { '-', '+', '*', '-','/','=','<','>','@','[',']','{','}','_','|','~','«','»','|','(',')' };
		public static MauronCode_dataList<char> LineBreaks=new MauronCode_dataList<char>() {'¶'};
		public static MauronCode_dataList<char> WordEnders=new MauronCode_dataList<char>(){'.',',',':',';'};

		public static TextComponent_text ParseString(string str) {
			TextComponent_text text=new TextComponent_text();
			MauronCode_dataList<char> characters = new MauronCode_dataList<char>(str.ToCharArray());
			
			MauronCode_dataList<TextComponent_word> words=new MauronCode_dataList<TextComponent_word>();
			
			MauronCode_dataList<TextComponent_line> lines=new MauronCode_dataList<TextComponent_line>();

			TextComponent_line line = new TextComponent_line(text,new TextContext(1));
			lines.AddValue(line);

			TextComponent_word word = new TextComponent_word(line,new TextContext(1,1));
			line.AddWord(word);

			int charIndex = 0;
			int wordIndex = 0;

			for(int i=0; i<characters.Count;i++){
				line = lines.LastElement;
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

					word = new TextComponent_word (
						line,
						new TextContext(
							line.Context.LineOffset,
							line.WordCount,
							0
						)
					);
					line.AddWord(word);

					charIndex = 0;
					wordIndex = 0;

				}
				#endregion

				#region last word is complete start new

				if(word.IsComplete){
					word=new TextComponent_word(
						line,
						new TextContext(
							line.Context.LineOffset,
							line.WordCount,
							0
						)
					);
					line.AddWord(word);
					wordIndex++;
					charIndex=0;
				}

				#endregion

				TextComponent_character ch = new TextComponent_character (
					                             word,
					                             new TextContext (
						                             line.Context.LineOffset,
						                             wordIndex,
						                             charIndex
					                             ),
												c
				                             );
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
