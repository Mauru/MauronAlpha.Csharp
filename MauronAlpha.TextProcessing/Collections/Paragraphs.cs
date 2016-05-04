using MauronAlpha.HandlingData;
using MauronAlpha.TextProcessing.Units;

namespace MauronAlpha.TextProcessing.Collections {
	
	public class Paragraphs:MauronCode_dataList<Paragraph> {

		public Paragraphs() : base() { }
		public Paragraphs(MauronCode_dataList<Paragraph> data): this() {
			base.AddValuesFrom(data);
		}
		public Paragraphs(Lines data) : this() {
			if (data.IsEmpty) return;
			Paragraph p = new Paragraph();
			Add(p);
			foreach (Line l in data) {
				if (p.IsEmpty)
					p.TryAdd(l);
				else if (l.IsParagraphBreak) {
					p.TryAdd(l);
					p.FixParagraphEnd();
				}
				else if (p.HasParagraphBreak) {
					p = new Paragraph(l);
					Add(p);
				}
				else
					p.TryAdd(l);
			}
		}

		public Paragraphs(Characters data) {
			if (data.IsEmpty) return;
			Paragraph p = new Paragraph();
			Add(p);

			Words ww = new Words(data);
			Lines ll = new Lines(ww);

			foreach (Line l in ll) {
				if (p.IsEmpty)
					p.TryAdd(l);
				else if (l.IsParagraphBreak) {
					p.TryAdd(l);
					p.FixParagraphEnd();
				}
				else if (p.HasParagraphBreak) {
					p = new Paragraph(l);
					Add(p);
				}
				else
					p.TryAdd(l);
			}
		}

		public void ShiftIndex(int index, Text parent) {
			MauronCode_dataList<Paragraph> candidates = Range(index);
			foreach (Paragraph unit in candidates)
				unit.SetParent(parent, unit.Index + 1);
		}
		public void UnShiftIndex(int index, Text parent) {
			MauronCode_dataList<Paragraph> candidates = Range(index);
			foreach (Paragraph unit in candidates)
				unit.SetParent(parent, unit.Index - 1);
		}

		public new Paragraphs Range(int index) {
			return new Paragraphs(base.Range(index));
		}
		public new Paragraphs Range(int start, int end) {
			return new Paragraphs(base.Range(start, end));
		}

	}

}
