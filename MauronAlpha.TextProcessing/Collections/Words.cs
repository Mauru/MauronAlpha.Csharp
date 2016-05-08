using MauronAlpha.HandlingData;
using MauronAlpha.HandlingData.Interfaces;
using MauronAlpha.TextProcessing.Units;

namespace MauronAlpha.TextProcessing.Collections {

	public class Words:MauronCode_dataList<Word> {

		//Constructors
		public Words(): base() {}
		public Words(Word w) :this() {
			Add(w);
		}
		public Words(MauronCode_dataList<Word> data) :this() {
			base.AddValuesFrom(data);
		}
		public Words(Characters data) :this() {
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
		
		public Words(string text) :this(new Characters(text)) {}

		//Modifiers
		public Words Add(string word) {
			Characters data = new Characters(word);
			if (data.IsEmpty) 
				return this;

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
			return this;
		}
		public Words ShiftIndex(int index, Line parent) {
			MauronCode_dataList<Word> candidates = Range(index);
			foreach (Word unit in candidates)
				unit.SetParent(parent, unit.Index + 1);
			return this;
		}
		public Words UnShiftIndex(int index, Line parent) {
			MauronCode_dataList<Word> candidates = Range(index);
			foreach (Word unit in candidates)
				unit.SetParent(parent, unit.Index - 1);
			return this;
		}

		public new Words Reverse() {
			base.Reverse();
			return this;
		}

		//Boolean Conditionals
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

		//Queries
		public new Words Range(int index) {
			return new Words(base.Range(index));
		}
		public new Words Range(int start, int end) {
			return new Words(base.Range(start, end));
		}

		public static Word LineBreak {
			get {
				return new Word(Characters.LineBreak);
			}
		}
		public static Word ParagraphBreak {
			get {
				return new Word(Characters.ParagraphBreak);
			}
		}

	}

}
