using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.Layout.Layout2d.Position {

	//A Wrapper for The Geometry class
	public class Layout2d_position:Layout2d_component {
		
		//constructor
		public Layout2d_position (Layout2d_unit anchor)	: base() {
			UNIT_anchor=anchor;
		}
		//constructor
		public Layout2d_position (Layout2d_unit anchor, Vector2d position, bool b_isStatic)
			: base() {
			B_isStatic = b_isStatic;
			V_position = position;
			UNIT_anchor=anchor;
		}
		public Layout2d_position (Vector2d position, bool b_isStatic)
			: base() {
			B_isStatic=b_isStatic;
			V_position=position;
		}

		//The "offset" relative to the layout parent position
		private Layout2d_unit UNIT_anchor;

		//The actual position
		private Vector2d V_position=new Vector2d(0,0);

		//Can the position change?
		private bool B_isStatic = false;
		public bool IsStatic {
			get {
				return B_isStatic;
			}
		}
	
	}

}
