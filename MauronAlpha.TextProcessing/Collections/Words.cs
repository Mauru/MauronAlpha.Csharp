using MauronAlpha.HandlingData;
using MauronAlpha.HandlingData.Interfaces;
using MauronAlpha.TextProcessing.Units;

namespace MauronAlpha.TextProcessing.Collections {

	public class Words:MauronCode_dataList<Word> {

		public Words(): base() {}
		public Words(MauronCode_dataList<Word> data):this() {
			base.AddValuesFrom(data);
		}
		public Words(Characters data) : this() {
			if (data.IsEmpty) return;
			Word w = new Word();
			Add(w);
			int index = 0;
			foreach (Character c in data) {
				if (w.IsEmpty)
					w.TryAdd(c);
				else if (c.IsUtility) {
					w = new Word(c);
					Add(w);
				}
				else if (w.IsUtility) {
					w = new Word(c);
					Add(w);
				}
				else
					w.TryAdd(c);
				index++;
			}
		}
		public Words(string text): this(new Characters(text)) {}

		public void Add(string word) {
			Characters data = new Characters(word);
			if (data.IsEmpty) return;
			Word w = new Word();
			Add(w);
			int index = 0;
			foreach (Character c in data) {
				if (w.IsEmpty)
					w.TryAdd(c);
				else if (c.IsUtility) {
					w = new Word(c);
					Add(w);
				}
				else if (w.IsUtility) {
					w = new Word(c);
					Add(w);
				}
				else
					w.TryAdd(c);
				index++;
			}
		}

		public bool HasLineOrParagraphBreak {
			get {
				foreach (Word unit in this)
					if (unit.IsLineOrParagraphBreak)
						return true;
				return false;
			}
		}
		public bool HasLineBreak {
			get {
				foreach (Word unit in this)
					if (unit.IsLineBreak)
						return true;
				return false;
			}
		}
		public bool HasParagraphBreak {
			get {
				foreach (Word unit in this)
					if (unit.IsParagraphBreak)
						return true;
				return false;
			}
		}

		public void ShiftIndex(int index, Line parent) {
			MauronCode_dataList<Word> candidates = Range(index);
			foreach (Word unit in candidates)
				unit.SetParent(parent, unit.Index + 1);
		}
		public void UnShiftIndex(int index, Line parent) {
			MauronCode_dataList<Word> candidates = Range(index);
			foreach (Word unit in candidates)
				unit.SetParent(parent, unit.Index - 1);
		}

		public static Word LineBreak {
			get {
				Word unit = new Word(Characters.LineBreak);
				return unit;
			}
		}
		public static Word ParagraphBreak {
			get {
				Word unit = new Word(Characters.ParagraphBreak);
				return unit;
			}
		}
		public static Word Tab {
			get {
				Word unit = new Word(Characters.Tab);
				return unit;
			}
		}
		public static Word WhiteSpace {
			get {
				Word unit = new Word(Characters.WhiteSpace);
				return unit;
			}
		}

		public new Words Reverse() {
			base.Reverse();
			return this;
		}
		public new Words Range(int index) {
			return new Words(base.Range(index));
		}
	}
}
