using MauronAlpha.TextProcessing.DataObjects;
using MauronAlpha.TextProcessing.Collections;

namespace MauronAlpha.TextProcessing.Units {
	
	//Defines a character in string analyzations
	public class Character:TextUnit {

		public override TextUnitType UnitType {
			get {
				return TextUnitTypes.Character;
			}
		}

		//constructors
		public Character() : base(TextUnitTypes.Character) { }
		public Character(char ch) : this() {
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

		public TextContext Context {
			get {
				return Parent.Context.Copy.SetCharacter(Index);
			}
		}
		public TextContext Edit {
			get {
				if (IsEmpty)
					return Context;

				Paragraph p = Paragraph;

				if(IsParagraphBreak)
					return new TextContext(p.Index + 1, 0, 0, 0);

				Line l = Line;
				if (IsLineBreak)	
					return new TextContext(p.Index,l.Index+1,0,0);

				Word w  = Parent;
				if (IsUtility)
					return new TextContext(p.Index, l.Index, w.Index + 1, 0);

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

		//Conditional Queries with boolean saves ("Tries")
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
		public bool TryBehind(ref Character unit) {
			if (!HasParent)
				return false;

			// in word
			if (Index > 0)
				return Parent.TryIndex(Index - 1, ref unit);

			Word w = null;
			if (!Parent.TryBehind(ref w))
				return false;

			unit = w.LastChild;
			return true;
		}
		public bool TryAhead(ref Character unit) {
			if (!HasParent)
				return false;

			//in line
			if (Parent.TryIndex(Index + 1, ref unit))
				return true;

			Word w = null;
			if (!Parent.TryAhead(ref w))
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
			
		public Characters LookRight { 
			get {
				if (!HasParent)
					return new Characters();
				return Parent.ChildrenAfterIndex(Index);
			}
		}
		public Characters LookLeft {
			get {
				if (!HasParent)
					return new Characters();
				return Parent.ChildrenBeforeIndex(Index);
			}
		}
	}

	public class TextUnitType_character : TextUnitType {
		public override string Name {
			get { return "Character"; }
		}
		public static TextUnitType_character Instance {
			get {
				return new TextUnitType_character();
			}
		}
	}

}
