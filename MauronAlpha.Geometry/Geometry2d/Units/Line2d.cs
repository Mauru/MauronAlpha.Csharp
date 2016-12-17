namespace MauronAlpha.Geometry.Geometry2d.Units {

	//A line in 2d Space
	public class Line2d : GeometryComponent2d_unit {
		double a;
		double b;
		double c;

		public Line2d (Segment2d s) {
			a=s.End.Y-s.Start.Y; //if 0, a & b are on the same vertical plane

			b=s.Start.X-s.End.X; //if 0, a&b are on the same horizontal plane

			c = (s.End.X * s.Start.Y) - (s.Start.X * s.End.Y);
		}
	}
}
