using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

namespace MauronAlpha.TextProcessing.Units {

	//Defines a line in string analyzations
	public class Line:TextUnit {

		public override TextUnitType UnitType {
			get { return TextUnitTypes.Line; }
		}

		//constructors
		public Line() : base(TextUnitTypes.Line) {	}
		public Line(Word unit):this() {
			Words.Add(unit);
			unit.SetParent(this, 0);
		}
		public Line(Words data):this() {
			Words.AddValuesFrom(data);
		}
		public Line(Character data)	: this() {
			Word newW = new Word(data);
			TryAdd(newW);
		}
		public Line(string data) : this(new Words(data)) { }

		public TextContext Context {
			get {
				return Parent.Context.Copy.SetLine(Index);
			}
		}
		public TextContext Start {
			get {
				return Context.Copy;
			}
		}
		public TextContext CountAsContext {
			get {
				int cw = 0;
				int cc = 0;
				foreach (Word w in Words) {
					cw++;
					cc += w.Count;
				}
				return new TextContext(1, 1, cw, cc);
			}
		}
		public TextContext End {
			get {
				if (IsEmpty)
					return Context.Copy;
				return LastChild.End;
			}
		}
		public TextContext Edit {
			get {
				if (IsEmpty)
					return Context.Copy;

				Paragraph p = Parent;

				if (IsParagraphBreak)
					return new TextContext(p.Index + 1, 0, 0, 0);

				if (HasLineBreak)
					return new TextContext(p.Index, Index + 1, 0, 0);

				Word w = LastChild;
				if (w.IsUtility)
					return new TextContext(p.Index, Index, w.Index+1, 0);

				return new TextContext(p.Index, Index, w.Index, w.Count);
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
				foreach (Word unit in Words)
					result += unit.AsString;
				return result;
			}
		}
		public string AsVisualString {
			get {
				string result = "";
				foreach (Word unit in Words)
					result += unit.AsVisualString;
				return result;
			}
		}

		public int Index;
		public int Count {
			get {
				return Words.Count;
			}
		}
		public int VisualLength {
			get {
				int result = 0;
				foreach (Character unit in Characters)
					result += unit.VisualLength;
				return result;
			}
		}
		public int CountCharacters() {
			int result = 0;
			foreach (Word child in Words)
				result += child.Count;
			return result;
		}

