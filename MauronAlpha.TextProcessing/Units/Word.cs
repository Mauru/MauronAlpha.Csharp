using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

namespace MauronAlpha.TextProcessing.Units {

	//Defines a "word" in string analyzations - this can also mean a Utility character
	public class Word:TextUnit {

		public override TextUnitType UnitType {
			get { 
				return TextUnitTypes.Word;
			}
		}

		//constructors
		public Word() : base(TextUnitTypes.Word) { }
		public Word(Character unit)	: this() {
			Characters.Add(unit);
			unit.SetParent(this, 0);
		}
		public Word(MauronAlpha.HandlingData.MauronCode_dataStack<Character> cc): this() {
			while(!cc.IsEmpty) {
				Character c = cc.Pop;
				Add(c);
			}

		}
		public Word(Characters data):this() {
			data.Reverse();
			foreach (Character unit in data)
				TryAdd(unit);
		}

		public TextContext Context {
			get {
				return Parent.Context.Copy.SetWord(Index);
			}
		}
		public TextContext CountAsContext {
			get {
				return new TextContext(1, 1, 1, Count);
			}
		}
		public TextContext Start {
			get {
				return Context.Copy;
			}
		}
		public TextContext End {
			get {
				if (IsEmpty)
					return Start;
				return LastChild.Context.Copy;
			}
		}
		public TextContext Edit {
			get {
				if (IsEmpty)
					return Context.Copy;

				Paragraph p = Paragraph;
				if (IsParagraphBreak)
					return new TextContext(p.Index + 1, 0, 0, 0);

				Line l = Parent;
				if (IsLineBreak)
					return new TextContext(p.Index, l.Index + 1, 0, 0);

				if (IsUtility)
					return new TextContext(p.Index, l.Index, Index + 1,0);

				return new TextContext(p.Index, l.Index, Index, Count);
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
				foreach (Character unit in Characters)
					result += unit.AsString;
				return result;
			}
		}
		public string AsVisualString {
			get {
				string result = "";
				foreach (Character unit in Characters)
					result += unit.AsVisualString;
				return result;
			}
		}

		public int Index;
		public int Count {
			get {
				return Characters.Count;
			}
		}

		public void SetParent(Line unit, int index) {
			DATA_parent = unit;
			Index = index;
		}

		//Boolean properties
		public bool HasParent {
			get {
				if (DATA_parent == null)
					return false;
				return true;
			}
		}
		public bool IsLineOrParagraphBreak {
			get {
				foreach (Character unit in Characters)
					if (unit.IsLineBreak || unit.IsParagraphBreak)
						return true;
				return false;
			}
		}
		public bool IsEmpty {
			get {
				return Characters.IsEmpty;
			}
		}
		public bool IsUtility {
			get {
				Character test = null;
				if (!TryIndex(0, ref test))
					return false;
				return test.IsUtility;
			}
		}
		public bool IsParagraphBreak {
			get {
				if (IsEmpty)
					return false;
				return LastChild.IsParagraphBreak;
			}
		}
		public bool IsLineBreak {
			get {
				if (IsEmpty)
					return false;
				return LastChild.IsLineBreak;
			}
		}
		public bool IsWhiteSpace {
			get {
				if (IsEmpty)
					return false;
				return LastChild.IsWhiteSpace;
			}
		}
		public bool IsTab {
			get {
				if (IsEmpty)
					return false;
				return LastChild.IsTab;
			}
		}

		public bool Allows(Character character) {
			if (IsEmpty)
				return true;
			if (LastChild.IsUtility || character.IsUtility)
				return false;
			return true;
		}

		//Relational Queries with conditional save ("tries")
		public bool TryPrevious(ref Word result) {
			if (Index == 0)
				return false;
			result = Parent.ByIndex(Index - 1);
			return true;
		}
		public bool TryNext(ref Word result) {
			if (DATA_parent == null)
				return false;
			if (!Parent.HasIndex(Index + 1))
				return false;
			result = Parent.ByIndex(Index + 1);
			return true;
		}
		public bool TryIndex(int index, ref Character result) {
			if (index < 0)
				return false;
			if (index >= Count)
				return false;
			result = Characters.Value(index);
			return true;
		}
		public bool TryBehind(ref Word unit) {
			if (!HasParent)
				return false;

			// in line
			if (Index > 0)
				return Parent.TryIndex(Index - 1, ref unit);

			// in text
			Line l = null;
			if (!Parent.TryBehind(ref l))
				return false;
			unit = l.LastChild;

			return true;
		}
		public bool TryAhead(ref Word unit) {
			if (!HasParent)
				return false;

			//in line
			if (Parent.TryIndex(Index + 1, ref unit))
				return true;

			Line l = null;
			if (!Parent.TryAhead(ref l))
				return false;

			if (l.IsEmpty)
				return false;

			unit = l.FirstChild;
			return true;
		}

