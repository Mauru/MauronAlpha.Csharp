using System.Collections.Generic;

using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Interfaces {
	
	public interface I_layoutRenderer {

		I_layoutRenderer Draw( I_layoutUnit source, I_layoutModel layout );

	}

}
