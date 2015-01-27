﻿using System.Collections.Generic;

using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Interfaces {
	
	public interface I_layoutRenderer {

		I_layoutRenderer DrawRegions (ICollection<string> regions, I_layoutModel layout);

	}

}
