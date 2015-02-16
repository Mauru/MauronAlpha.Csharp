using MauronAlpha.Events;
using MauronAlpha.Layout.Layout2d.Context;

namespace MauronAlpha.Layout.Layout2d.Interfaces {

	//Interface for a class that controls layout
	public interface I_layoutController {

		string Name { get; }

		EventHandler EventHandler { get; }

		I_layoutRenderer Output { get; }

		Layout2d_context Context { get;	}
	}

}