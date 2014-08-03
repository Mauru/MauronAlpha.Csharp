using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.Text.Units {
	public class TextComponent_Line : TextComponent {

		//constructor
		public TextComponent_Line (MauronConsole console, int index) {
			SetIndex(index);
			SetWindow(console);
		}
		public TextComponent_Line (MauronConsole console, int index, string text) {
			SetIndex(index);
			SetWindow(console);
			SetText(text);
		}

		//is the line empty (unset)
		private bool B_isEmpty=true;
		public bool IsEmpty {
			get {
				return B_isEmpty;
			}
		}
		private TextComponent_Line SetIsEmpty (bool status) {
			B_isEmpty=status;
			return this;
		}

		//Clear the text
		public TextComponent_Line Clear ( ) {
			SetText(null);
			SetIsEmpty(true);
			return this;
		}


		//the Text
		private string STR_text;
		public string Text { get { return STR_text; } }
		public TextComponent_Line SetText (string text) {
			STR_text=text;
			SetIsEmpty(false);
			return this;
		}

		private MauronConsole M;
		public MauronCode Window {
			get {
				if( M==null ) {
					MauronCode.Error("Invalid output source (currently hardcoded to console)", this);
				}
				return M;
			}
		}
		public TextComponent_Line SetWindow (MauronConsole m) {
			M=m;
			return this;
		}

		public static TextComponent_Line New (MauronConsole window, string s) {
			return new TextComponent_Line(window, window.LineBuffer.NextIndex, s);
		}
		public static TextComponent_Line New (MauronConsole window) {
			return new TextComponent_Line(window, window.LineBuffer.NextIndex);
		}


		//the line number
		private int INT_index;
		public int Index { get { return INT_index; } }
		public TextComponent_Line SetIndex (int n) {
			INT_index=n;
			return this;
		}
	}

}
