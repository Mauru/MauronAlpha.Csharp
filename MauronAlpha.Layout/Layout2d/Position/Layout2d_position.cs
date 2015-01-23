using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Layout.Layout2d.Position {

	//A Wrapper for The Geometry class
	public class Layout2d_position:Layout2d_component {
		
		//constructor
		public Layout2d_position ()	: base() {}
		public Layout2d_position (Vector2d position)
			: base() {
			V_position = position;
		}

		public Layout2d_position Instance { 
			get {
				return new Layout2d_position(AsVector);
			}
		}
		public Layout2d_position SetIsReadOnly(bool b_isReadOnly) {
			B_isReadOnly=b_isReadOnly;
			return this;
		}

		public string AsString {
			get { return V_position.AsString; }
		}

		//The actual position
		private Vector2d V_position = new Vector2d(0,0);
		public Vector2d AsVector { 
			get { 
				return V_position; 
			} 
		}

		//Can the position change?
		private bool B_isStatic = false;
		public bool IsStatic {
			get {
				return B_isStatic;
			}
		}
		
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		
	}

}
