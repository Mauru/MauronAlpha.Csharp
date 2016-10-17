namespace MauronAlpha.MonoGame.Geometry.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry3d.Transformation;

	using Microsoft.Xna.Framework;

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

		System.Nullable<float> _angle;
		public float AngleAsRadFloat {
			get {
				if (_angle == null)
					_angle = (float)_segment.Angle_AB;
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
		/*public Vector2 Start2Mono {
			get {
				return Vector2dAsVector2(_segment.A);
			}
		}
		public Vector2 End2Mono {
			get {
				return Vector2dAsVector2(_segment.B);
			}
		}
		public Vector2 ScaleVector {
			get {
				Vector2d d = _segment.A.Difference(_segment.B).Normalized;
				return new Vector2(d.FloatX, d.FloatY);
			}
		}
		public float AngleAsFloat {
			get {
				return (float)_segment.Angle_AB;
			}
		} */
		System.Nullable<Color> _color;
		public Color Color {
			get {
				if (_color == null)
					return Color.White;
				return _color.Value;
			}
		}

		/* not needed
		// calculates the endpoint
		Matrix3d _matrix;
		public Matrix3d Matrix {
			get {
				if (_matrix == null)
					_matrix = Matrix3d.FromSegment(_segment);
				return _matrix;
			}
		}

		Matrix _mono;
		public Matrix MonoMatrix {
			get {
				if (_mono == null)
					_mono = MonoGameLine.ToMonoMatrix(Matrix);
				return _mono;
			}
		}
		public Matrix RegenerateMonoMatrix() {
			_mono = MonoGameLine.ToMonoMatrix(_matrix);
			return _mono;
		}
		 
		 public static Vector2d EndPoint(Matrix3d matrix) {
			return matrix.ApplyTo(1, 0);
		}
		 */
		public static Matrix ToMonoMatrix(Matrix3d matrix) {
			return new Matrix(
				(float)matrix.Value(1, 1), (float)matrix.Value(1, 2), (float)matrix.Value(1, 3), (float) matrix.Value(1, 4),
				(float)matrix.Value(2, 1), (float)matrix.Value(2, 2), (float)matrix.Value(2, 3), (float) matrix.Value(2, 4),
				(float)matrix.Value(3, 1), (float)matrix.Value(3, 2), (float)matrix.Value(3, 3), (float) matrix.Value(3, 4),
				(float)matrix.Value(4, 1), (float)matrix.Value(4, 2), (float)matrix.Value(4, 3), (float)matrix.Value(4, 4)
			);
		}
		public static Vector2 Vector2dAsVector2(Vector2d v) {
			return new Vector2(v.FloatX, v.FloatY);
		}


	}

}
