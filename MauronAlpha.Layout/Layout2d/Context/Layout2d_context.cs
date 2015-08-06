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
		public Layout2d_context(Layout2d_position position, Layout2d_size size)
			: this() {
			CONTEXT_position = position;
			CONTEXT_size = size;
		}
		public Layout2d_context( int x, int y, int width, int height):this( new Vector2d(x,y),new Vector2d(width,height)) {}
		protected Layout2d_context( Layout2d_context source ) : this(source.Position.AsVector, source.Size.AsVector) {
			SetResizeMode(ResizeMode);
			SetMaxSize(MaxSize);
		}

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
		private Layout2d_size CONTEXT_maxSize;
		public Layout2d_size MaxSize {
			get {
				if (CONTEXT_maxSize == null)
					CONTEXT_maxSize = new Layout2d_size(-1,-1);
				return CONTEXT_maxSize.SetIsReadOnly(true);
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

		private Layout2d_resizeMode CONTEXT_resize = ResizeMode_fixed.Instance;
		public Layout2d_resizeMode ResizeMode { get { return CONTEXT_resize; } }
		public Layout2d_context SetResizeMode(Layout2d_resizeMode mode) {
			CONTEXT_resize = mode;
			return this;
		}
		public Layout2d_context SetResizeMode(string mode) {
			CONTEXT_resize = Layout2d_resizeMode.ByString(mode);
			return this;
		}
		public Layout2d_context SetMaxSize(Vector2d v) {
			CONTEXT_maxSize =new Layout2d_size(v);
			return this;
		}
		public Layout2d_context SetMaxSize(int x, int y) {
			CONTEXT_maxSize = new Layout2d_size(x,y);
			return this;
		}
		public Layout2d_context SetMaxSize(Layout2d_size v) {
			CONTEXT_maxSize = v;
			return this;
		}
		
		//Methods
		public Layout2d_context Instance {
			get {
				return new Layout2d_context(Position, Size).SetMaxSize(MaxSize).SetResizeMode(ResizeMode);
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