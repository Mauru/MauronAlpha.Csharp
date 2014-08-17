using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text {

	public class TextContext:MauronCode_dataObject {

		//constructor
		public TextContext():base(DataType_maintaining.Instance) {}

		public TextContext(int line, int word, int character){
			SetLineOffset(line);
			SetWordOffset(word);
			SetCharacterOffset(character);
		}
		public TextContext (int line, int word) {
			SetLineOffset(line);
			SetWordOffset(word);
		}
		public TextContext (int line) {
			SetLineOffset(line);
		}

		#region line
		private int INT_lineOffset;
		public int LineOffset {
			get { return INT_lineOffset; }
		}
		public TextContext SetLineOffset(int n) {
			INT_lineOffset=n;
			return this;
		}
		public bool IsLine {
			get {
				return LineOffset!=null;
			}
		}
		public bool IsLineOnly {
			get {
				return !IsCharacter&&!IsWord&&IsLine;
			}
		}
		#endregion

		#region word
		private int INT_wordOffset;
		public int WordOffset {
			get { return INT_wordOffset; }
		}
		public TextContext SetWordOffset(int n){
			INT_wordOffset=n;
			return this;
		}
		public bool IsWord {
			get {
				return WordOffset!=null&&WordOffset!=0;
			}
		}
		public bool IsWordOnly {
			get {
				return !IsCharacter&&IsWord&&!IsLine;
			}
		}
		#endregion

		#region character
		private int INT_characterOffset;
		public int CharacterOffset {
			get { return INT_characterOffset; }
		}
		public TextContext SetCharacterOffset(int n) {
			INT_characterOffset=n;
			return this;
		}
		public bool IsCharacter {
			get {
				return CharacterOffset!=null&&CharacterOffset!=0;
			}
		}
		public bool IsCharacterOnly {
			get {
				return IsCharacter&&!IsWord&&!IsLine;
			}
		}
		#endregion

		public static TextContext End {
			get {
				return new TextContext(-1,-1,-1);
			}
		}
		public static TextContext Start {
			get {
				return new TextContext(0, 0, 0);
			}
		}

		public bool IsEmpty {
			get {
				return IsLine&&IsCharacter&&IsWord;
			}
		}

	}

}