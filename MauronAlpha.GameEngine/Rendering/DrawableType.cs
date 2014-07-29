using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.GameEngine {

	//Placeable type
	public abstract class DrawableType : MauronCode_subtype {
		public abstract string Name { get; }
		public virtual bool IsDrawParent { get { return false; } }
		public virtual bool IsDrawEnd { get { return false; } }
		public virtual bool IsDrawLayer { get { return false; } }
		public virtual bool IsContent { get { return false; } }
	}

}
