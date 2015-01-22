using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;

using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Context {
	
	//A class describing A LayoutUnit's Position in the Action/Reaction-dependency tree	
	public class Layout2d_context:Layout2d_component {

		//constructors
		private Layout2d_context():base() {}

		public Layout2d_context(I_layoutUnit anchor):this() {
			LAYOUT_anchor = anchor.AsReference;	
		}

		internal Layout2d_context( Layout2d_context source ) : this() {
			LAYOUT_position = source.LAYOUT_position;
			LAYOUT_size = source.LAYOUT_size;
			B_isStatic = source.B_isStatic;
		}
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
		public Layout2d_context( int x, int y, int width, int height, bool b_isStatic):this( new Vector2d(x,y),new Vector2d(width,height), b_isStatic) {}

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

		//As String
		public string AsString { 
			get {
				return "{[ Position : "+Position.AsString+" ], [ Size : "+Size.AsString+" ]}";
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
		public Layout2d_position Position { get {
			if(LAYOUT_position == null)
				throw NullError("No position set!,(Position)",this,typeof(Vector2d));
			return LAYOUT_position.Instance.SetIsReadOnly(true);
		} }
		
        private Layout2d_size LAYOUT_size;
		public Layout2d_size Size {
			get {
				if(LAYOUT_size==null)
					throw NullError("LAYOUT_size can not be null!,(Size)",this, typeof(Layout2d_size));
				return LAYOUT_size;
			}
		}

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