		//Conditional Modifiers
		public bool TryAdd(Character character) {
			if (!Allows(character))
				return false;
			if (!IsEmpty && FirstChild.IsEmpty)
				Characters.RemoveByKey(0);
			character.SetParent(this, Count);
			Characters.Add(character);
			return true;
		}
		public bool TryAdd(Character character, int index) {
			if (!Allows(character))
				return false;
			if (!IsEmpty && FirstChild.IsEmpty)
				Characters.RemoveByKey(0);
			if (index < 0)
				index = 0;
			int count = Count;
			if (index > count)
				index = count;

			if (index < count)
				Characters.ShiftIndex(index, this);
			Characters.InsertValueAt(index, character);
			character.SetParent(this, index);
			return true;
		}
		public bool Remove(int index) {
			if (index < 0)
				index = 0;
			if (IsEmpty)
				return false;
			if (index >= Count)
				return false;
			Characters.UnShiftIndex(index,this);
			Characters.RemoveByKey(index);
			return true;
		}

		//Blind Modifiers
		public Word Add(Character c) {
			int count = Count;
			Characters.Add(c);
			c.SetParent(this, count);
			return this;
		}
		public Word Add(Characters cc) {
			int count = Count;
			int offset = 0;
			foreach (Character c in cc) {
				Characters.Add(c);
				c.SetParent(this, count + offset);
				offset++;
			}
			return this;
		}
		public Word Insert(Characters data, int index) {
			if (index < 0)
				index = 0;
			int count = Count;
			if (index >= count)
				index = count;
			int offset = index;
			Characters shift = Characters.Range(index);
			foreach (Character c in data) {
				Characters.InsertValueAt(offset, c);
				c.SetParent(this, offset);
				offset++;
			}
			count = data.Count;
			foreach (Character c in shift)
				c.SetParent(this, c.Index + count);
			return this;
		}
		public Word Insert(Character c, int index) {
			if (index < 0)
				index = 0;
			int count = Count;
			if (index > count)
				index = count;
			if (index < count)
				Characters.ShiftIndex(index, this);
			Characters.InsertValueAt(index, c);
			c.SetParent(this, index);
			return this;
		}

		//Relational Properties & Methods
		public Text Text {
			get {
				return Paragraph.Parent;
			}
		}

		public Paragraph Paragraph {
			get {
				return Parent.Parent;
			}
		}

		private Line DATA_parent;
		public Line Parent {
			get {
				if (DATA_parent == null) {
					DATA_parent = new Line();
					DATA_parent.TryAdd(this);
				}
				return DATA_parent;
			}
		}

		public Word Next {
			get {
				if (IsParagraphBreak)
					return Paragraph.Next.FirstWord;
				if (IsLineBreak)
					return Parent.Next.FirstChild;
				Word result = null;
				if (Parent.TryIndex(Index + 1, ref result))
					return result;
				return Parent.NewChild;
			}
		}

		public Words LookRight {
			get {
				if (!HasParent)
					return new Words();
				return Parent.ChildrenAfterIndex(Index);
			}
		}
		public Words LookLeft {
			get {
				if (!HasParent)
					return new Words();
				return Parent.ChildrenBeforeIndex(Index);
			}
		}

		public Character NewChild {
			get {
				Character unit = Characters.Empty;
				unit.SetParent(this, Count);
				Characters.Add(unit);
				return unit;
			}
		}
		public Character LastChild {
			get {
				if (IsEmpty)
					return NewChild;
				return Characters.LastElement;
			}
		}
		public Character FirstChild {
			get {
				if (IsEmpty)
					return NewChild;
				return Characters.FirstElement;
			}
		}
		public Character ByIndex(int index) {
			if (IsEmpty)
				return NewChild;
			if (index <= 0)
				return FirstChild;
			if (index >= Count)
				return LastChild;
			return Characters.Value(index);
		}
		public Character CreateEmptyCharacterAtIndex(int index) {
			if (IsEmpty)
				return NewChild;
			if (Index >= Count)
				return NewChild;

			Characters.ShiftIndex(index, this);
			Character newC = new Character();
			newC.SetParent(this, index);
			Characters.InsertValueAt(index, newC);
			return newC;
		}

		public Characters Characters = new Characters();
		public Characters CharactersUntilUtility {
			get {
				Characters result = new Characters();
				foreach(Character u in Characters) {
					if (u.IsUtility)
						return result;
					else
						result.Add(u);
				}
				return result;
			}
		}

		public Characters ChildrenAfterIndex(int index) {
			return Characters.Range(index + 1);
		}
		public Characters ChildrenBeforeIndex(int index) {
			return Characters.Range(0, index);
		}
		public Characters ChildrenByRange(int start, int end) {
			if (start < 0)
				start = 0;
			if (end < 0)
				end = 0;
			return Characters.Range(start, end);
		}

		//Split the line at index
		public Characters SplitAt(int index) {
			if (index <= 0)
				return new Characters(Characters.ExtractRange(0));
			if (index >= Count)
				return new Characters();
			Characters result = new Characters(Characters.ExtractRange(index));
			return result;
		}

	}

	public class TextUnitType_word : TextUnitType {
		public override string Name {
			get {
				return "Word";
			}
		}
		public static TextUnitType_word Instance {
			get {
				return new TextUnitType_word();
			}
		}
	}

}
