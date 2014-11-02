using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.Layout.Layout2d.Position {

	//A Wrapper for The Geometry class
	public class Layout2d_position:Layout2d_component {
		
		//constructor
		public Layout2d_position (Layout2d_unit anchor)	: base() {
			UNIT_anchor=anchor;
		}

		//The "offset" relative to the layout parent position
		private Layout2d_unit UNIT_anchor;

		//The actual position
		private Vector2d V_position=new Vector2d(0,0);

		//Can the position change?
		public bool IsStatic {
			get {
				return false;
			}
		}
	}

}
