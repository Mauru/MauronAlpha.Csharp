using MauronAlpha.HandlingData;
using MauronAlpha.TextProcessing.Units;

namespace MauronAlpha.TextProcessing.Collections {

	public class Lines:MauronCode_dataList<Line> {

		public Lines(): base() {}
		public Lines(Line l): base() {
				Add(l);
		}
		public Lines(MauronCode_dataList<Line> data): this() {
			base.AddValuesFrom(data);
		}
		public Lines(Words data): this() {
			if (data.IsEmpty) return;
			Line l = new Line();
			Add(l);
			int count = data.Count;
			foreach (Word w in data) {
				if (w.IsEmpty) { 
					//ignore empty words
				}					
				else if (l.IsEmpty)
					l.TryAdd(w);
				else if (w.IsParagraphBreak) {
					if (!l.HasLineBreak)
						l.TryAdd(Words.LineBreak);
					l = new Line(w);
					Add(l);
				}
				else if (l.IsParagraphBreak) {
					l = new Line(w);
					Add(l);
				}
				else if (l.HasLineBreak) {
					l = new Line(w);
					Add(l);
				}
				else
					l.TryAdd(w);
			}
		}
		public Lines(string text) : this(new Words(text)) {}

		public bool HasLineBreak {
			get {
				foreach (Line unit in this) {
					if (unit.HasLineBreak)
						return true;
				}
				return false;
			}
		}
		public bool HasParagraphBreak {
			get {
				foreach (Line unit in this)
					if (unit.IsParagraphBreak)
						return true;
				return false;
			}
		}

		public void ShiftIndex(int index, Paragraph parent) {
			MauronCode_dataList<Line> candidates = Range(index);
			foreach (Line unit in candidates)
				unit.SetParent(parent, unit.Index + 1);
		}
		public void UnShiftIndex(int index, Paragraph parent) {
			MauronCode_dataList<Line> candidates = Range(index);
			foreach (Line unit in candidates)
				unit.SetParent(parent, unit.Index - 1);
		}
		public void EndAll() {
			foreach (Line l in this)
				if (!l.HasLineOrParagraphBreak)
					l.TryEnd();
		}

		public static Line LineBreak {
			get {
				return new Line(Words.LineBreak);
			}
		}
		public static Line ParagraphBreak {
			get {
				return new Line(Words.ParagraphBreak);
			}
		}

		public new Lines Reverse() {
			base.Reverse();
			return this;
		}
		public new Lines Range(int index) {
			return new Lines(base.Range(index));
		}
		public new Lines Range(int start,int end) {
			return new Lines(base.Range(start,end));
		}

		//tests if this is a self-contained paragraph and fixes if necessary - returns true if fixes were needed
		public bool FixAsContainedParagraph() {
			if (IsEmpty)
				return false;
			if (!LastElement.IsParagraphBreak)
				return false;
			int count = Count;
			if (count == 1) {
				Line newLine = Lines.LineBreak;
				base.InsertValueAt(0, newLine);
				return true;
			}
			Line test = base.Value(count - 2);
			if (!test.HasLineBreak) {
				test.TryAdd(Words.LineBreak);
				return true;
			}
			return false;
		}

	}
}
