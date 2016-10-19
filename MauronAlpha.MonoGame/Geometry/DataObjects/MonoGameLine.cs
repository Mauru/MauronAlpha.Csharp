namespace MauronAlpha.MonoGame.Geometry.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry3d.Transformation;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Utility;

	using MauronAlpha.MonoGame.Collections;

	public class MonoGameLine : MonoGameComponent {

		Segment2d _segment;
		public Segment2d Segment {
			get { return _segment; }
		}
		public MonoGameLine(Vector2d start, Vector2d end) : base() {
			_segment = new Segment2d(start, end);
		}
		public MonoGameLine(double x1, double y1, double x2, double y2):base() {
			_segment = new Segment2d(
				new Vector2d(x1, y1),
				new Vector2d(x2, y2)
			);
		}
		public MonoGameLine(double x1, double y1, double x2, double y2, double thickness) : this(x1,y1,x2,y2) {
			_thickness = thickness;
		}
		public MonoGameLine(double x, double y, double degree, double magnitude, double thickness, bool isMagnitude):base() {

			Vector2d start = new Vector2d(x, y);

			Vector2d end = start.Project(degree, magnitude);

			_segment = new Segment2d(start, end);

			_thickness = thickness;

		}
		public MonoGameLine(Vector2d start, double degree, double magnitude, double thickness): base() {

			Vector2d end = start.Project(degree, magnitude);

			_segment = new Segment2d(start, end);

			_thickness = thickness;
		}

		public MonoGameLine(Segment2d s) {
			_segment = s;
		}

		System.Nullable<Rectangle> _rect;
		public Rectangle Rectangle {
			get {
				if (_rect == null) {
					Vector2d start = _segment.A;
					_rect = new Rectangle(start.IntX, start.IntY, (int)_segment.Distance_AB, (int)_thickness);
				}
				return _rect.Value;
			}
		}

		System.Nullable<double> _angle;
		public double AngleAsRad {
			get {
				if (_angle == null) {

					Vector2d start = _segment.A;
					Vector2d end = _segment.B.Copy;
					Vector2d newend = end.Copy.Subtract(start);

					Vector2d c = new Vector2d(newend.X, 0);

					//_angle = System.Math.Atan(newend.Y/c.X);
					_angle = _segment.Angle_ABRad;
					System.Diagnostics.Debug.Print(start.AsString+":"+end.AsString+" | "+newend.AsString + " @ "+_angle+"#" + _segment.Angle_AB+":"+_segment.Angle_ABRad);
				}
				return _angle.Value;
			}
		}

		double _thickness = 1;
		public double Thickness { get { return _thickness; } }

		public double Magnitude {
			get {
				return _segment.Distance_AB;
			}
		}

		System.Nullable<Color> _color;
		public Color Color {
			get {
				if (_color == null)
					return Color.White;
				return _color.Value;
			}
		}

		public static Matrix ToMonoMatrix(Matrix3d matrix) {
			return new Matrix(
				(float)matrix.Value(1, 1), (float)matrix.Value(1, 2), (float)matrix.Value(1, 3), (float) matrix.Value(1, 4),
				(float)matrix.Value(2, 1), (float)matrix.Value(2, 2), (float)matrix.Value(2, 3), (float) matrix.Value(2, 4),
				(float)matrix.Value(3, 1), (float)matrix.Value(3, 2), (float)matrix.Value(3, 3), (float) matrix.Value(3, 4),
				(float)matrix.Value(4, 1), (float)matrix.Value(4, 2), (float)matrix.Value(4, 3), (float)matrix.Value(4, 4)
			);
		}
		public static Vector2 AsVector2(Vector2d v) {
			return new Vector2(v.FloatX, v.FloatY);
		}
		public static Vector2 AsVector2(double x, double y) {
			return new Vector2((float)x, (float)y);
		}

	}

}
