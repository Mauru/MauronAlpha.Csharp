using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;

namespace MauronAlpha.Geometry.Geometry2d.Shapes {
	
	public class Triangle2d:Polygon2d {

		public Triangle2d(Vector2d a, Vector2d b, Vector2d c):base(new Vector2dList{a,b,c}) {}
		public Triangle2d(double width):base(new Vector2dList() {
			new Vector2d(0,0),
			new Vector2d(0,width),
			new Vector2d(width,width)
		}) { }
	}
}
