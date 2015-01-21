using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.HandlingErrors;

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
		public Layout2d_unitReference Anchor {
			get {
				if(UNIT_anchor == null)
					throw NullError("Anchor can not be null!,(Anchor)",this,typeof(Layout2d_unit));
				return UNIT_anchor.AsReference;
			}
		}
		
		public Layout2d_position SetAnchor(Layout2d_unitReference unit){
			if(IsReadOnly)
				throw Error("Is Protected!,(SetAnchor)",this,ErrorType_protected.Instance);
			UNIT_anchor=unit;
			return this;
		}
		public Layout2d_position Instance { 
			get {
				if(HasAnchor)
					return new Layout2d_position(Anchor, AsVector, IsStatic);
				return new Layout2d_position(AsVector,IsStatic);
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
		private Vector2d V_position=new Vector2d(0,0);
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
		
		public bool HasAnchor {
			get {
				return UNIT_anchor!=null;
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
