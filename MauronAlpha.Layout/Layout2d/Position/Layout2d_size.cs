using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;

namespace MauronAlpha.Layout.Layout2d.Position {


	public class Layout2d_size:Layout2d_component {

		//constructor
		public Layout2d_size (Vector2d size, bool b_isStatic)
			: base() {
			B_isStatic = b_isStatic;
			RECT_bounds=new Rectangle2d(size);			
		}

		private Rectangle2d RECT_bounds = new Rectangle2d();
		public Rectangle2d Bounds { get {
			return RECT_bounds.SetIsReadOnly(true);
		} }
		
		public Vector2d AsVector2d { get {
			return new Vector2d(Bounds.Width,Bounds.Height);
		} }

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
	
	}

}
