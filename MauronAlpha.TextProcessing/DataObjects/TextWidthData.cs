using MauronAlpha.HandlingData;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.Units;

namespace MauronAlpha.TextProcessing.DataObjects {
	public class TextWidthData : MauronCode_dataIndex<Lines> {

		public TextWidthData(Text text): base() {
			Lines ll = text.Lines;
			foreach (Line l in ll) {
				int x = l.VisualLength;
				Lines xl = null;
				if (!TryKey(x, ref xl))
					SetValue(x, new Lines(l));
				else
					xl.Add(l);
			}
		}
	
	}
}