		//Relational Modifiers
		public void SetParent(Paragraph unit, int index) {
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
		public bool IsEmpty {
			get {
				return Words.IsEmpty;
			}
		}
		public bool IsParagraphBreak {
			get {
				if (IsEmpty)
					return false;
				return Words.LastElement.IsParagraphBreak;
			}
		}
		public bool HasLineBreak {
			get {
				return Words.HasLineBreak;
			}
		}
		public bool HasLineOrParagraphBreak {
			get {
				return Words.HasLineOrParagraphBreak;
			}
		}
		
		//Boolean Queries
		public bool Allows(Word word) {
			if (IsEmpty)
				return true;
			if (HasLineOrParagraphBreak)
				return false;
			return true;
		}
		public bool HasIndex(int index) {
			if (IsEmpty)
				return false;
			return Words.ContainsKey(index);
		}
		//Test for word to the left of index - if any end the paragraph or line return false
		public bool TestForLeftEnd(int index) {
			if (IsEmpty)
				return true;
			if (index < 0)
				index = 0;
			int count = Count;
			if (index > count)
				index = count;

			if (index == 0)
				return true;
			Word prev = null;
			if (HasIndex(index - 1))
				prev = ByIndex(index - 1);
			if (prev != null && prev.IsLineBreak || prev.IsParagraphBreak)
				return false;
			return true;
		}

		//Relational Queries with conditional save ("tries")	
		public bool TryBehind(ref Line unit) {
			if (!HasParent)
				return false;
			// in line
			if (Index > 0)
				return Parent.TryIndex(Index - 1, ref unit);

			Paragraph p = null;
			if (!Parent.TryPrevious(ref p))
				return false;

			unit = p.LastChild;

			return true;
		}
		public bool TryAhead(ref Line unit) {
			if (!HasParent)
				return false;

			//in paragraph
			if (Parent.TryIndex(Index + 1, ref unit))
				return true;

			Paragraph p = null;
			if (!Parent.TryNext(ref p))
				return false;

			if (p.IsEmpty)
				return false;
			unit = p.FirstChild;

			return true;
		}
		public bool TryPrevious(ref Line result) {
			if (Index == 0)
				return false;
			result = Parent.ByIndex(Index - 1);
			return true;
		}
		public bool TryNext(ref Line result) {
			if (DATA_parent == null)
				return false;
			if (!Parent.HasIndex(Index + 1))
				return false;
			result = Parent.ByIndex(Index + 1);
			return true;
		}
		public bool TryIndex(int index, ref Word result) {
			if (IsEmpty)
				return false;
			if (index < 0)
				return false;
			int count = Count;
			if (index >= count)
				return false;
			result = Words.Value(index);
			return true;
		}
		
		//Conditional Modifiers
		public bool TryAdd(Word word) {
			if (!Allows(word))
				return false;
			if (!IsEmpty && FirstChild.IsEmpty)
				Words.RemoveByKey(0);
			word.SetParent(this, Count);
			Words.Add(word);
			return true;
		}
		public bool TryAdd(Words data) {
			if (HasLineOrParagraphBreak)
				return false;
			foreach (Word w in data) {
				w.SetParent(this, Count);
				Words.Add(w);
			}
			return true;
		}
		public bool TryEnd() {
			if (!HasLineOrParagraphBreak)
				return TryAdd(Words.LineBreak);
			return false;
		}
		public bool TryInlineMergeAtIndex(int index) {
			if (IsEmpty)
				return false;
			Word target = null;
			Word next = null;

			bool foundTarget = TryIndex(index, ref target);
			bool foundNext = TryIndex(index, ref next);

			if (!foundTarget || !foundNext)
				return false;

			if (target.IsUtility || next.IsUtility)
				return false;

			Characters chars = next.Characters;
			Remove(next.Index);

			foreach (Character c in chars)
				target.TryAdd(c);

			return true;
		}
		public bool Remove(int index) {
			if (IsEmpty)
				return false;
			if (index < 0)
				index = 0;
			if (index >= Count)
				return false;
			Words.UnShiftIndex(index, this);
			Words.RemoveByKey(index);
			return true;
		}

		//Blind modifiers
		public Line Insert(Word word, int index) {
			if (index < 0)
				index = 0;
			int count = Count;
			if (index > count)
				index = count;
			if (IsEmpty) {
				Words.Add(word);
				word.SetParent(this, 0);
				return this;
			}

			Words.ShiftIndex(index, this);
			Words.InsertValueAt(index, word);
			word.SetParent(this, index);

			return this;
		}
		public Line Insert(Words data, int index) {
			if (index < 0)
				index = 0;
			int count = Count;
			if (index >= count)
				index = count;
			int offset = index;
			Words shift = Words.Range(index);
			foreach (Word w in data) {
				Words.InsertValueAt(offset, w);
				w.SetParent(this, offset);
				offset++;
			}
			count = data.Count;
			foreach (Word w in shift)
				w.SetParent(this, w.Index + count);
			return this;
		}
		public Line Add(Word w) {
			int count = Count;
			Words.Add(w);
			w.SetParent(this, count);
			return this;
		}
		public Line Add(Words ww) {
			int count = Count;
			int offset = 0;
			foreach (Word w in ww) {
				Words.Add(w);
				w.SetParent(this, count + offset);
				offset++;
			}
			return this;
		}

		//Relational Queries
		public Text Text {
			get {
				return Parent.Parent;
			}
		}

		private Paragraph DATA_parent;
		public Paragraph Parent {
			get {
				if (DATA_parent == null) {
					Paragraph unit = new Paragraph();
					DATA_parent = unit;
					unit.TryAdd(this);
				}
				return DATA_parent;
			}
		}

		public Words Words = new Words();
		//Split the line at index
		public Words SplitAt(int index) {
			if (index <= 0)
				return new Words(Words.ExtractRange(0));
			if (index >= Count)
				return new Words();
			return new Words(Words.ExtractRange(index));
		}

		public Word LastChild {
			get {
				if (IsEmpty)
					return NewChild;
				return Words.LastElement;
			}
		}
		public Word FirstChild {
			get {
				if(IsEmpty)
					return NewChild;
				return Words.FirstElement;
			}
		}
		public Word NewChild {
			get {
				Word unit = new Word();
				unit.SetParent(this, Count);
				Words.Add(unit);
				return unit;
			}
		}
		public Word ByIndex(int index) {
			if (IsEmpty)
				return NewChild;
			if (index <= 0)
				return FirstChild;
			if (index >= Count)
				return LastChild;
			return Words.Value(index);
		}

		public Words ChildrenAfterIndex(int index) {
			return Words.Range(index + 1);
		}
		public Words ChildrenBeforeIndex(int index) {
			return Words.Range(0, index);
		}
		public Words ChildrenByRange(int start, int end) {
			if (start < 0)
				start = 0;
			if (end < 0)
				end = 0;
			return Words.Range(start, end);
		}
		public Words WordsUntilLineBreak {
			get {
				Words result = new Words();
				foreach (Word u in Words) {
					if (!u.IsLineOrParagraphBreak)
						result.Add(u);
					else
						break;
				}
				return result;
			}
		}

		public Line Next {
			get {
				if (LastChild.IsParagraphBreak)
					return Parent.Next.FirstChild;
				Line result = null;
				bool found = Parent.TryIndex(Index + 1, ref result);
				if (found)
					return result;
				if (IsParagraphBreak)
					return Parent.Next.FirstChild;
				return Parent.NewChild;

			}
		}

		public Lines LookRight {
			get {
				if (!HasParent)
					return new Lines();
				return Parent.ChildrenAfterIndex(Index);
			}
		}
		public Lines LookLeft {
			get {
				if (!HasParent)
					return new Lines();
				return Parent.ChildrenBeforeIndex(Index);
			}
		}

		public Characters Characters {
			get {
				Characters result = new Characters();
				foreach (Word unit in Words)
					result.AddValuesFrom(unit.Characters);
				return result;
			}
		}

		public Character CharacterByIndex(int index) {
			if (IsEmpty)
				return FirstCharacter;
			if (index < 0)
				index = 0;
			if (index >= CountCharacters())
				return LastCharacter;

			int offset = 0;
			foreach (Word child in Words) {
				int count = Count;
				if (offset + count >= index)
					return child.ByIndex(index - offset);
				offset += count;
			}
			return LastCharacter;
		}
		public Character LastCharacter {
			get {
				return LastChild.LastChild;
			}
		}
		public Character FirstCharacter {
			get {
				return FirstChild.FirstChild;
			}
		}

	}

	public class TextUnitType_line : TextUnitType {
		public override string Name {
			get {
				return "Line";
			}
		}
		public static TextUnitType_line Instance {
			get {
				return new TextUnitType_line();
			}
		}
	}


}
