namespace MauronAlpha.Geometry.Geometry2d.Units {

	//A line in 2d Space
	public class Line2d : GeometryComponent2d_unit {
		double a;
		double b;
		double c;

		public Line2d (Segment2d s) {
			a=s.B.Y-s.A.Y; //if 0, a & b are on the same vertical plane

			b=s.A.X-s.B.X; //if 0, a&b are on the same horizontal plane

			c = (s.B.X * s.A.Y) - (s.A.X * s.B.Y);
		}





	}
}
