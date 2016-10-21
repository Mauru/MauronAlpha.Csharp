namespace MauronAlpha.Geometry.Geometry2d.Utility {
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Collections;

	using MauronAlpha.Geometry.Geometry3d.Transformation; //#TODO#:Switch to matrix2d class//

	//Base class for generic mathematical operations
	public class GeometryHelper2d:MauronCode_utility {

		public static double DotProduct(Vector2d a, Vector2d b, Vector2d c) {
			Vector2d ba = new Vector2d(a.X - b.X, a.Y - b.Y);
			Vector2d bc = new Vector2d(c.X - b.X, c.Y - b.Y);
			return (ba.X * bc.X + ba.Y * bc.Y);
		}
		public static double CrossProduct(Vector2d a, Vector2d b, Vector2d c) {
			Vector2d ba = new Vector2d(a.X - b.X, a.Y - b.Y);
			Vector2d bc = new Vector2d(c.X - b.X, c.Y - b.Y);
			return (ba.X * bc.Y - ba.Y * bc.X);
		}

		public static double Abs(double n) {
			return System.Math.Abs(n);
		}

		//A precision value to avoid 0
		public static double NotQuiteZero=0.00001;
	
		// degrees to radians
		public static double Deg2Rad (double n) {
			return PI * n / 180.0;
		}
		public static double Sin(double n) {
			return System.Math.Sin(n);
		}
		public static double Asin(double n) {
			return System.Math.Asin(n);
		}
		public static double Cos(double n) {
			return System.Math.Cos(n);
		}
		public static double Acos(double n) {
			return System.Math.Acos(n);
		}
		public static double Atan(double n) {
			return System.Math.Atan(n);
		}
		public static double Tan(double n) {
			return System.Math.Tan(n);
		}
		public static double Atan2(double x, double y) {
			return System.Math.Atan2(x, y);
		}

		public static double Pow(double n, double p) {
			return System.Math.Pow(n, p);
		}
		public static double Sqrt(double n) {
			return System.Math.Sqrt(n);
		}

		// radians to degrees
		public static double Rad2Deg (double n) {
			return n * (180.0 / PI);
		}

		//turn any angle into a number 0<360
		public static int NormaliseAngle(int degrees) {
			int retval=degrees%360;
			if( retval<0 )
				retval+=360;
			return retval;
		}

		public static Vector2d ProjectionMatrixDegree(double degree, double distance) {
			double rad = Deg2Rad(90);

			System.Diagnostics.Debug.Print("Rotating by " + degree + " degrees with magnitude " + distance + ".");
			//we assume 0 degrees is up
			Matrix3d scale = Matrix3d.Scale(distance, 1, 1);
			Vector2d scaled = scale.ApplyTo(1, 1);
			System.Diagnostics.Debug.Print("Scaled vector = " + scaled.AsString);

			Matrix3d rotate = Matrix3d.RotationZRad(rad);
			System.Diagnostics.Debug.Print("Matrix is [ " + rotate[0, 0] + "," + rotate[0, 1] + " ; " + rotate[1, 0] + "," + rotate[1, 1] + " ].");
			Vector2d rotated = rotate.ApplyTo(0,1);
			System.Diagnostics.Debug.Print("Final vector = " + rotated.AsString);
			//System.Diagnostics.Debug.Print("Final vector = " + scale.ApplyTo(rotated).AsString);
			return rotated;
		}

		//return PI
		public static double PI {
			get {
				return System.Math.PI;
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
