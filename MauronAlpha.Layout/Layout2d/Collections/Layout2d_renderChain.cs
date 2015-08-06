using MauronAlpha.HandlingData;

using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Collections {
	public class Layout2d_renderChain:Layout2d_component {

		private MauronCode_dataList<I_layoutUnit> DATA_units = new MauronCode_dataList<I_layoutUnit>();

		public Layout2d_renderChain Add(I_layoutUnit unit) {
			DATA_units.Add(unit);
			return this;
		}

	}
}
