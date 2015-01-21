using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;

namespace MauronAlpha.Layout.Layout2d.Position {

	//Proxy unit describing the size of a layout Object
	public class Layout2d_size:Layout2d_component {

		//constructor
		public Layout2d_size() {
			B_isStatic = false;
			RECT_bounds = new Rectangle2d ();
		}
		public Layout2d_size (Vector2d size, bool b_isStatic)
			: base() {
			B_isStatic = b_isStatic;
			RECT_bounds=new Rectangle2d(size);			
		}

		private Rectangle2d RECT_bounds = new Rectangle2d();
		public Rectangle2d Bounds { get {
			return RECT_bounds.SetIsReadOnly(true);
		} }
		
		public Vector2d AsVector { get {
			return new Vector2d(Bounds.Width,Bounds.Height);
		} }

		public string AsString {
			get {
				return AsVector.AsString;
			}
		}

		public double Height { 
			get {
				return Bounds.Height;
			}
		}
		public double Width {
			get {
				return Bounds.Width;
			}
		}

		private bool B_isStatic = false;
		public bool IsStatic {
			get {
				return B_isStatic;
			}
		}

		private bool B_isReadOnly=false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		
        public Layout2d_size SetIsReadOnly (bool b_isReadOnly) {
			B_isReadOnly=b_isReadOnly;
			return this;
		}
        public Layout2d_size Instance { get {
            Layout2d_size result = new Layout2d_size(AsVector,IsStatic);
            return result;
        } }
	}

}
