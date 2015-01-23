using System;
using MauronAlpha.Interfaces;

using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;

using MauronAlpha.Geometry.Geometry2d.Units;


namespace MauronAlpha.Layout.Layout2d.Context {
	
	//A class describing A LayoutUnit's Position in the Action/Reaction-dependency tree	
	public class Layout2d_context:Layout2d_component,IEquatable<Layout2d_context>,I_instantiable<Layout2d_context>,I_protectable<Layout2d_context> {

		//constructors
		public Layout2d_context():base() {}

		internal Layout2d_context( Layout2d_context source ) : this() {
			LAYOUT_position = source.LAYOUT_position;
			LAYOUT_size = source.LAYOUT_size;
		}

		public Layout2d_context(Vector2d position, Vector2d size):this() {
			LAYOUT_position=new Layout2d_position( position);
			LAYOUT_size = new Layout2d_size(size);
		}
		public Layout2d_context( int x, int y, int width, int height):this( new Vector2d(x,y),new Vector2d(width,height)) {}

		//As String
		public string AsString { 
			get {
				return "{[ Position : "+Position.AsString+" ], [ Size : "+Size.AsString+" ]}";
			}
		}

		//Boolean states
		public bool Equals (Layout2d_context other) {
			return Size.Equals(other.Size)&&Position.Equals(other.Position);
		}
		
		private bool B_isReadOnly = false;
		public bool IsReadOnly { get {
			return B_isReadOnly;
		} }

		//Position
		private Layout2d_position LAYOUT_position;
		public Layout2d_position Position { get {
			if(LAYOUT_position == null)
				throw NullError("No position set!,(Position)",this,typeof(Vector2d));
			return LAYOUT_position.SetIsReadOnly(true);
		} }
		
		//Size
        private Layout2d_size LAYOUT_size;
		public Layout2d_size Size {
			get {
				if(LAYOUT_size==null)
					throw NullError("LAYOUT_size can not be null!,(Size)",this, typeof(Layout2d_size));
				return LAYOUT_size.SetIsReadOnly(true);
			}
		}

		//Methods
        public Layout2d_context SetSize(Layout2d_size size) {
            LAYOUT_size = size;
            return this;
        }
		public Layout2d_context Instance {
			get {
				return new Layout2d_context(Position.AsVector, Size.AsVector);
			}
		}
		public Layout2d_context SetIsReadOnly(bool status) {
			B_isReadOnly = status;
			return this;
		}
	}

}