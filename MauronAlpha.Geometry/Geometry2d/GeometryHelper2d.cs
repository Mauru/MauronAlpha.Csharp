namespace MauronAlpha.Geometry {
	using System;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
		using MauronAlpha.Geometry.Geometry2d.Collections;

	//Base class for generic mathematical operations
	public class GeometryHelper2d:MauronCode_utility {
		
		//A precision value to avoid 0
		public static double NotQuiteZero=0.00001;
	
		// degrees to radians
		public static double Deg2Rad (double n) {
			return Math.PI*n/180;
		}

		// radians to degrees
		public static double Rad2Deg (double n) {
			return n*(180/Math.PI);
		}

		//turn any angle into a number 0<360
		public static int NormaliseAngle(int degrees) {
			int retval=degrees%360;
			if( retval<0 )
				retval+=360;
			return retval;
		}

		//return PI
		public static double PI {
			get {
				return Math.PI;
			}
		}

		public static Vector2d FindCenterOfPolygon(Polygon2d poly) {
			return poly.Bounds.Center;
		}
		public static bool FindMinMax(Vector2dList points, out Vector2d min, out Vector2d max) {
			Vector2d tMin = null, tMax = null;
			foreach (Vector2d v in points) {

				if (tMin == null)
					min = v;
				else {

					int result = v.CompareTo(tMin);

					if (result < 0)
						tMin = v;

					if (tMax == null)
						tMax = v;
					else if (result > 0)
						tMax = v;
				}
			}
			min = tMin; max = tMax;
			return min != null;
		}
	
	}

}
