using MauronAlpha.Events.Interfaces;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Collections;

namespace MauronAlpha.Layout.Layout2d.Interfaces {

	//A Layout object that can have children
	public interface I_layoutParent {

		I_eventHandler EventHandler { get; }

		Layout2d_unitReference Parent { get; }
		Layout2d_unitCollection Children { get; }
		bool IsReadOnly { get; }
		Layout2d_unitType UnitType { get; }

		Layout2d_unitReference AsReference { get; }

	}

}
