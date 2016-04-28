using MauronAlpha.TextProcessing.DataObjects;

namespace MauronAlpha.TextProcessing.Units {
	
	//Defines a character in string analyzations
	public class Character:TextUnit {

		public TextContext Context {
			get {
				return Parent.Context.Copy.SetCharacter(Index);
			}
		}
		public TextContext Edit {
			get {
				if (IsEmpty)
					return Context;
				if(IsParagraphBreak) {
					Paragraph p = Paragraph;
					return p.Context.Copy.SetParagraph(p.Index + 1);
				}
				if (IsLineBreak) {
					Line l = Line;
					return l.Context.Copy.SetLine(l.Index + 1);
				}
				if (IsUtility) {
					Word w = Parent;
					return w.Context.Copy.SetWord(w.Index + 1);
				}
				return Context.Copy.SetCharacter(Index + 1);
			}
		}
		
		public Encoding Encoding {
			get {
				if (HasParent)
					return Parent.Encoding;
				return MauronAlpha.TextProcessing.DataObjects.Encoding.Instance;
			}
		}

		public Character() : base() { }
		public Character(char ch) : base() {
			if (Encoding.IsParagraphBreak(ch))
				IsParagraphBreak = true;
			else if (Encoding.IsLineBreak(ch))
				IsLineBreak = true;
			else if (Encoding.IsTab(ch))
				IsTab = true;
			else if (Encoding.IsWhiteSpace(ch))
				IsWhiteSpace = true;
			else
				Symbol = ch;
		}

		public char Symbol;

		public string AsString {
			get {
				string result = "";
				if (IsWhiteSpace)
					return result += Encoding.WhiteSpace;
				if (IsLineBreak)
					return result += Encoding.LineBreak;
				if (IsParagraphBreak)
					return result += Encoding.ParagraphBreak;
				if (IsTab) {
					if (Encoding.TreatTabAsWhiteSpaces)
						return result += Encoding.TabAsWhiteSpaces;
					else
						return result += Encoding.Tab;
				}
				if(IsEmpty)
					return "";
				return ""+Symbol;
			}
		}
		public string AsVisualString {
			get {
				string result = "";
				if (IsWhiteSpace)
					return result += Encoding.VisualWhiteSpace;
				if (IsLineBreak)
					return result += Encoding.VisualLineBreak;
				if (IsParagraphBreak)
					return result += Encoding.VisualParagraphBreak;
				if (IsTab) {
					if (Encoding.TreatTabAsWhiteSpaces)
						return result += Encoding.TabAsVisualWhiteSpaces;
					else
						return result += Encoding.Tab;
				}
				if (IsEmpty)
					return "";
				return "" + Symbol;
			}
		}

		public int Index;
		public int VisualLength {
			get {
				if (IsWhiteSpace)
					return 1;
				if (IsTab)
					return Encoding.TabAsWhiteSpaceNo;
				if (IsLineBreak)
					return 0;
				if (IsParagraphBreak)
					return 0;
				if (IsEmpty)
					return 0;
				return 1;
			}
		}

		public bool HasParent {
			get {
				if (DATA_parent == null)
					return false;
				return true;
			}
		}
		public bool IsWhiteSpace = false;
		public bool IsTab = false;
		public bool IsLineBreak = false;
		public bool IsParagraphBreak = false;
		public bool IsEmpty = false;
		public bool IsUtility {
			get {
				if(IsWhiteSpace || IsTab || IsLineBreak || IsParagraphBreak)
					return true;
				return false;
			}
		}

		public Text Text {
			get { 
				return Paragraph.Parent; 
			}
		}

		public Paragraph Paragraph {
			get { 
				return Line.Parent;
			}
		}

		public Line Line {
			get {
				return Parent.Parent;
			}
		}

		private Word DATA_parent;
		public Word Parent {
			get {
				if (DATA_parent == null){
					DATA_parent = new Word();
					DATA_parent.TryAdd(this);
				}
				return DATA_parent;
			}
		}

		public Character Next {
			get {
				if (IsLineBreak)
					return Parent.Next.FirstChild;
				if (IsParagraphBreak)
					return Paragraph.Next.FirstCharacter;
				Character result = null;
				if (Parent.TryIndex(Index + 1, ref result))
					return result;
				return Parent.Next.FirstChild;
			}
		}
		public bool TryNext(ref Character unit) {
			if (!HasParent)
				return false;
			return Parent.TryIndex(Index + 1, ref unit);
		}
		public bool TryPrevious(ref Character unit) {
			if (!HasParent)
				return false;
			if (Index <= 0)
				return false;
			return Parent.TryIndex(Index - 1, ref unit);
		}
		public bool TryStepLeft(ref Character unit) {
			if (!HasParent)
				return false;
			if (Index > 0) {
				unit = Parent.ByIndex(Index - 1);
				return true;
			}
			Word w = null;
			if (!Parent.TryStepLeft(ref w))
				return false;
			unit = w.LastChild;
			return true;
		}
		public bool TryStepRight(ref Character unit) {
			if (!HasParent)
				return false;
			if (Index+1 < Parent.Count) {
				unit = Parent.ByIndex(Index + 1);
				return true;
			}
			Word w = null;
			if (!Parent.TryStepRight(ref w))
				return false;
			if (w.IsEmpty)
				return false;
			unit = w.FirstChild;
			return true;
		}
		public void SetParent(Word unit, int index) {
			DATA_parent = unit;
			Index = index;
		}

	}

}
