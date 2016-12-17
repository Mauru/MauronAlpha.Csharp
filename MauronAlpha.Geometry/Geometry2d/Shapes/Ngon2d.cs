using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;
using System;

namespace MauronAlpha.Geometry.Geometry2d.Shapes {
	public class Ngon2d:Polygon2d {

		public Ngon2d(int sides, double radius)	: base() {
			Vector2dList points = new Vector2dList();
			for (int index = 0; index < sides; index++) {
				double factor = 2 * Math.PI * index / sides;
				Vector2d point = new Vector2d(radius * Math.Cos(factor), radius * Math.Sin(factor));
				points.Add(point);
			}
			SetPoints(points);
		}
	
	}
}
