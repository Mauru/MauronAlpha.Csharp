using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;
using MauronAlpha.Geometry.Geometry2d.Interfaces;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Shapes;

namespace MauronAlpha.Geometry.Geometry2d.Units {
	using MauronAlpha.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

	///<summary> Approximation of an area as rectangle </summary>
	public class Polygon2dBounds : GeometryComponent2d, I_polygonShape2d {

		Vector2d _min;
		Vector2d _max;

		//Constructors
		public Polygon2dBounds(Vector2d min, Vector2d max): base() {
			_min = min;
			_max = max;
		}
		public Polygon2dBounds( I_polygonShape2d parentShape ): base() {
			Vector2dList pp = parentShape.Points.MinMax;
			_min = pp[0];
			_max = pp[1];
		}
		public Polygon2dBounds( Vector2dList points )	: base() {
			Vector2dList minmax = points.MinMax;
			_min = minmax[0];
			_max = minmax[1];
		}
		public Polygon2dBounds(double width, double height)	: base() {
			_min = new Vector2d(0, 0);
			_max = new Vector2d(width, height);
		}
		public Polygon2dBounds(Vector2d size) : this(size.X, size.Y) { }
		public Polygon2dBounds(double x, double y, double width, double height): base() {
			_min = new Vector2d(x, y);
			_max = new Vector2d(x + width, y + height);
		}

		public Vector2d Position {
			get {
				return _min;
			}
		}

		public Vector2d Min {
			get {
				return _min;
			}
		}
		public Vector2d Max {
			get {
				return _max;
			}
		}
		public Vector2d Center {
			get {
				return _min.Copy.Add(Size.Divide(2));
			}
		}
		public Vector2d Size {
			get {
				return Max.Difference(Min).Normalized;
			}
		}

		//Instancing & cloning
		public Polygon2dBounds Instance {
			get {
				return Copy;
			}
		}
		public Polygon2dBounds Copy {
			get {
				return new Polygon2dBounds(_min, _max);
			}
		}
		I_polygonShape2d I_polygonShape2d.Copy {
			get {
				return Instance;
			}
		}

		public bool Equals(Polygon2dBounds other) {
			return Min.Equals(other.Min) && Max.Equals(other.Max);
		}
		public bool Equals(I_polygonShape2d other) {
			return Points.Equals(other.Points);
		}

		public Vector2dList Points {
			get {
				return new Vector2dList(){
					_min,
					_max.Copy.SetY(_min.Y),
					_max,
					_min.Copy.SetY(_max.Y)
				};
			}
		}

		public double X {
			get { return _min.X; }
		}
		public double Y {
			get { return _min.Y; }
		}
		public double Width {
			get { return Size.X; }
		}
		public double Height {
			get { return Size.Y; }
		}

		Polygon2dBounds I_polygonShape2d.Bounds {
			get {
				return this;
			}
		}
		void I_polygonShape2d.SetBounds(Polygon2dBounds bounds) {
			return;
		}

		public Segment2dList Segments {
			get {
				Segment2dList result = new Segment2dList();
				Vector2dList points = Points;
				Vector2d buffer = Points.LastElement;
				int count = points.Count;

				foreach (Vector2d p in points) {
					Segment2d s = new Segment2d(buffer, p);
					result.Add(s);
					buffer = p;
				}
				return result;
			}
		}

		public string AsString {
			get { return _min.AsString + "|" + _max.AsString; }
		}

		//Static accessors
		public static Polygon2dBounds Empty {
			get {
				return new Polygon2dBounds(Vector2d.Zero, Vector2d.Zero);
			}
		}
		public static Polygon2dBounds FromMinMax(Vector2d min, Vector2d max) {
			return new Polygon2dBounds(min, max);
		}
	}

}
