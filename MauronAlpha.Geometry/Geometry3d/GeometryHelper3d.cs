namespace MauronAlpha.Geometry.Geometry3d {

	using MauronAlpha.Geometry.Geometry3d.Collections;
	using MauronAlpha.Geometry.Geometry3d.Units;
	using MauronAlpha.Geometry.Geometry3d.Interfaces;
	using MauronAlpha.Geometry.Geometry3d.Transformation;

	public class GeometryHelper3d:GeometryComponent3d {

		public static Transformations3d RotateAroundSegment(Segment3d axis, double degrees) {

			Vector3d offset = axis.Start.Instance;

			Vector3d a = axis.AnglesAsDegree.Inverted;

			Transformations3d steps = new Transformations3d();
			steps.Add(Matrix3d.FromVector3d(offset.Inverted));
			steps.Add(Matrix3d.RotationX(a.X));
			steps.Add(Matrix3d.RotationY(a.Y));
			steps.Add(Matrix3d.RotationZ(a.Z));
			steps.Add(Matrix3d.RotationY(degrees)); // this is the actual rotation
			steps.Add(Matrix3d.RotationZ(a.Z * -1));
			steps.Add(Matrix3d.RotationY(a.Y * -1));
			steps.Add(Matrix3d.RotationX(a.X * -1));
			steps.Add(Matrix3d.FromVector3d(offset));

			return steps;
		}

		public static Segment3d SetSegmentStartOnGridOrigin(Segment3d s) {
			Vector3d start = s.Start;
			Vector3d end = s.End;

			Vector3d d = start.Inverted;
			Vector3d newEnd = new Vector3d(end.X-d.X,end.Y-d.Y,end.Z-d.Z);

			return new Segment3d(Vector3d.Zero,newEnd);
		}

		public static double DegreeToRadians(double n) { 
			return System.Math.PI*n/180;
		}
		public static double RadiansToDegree(double n) { 
			return n*(180/System.Math.PI);
		}
		public static Vector3d RadiansToDegree(double x, double y, double z) {
			return new Vector3d(RadiansToDegree(x),RadiansToDegree(y),RadiansToDegree(z));
		}
	}
}
