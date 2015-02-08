using System;

using MauronAlpha.Interfaces;

using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;

using MauronAlpha.Geometry.Geometry2d.Units;


namespace MauronAlpha.Layout.Layout2d.Context {
	
	//A class describing A LayoutUnit's Position in the Action/Reaction-dependency tree	
	public class Layout2d_context : Layout2d_component,
	IEquatable<Layout2d_context>,
	I_instantiable<Layout2d_context>,
	I_protectable<Layout2d_context> {

		//constructors
		public Layout2d_context():base() {}
		public Layout2d_context(Vector2d position, Vector2d size):this() {
			CONTEXT_position = new Layout2d_position(position);
			CONTEXT_size = new Layout2d_size(size);
		}
		public Layout2d_context( int x, int y, int width, int height):this( new Vector2d(x,y),new Vector2d(width,height)) {}
		protected Layout2d_context( Layout2d_context source ) : this(source.Position.AsVector, source.Size.AsVector) {}

		//As string
		public string AsString {
			get {
				return "{"+Size.AsVector.AsString+"|"+Position.AsVector.AsString+"}";
			}
		}

		//Size
		private Layout2d_size CONTEXT_size;
		public Layout2d_size Size {
			get {
				if( CONTEXT_size==null )
					CONTEXT_size = new Layout2d_size();
				return CONTEXT_size.SetIsReadOnly(true);
			}
		}
		
		//Position
		private Layout2d_position CONTEXT_position;
		public Layout2d_position Position {
			get {
				if( CONTEXT_position==null )
					CONTEXT_position = new Layout2d_position();
				return CONTEXT_position.SetIsReadOnly(true);
			}
		}

		//Boolean states
		public bool Equals (Layout2d_context other) {
			if( !CONTEXT_position.Equals(other.Position) )
				return false;
			if( !CONTEXT_size.Equals(other.Size) )
				return false;
			return true;
		}
		private bool B_isReadOnly = false;
		public bool IsReadOnly { 
			get {
				return B_isReadOnly;
			}
		}

		//Methods

		public Layout2d_context Instance {
			get {
				return new Layout2d_context(CONTEXT_position.AsVector, CONTEXT_size.AsVector);
			}
		}
        public Layout2d_context SetSize(Layout2d_size size) {
            CONTEXT_size = size;
            return this;
        }
		public Layout2d_context SetPosition (Layout2d_position position) {
			CONTEXT_position = position;
			return this;
		}
		public Layout2d_context SetIsReadOnly(bool status) {
			B_isReadOnly = status;
			return this;
		}

	}

}