namespace MauronAlpha.Geometry.Geometry3d.Shapes {
	using MauronAlpha.Geometry.Geometry3d.Collections;
	using MauronAlpha.Geometry.Geometry3d.Units;
	using MauronAlpha.Geometry.Geometry3d.Transformation;
	
	public class Rectangle3d:Mesh {

		public Rectangle3d(double width, double height) : base(CreatePoints(width,height)) {}
		public Rectangle3d(Vector3dList points, Matrix3d matrix) : base(points, matrix) { }
		
		static Vector3dList CreatePoints(double width,double height) {

			return new Vector3dList() {
				new Vector3d(0,0,0),
				new Vector3d(width,0,0),
				new Vector3d(width,height,0),
				new Vector3d(0,height,0)
			};

			

		}

		public Vector3d Min {
			get { return Point(0); }
		}
		public Vector3d Max {
			get {
				return Point(2);
			}
		}
		public Vector3d Center {
			get {
				Vector3d diff = Min.Difference(Max);
				return Min.Instance.Add(diff.Divide(2));
			}
		}

		public Rectangle3d Copy {
			get {
				return new Rectangle3d(base.Points, base.Matrix);
			}
		}

		public Rectangle3d RotateX(double degree) {

			double rad = GeometryHelper3d.DegreeToRadians(degree);

			Matrix3d m = Matrix3d.RotationX(rad);
			base.Matrix.Multiply(m);
			return this;

		}
		public Rectangle3d RotateY(double degree) {

			double rad = GeometryHelper3d.DegreeToRadians(degree);

			Matrix3d m = Matrix3d.RotationY(rad);
			base.Matrix.Multiply(m);
			return this;

		}
		public Rectangle3d RotateZ(double degree) {

			double rad = GeometryHelper3d.DegreeToRadians(degree);

			Matrix3d m = Matrix3d.RotationZ(rad);
			base.Matrix.Multiply(m);
			return this;

		}

		public Rectangle3d Translate(double x, double y, double z) {
			base.Matrix.AddToTranslation(x,y,z);
			return this;
		}
		public Rectangle3d Translate(Vector3d v) {
			return Translate(v.X, v.Y, v.Z);
		}

		public Segment3d Segment(int p1, int p2) {
			return new Segment3d(Point(p1), Point(p2));
		}
	}
}
