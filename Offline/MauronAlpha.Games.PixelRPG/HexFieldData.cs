using MauronAlpha.GameEngine;
using MauronAlpha.Geometry;
using MauronAlpha;

using System;

namespace MauronAlpha.Games.PixelRPG {

    //game specific information components for a hex
    public class HexFieldData : MauronCode_dataobject {
		public HexFieldStyle Style;
		public HexField Field;
		public HexFieldData Instance { get{
			return new HexFieldData(this);
		}
		}
		public HexFieldData() {}
		public HexFieldData ( HexFieldData instance) {
			Style=instance.Style;
		}

    }

}
