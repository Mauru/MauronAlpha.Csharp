using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.Geometry.Geometry2d.Shapes {

	//A Rectangle
	public class Rectangle2d : Polygon2d {
		public Rectangle2d(){}
		public Rectangle2d(Vector2d[] v){
			SetPoints(v);
		}
		public Rectangle2d(double x, double y, double width, double height){
			SetPoints(new Vector2d[4] {new Vector2d(x,y),new Vector2d(x+width,y),new Vector2d(x+width,y+height),new Vector2d(x,y+height)});
		}
	}
}
