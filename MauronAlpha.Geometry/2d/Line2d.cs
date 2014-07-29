using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.Geometry._2d {
	public class Line2d : GeometryObject2d {
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
