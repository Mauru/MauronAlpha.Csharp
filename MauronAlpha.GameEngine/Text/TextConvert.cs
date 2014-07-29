using System.Collections.Generic;

namespace MauronAlpha.GameEngine.Text {

	//a class that handles textconversion
	public class TextConvert : MauronCode_utility {
		//converts a stack of characters into a string
		public static string WordToText (Stack<char> word) {
			string r="";
			foreach( char c in word.ToArray() ) {
				r+=c;
			}
			return r;
		}

		//a basic lexer that divides a string into words and characters
		public static Stack<Stack<char>> StringToCharStack (string text) {
			Stack<Stack<char>> words=new Stack<Stack<char>>();
			char[] a_txt=text.ToCharArray(0, text.Length);
			Stack<char> word=new Stack<char>();
			foreach( char i in a_txt ) {
				if( word.Count>0&&word.Peek()==' '&&i==' ' ) {
					//ignore multiple white spaces
				}
				else {
					word.Push(i);
				}
				if( TextDisplay.WordSplitCharacters.Contains(i) ) {
					words.Push(word);
					word=new Stack<char>();
				}
			}
			if( word.Count>0 ) {
				words.Push(word);
			}
			return words;
		}

	}

}
