using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.Layout.Layout2d.Context {
	
	//A class describing A LayoutUnit's Position in the Action/Reaction-dependency tree	
	public class Layout2d_context:Layout2d_component {

		//constructors
		public Layout2d_context() {}
		public Layout2d_context (Layout2d_unitReference anchor)	: this() {
			LAYOUT_anchor = anchor;
			LAYOUT_position = new Layout2d_position(anchor);
			LAYOUT_size = new Layout2d_size ();
		}
		public Layout2d_context(Layout2d_unitReference anchor, Vector2d position, Vector2d size, bool b_isStatic):this() {
			LAYOUT_position=new Layout2d_position(anchor, position, b_isStatic);
			LAYOUT_size = new Layout2d_size(size, b_isStatic);
			LAYOUT_anchor = anchor;
			B_isStatic = b_isStatic;
		}
		public Layout2d_context (Vector2d position, Vector2d size, bool b_isStatic)	: this() {
			LAYOUT_position=new Layout2d_position(position, b_isStatic);
			LAYOUT_size=new Layout2d_size(size, b_isStatic);
			B_isStatic=b_isStatic;
		}

		//The component this context is attached to
		private Layout2d_unitReference LAYOUT_anchor;
		public Layout2d_unitReference Anchor {
			get {
				if(LAYOUT_anchor==null){
					throw NullError("Anchor can not be null!,(Anchor)",this,typeof(Layout2d_unitReference));
				}
				return LAYOUT_anchor;
			}
		}

		//Boolean states
		public bool HasAnchor {
			get {
				return (LAYOUT_anchor!=null);
			}
		}
		private bool B_isStatic = true;
		public bool IsStatic { 
			get {
				return B_isStatic;
			}
		}

		private Layout2d_position LAYOUT_position;
		
        private Layout2d_size LAYOUT_size;
        public Layout2d_size Size { get { return LAYOUT_size.Instance.SetIsReadOnly(true); } }

		private Layout2d_constraint LAYOUT_limit;
		public Layout2d_constraint Constraint {
			get {
				return LAYOUT_limit.Instance.SetIsReadOnly(true);
			}
		}

		//Methods
        public Layout2d_context SetSize(Layout2d_size size) {
            LAYOUT_size = size;
            return this;
        }
		public Layout2d_context SetAnchor(Layout2d_unitReference unit){
			LAYOUT_anchor = unit;
			return this;
		}
		public Layout2d_context SetConstraint(Vector2d vector){
			LAYOUT_limit = new Layout2d_constraint (vector);
			return this;
		}
        public Layout2d_context SetConstraint(Layout2d_constraint constraint)
        {
            LAYOUT_limit = constraint;
            return this;
        }
	}

}