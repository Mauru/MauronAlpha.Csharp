﻿using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.DataObjects;

namespace MauronAlpha.TextProcessing.Units {
	
	public class Word:TextUnit {

		public Word() : base() { }
		public Word(Character unit)	: this() {
			Characters.Add(unit);
			unit.SetParent(this, 0);
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
				Character c = LastChild;
				if (c.IsEmpty)
					return c.Context.Copy;
				if (c.IsParagraphBreak)
					return new TextContext(Paragraph.Index + 1, 0, 0, 0);
				TextContext context = Context;
				if (c.IsLineBreak)
					return context.Copy.SetWord(0);
				if (c.IsUtility)
					return context.Copy.SetWord(Index + 1);
				return c.Context.Copy.Add(0, 0, 0, 1);
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
		public bool Insert(Characters data, int index) {
			if (IsUtility)
				return false;
			if (!IsEmpty && data.HasUtility)
				return false;
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
			return true;
		}
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
		public bool TryStepLeft(ref Word unit) {
			if (!HasParent)
				return false;

			if (Index > 0) {
				unit = Parent.ByIndex(Index - 1);
				return true;
			}

			//no previous line
			Line l = null;
			if (!Parent.TryStepLeft(ref l))
				return false;
			unit = l.LastChild;
			return true;
		}
		public bool TryStepRight(ref Word unit) {
			if (!HasParent)
				return false;
			if (Index + 1 < Parent.Count) {
				unit = Parent.ByIndex(Index + 1);
				return true;
			}
			Line l = null;
			if (!Parent.TryStepRight(ref l))
				return false;
			if (l.IsEmpty)
				return false;
			unit = l.FirstChild;
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

		public Characters Characters = new Characters();
		//Split the line at index
		public Characters SplitAt(int index) {
			if (index <= 0)
				return new Characters(Characters.RemoveByRange(0));
			if (index >= Count)
				return new Characters();
			return new Characters(Characters.RemoveByRange(index));
		}

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

		public void SetParent(Line unit, int index) {
			DATA_parent = unit;
			Index = index;
		}
	
	}

}
