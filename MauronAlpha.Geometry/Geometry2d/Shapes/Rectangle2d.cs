using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.Geometry.Geometry2d.Shapes {

	//A Rectangle
	public class Rectangle2d : Polygon2d {
		public Rectangle2d():base(){}
		public Rectangle2d ( Vector2d size ):this(0,0,size.X,size.Y) {}
		public Rectangle2d(Vector2d[] v){
			SetPoints(v);
		}
		public Rectangle2d(double x, double y, double width, double height):base(){
			SetPoints(new Vector2d[4] {new Vector2d(x,y),new Vector2d(x+width,y),new Vector2d(x+width,y+height),new Vector2d(x,y+height)});
		}

		public new Rectangle2d SetIsReadOnly (bool state) {
			base.SetIsReadOnly(state);
			return this;
		}

	}
}
