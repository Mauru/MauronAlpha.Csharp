using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Collections;

namespace MauronAlpha.Layout.Layout2d.Interfaces {
	
	public interface I_layoutRenderer {

		I_layoutRenderer Clear();
		I_layoutRenderer HandleRenderRequest(Layout2d_renderChain chain);
		bool IsRendering { get; }
		bool NeedsRenderUpdate { get; }
	}

}
