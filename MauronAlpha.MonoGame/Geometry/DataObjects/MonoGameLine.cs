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
		
		//constructors
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
			Matrix3d scale = Matrix3d.Scale(magnitude, magnitude, magnitude);
			Vector2d scaledUnitVector = scale.ApplyTo(0, 1);
			_magnitude = magnitude;
			double rad = GeometryHelper.Deg2Rad(degree);
			Matrix3d rotation = Matrix3d.RotationZDegree(degree);
			_angle = rad;
			Vector2d transformedUnitVector = rotation.ApplyTo(scaledUnitVector);
			Vector2d end = transformedUnitVector.Add(start);
			_segment = new Segment2d(start, end);
			_thickness = thickness;
		}
		public MonoGameLine(Vector2d start, double rad, double magnitude, double thickness, bool isRad): base() {
			Matrix3d scale = Matrix3d.Scale(magnitude, magnitude, magnitude);
			Vector2d scaledUnitVector = scale.ApplyTo(0, 1);
			Matrix3d rotation = Matrix3d.RotationZRad(rad);
			_angle = rad;
			Vector2d transformedUnitVector = rotation.ApplyTo(scaledUnitVector);
			Vector2d end = transformedUnitVector.Add(start);
			_segment = new Segment2d(start, end);
			_thickness = thickness;
		}

		public MonoGameLine(Segment2d s):base() {
			_segment = s;
		}
		public MonoGameLine(Segment2d s, int thickness):base() {
			_segment = s;
			_thickness = thickness;
		}

		System.Nullable<Rectangle> _rect;
		public Rectangle Rectangle {
			get {
				if (_rect == null) {
					Vector2d start = _segment.Start;
					_rect = new Rectangle(start.IntX, start.IntY, (int)Magnitude, (int)Thickness);
				}
				return _rect.Value;
			}
		}

		System.Nullable<double> _angle;
		public double AngleAsRad {
			get {
				if (_angle == null) { 
					_angle = _segment.AngleRad*-1;
				}
				return _angle.Value;
			}
		}

		double _thickness = 1;
		public double Thickness { get { return _thickness; } }
		public void SetThickness(double n) {
			_thickness = n;
		}

		System.Nullable<double> _magnitude;
		public double Magnitude {
			get {
				if (_magnitude == null)
					_magnitude = _segment.Magnitude;
				return _magnitude.Value;
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

		public void Offset(Vector2d v) {
			_rect = null;
			_segment.Add(v);
		}

		// Utility functions
		public static Matrix ToMonoMatrix(Matrix3d matrix) {
			return new Matrix(
				(float)matrix.Value(1, 1), (float)matrix.Value(1, 2), (float)matrix.Value(1, 3), (float)matrix.Value(1, 4),
				(float)matrix.Value(2, 1), (float)matrix.Value(2, 2), (float)matrix.Value(2, 3), (float)matrix.Value(2, 4),
				(float)matrix.Value(3, 1), (float)matrix.Value(3, 2), (float)matrix.Value(3, 3), (float)matrix.Value(3, 4),
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
