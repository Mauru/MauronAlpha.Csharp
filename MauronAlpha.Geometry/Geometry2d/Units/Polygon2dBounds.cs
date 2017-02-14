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

		public void Offset(Vector2d v) {
			_min.Add(v);
			_max.Add(v);
		}
		public void Offset(double x, double y) {
			_min.Add(x, y);
			_max.Add(x, y);
		}

		Segment2dList I_polygonShape2d.Segments {
			get {
				return GenerateSegments(this);
			}
		}
		Polygon2dBounds I_polygonShape2d.Bounds {
			get {
				return this;
			}
		}
		void I_polygonShape2d.SetBounds(Polygon2dBounds bounds) {
			return;
		}

		public string AsString {
			get { return "{ X: " + _min.X + ", Y: " + _min.Y + ", Width: " + Width + ", Height:"  + Height + " }"; }
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
		public static Polygon2dBounds FromPoints(Vector2dList points) {
			Vector2d min = null;
			Vector2d max = null;
			foreach (Vector2d p in points) {
				if (min == null) {
					min = new Vector2d(p.X, p.Y);
					max = new Vector2d(p.X, p.Y);
				}
				else {
					if (min.X > p.X)
						min.SetX(p.X);
					if (min.Y > p.Y)
						min.SetY(p.Y);
					if (max.X < p.X)
						max.SetX(p.X);
					if (max.Y < p.Y)
						max.SetY(p.Y);
				}
			}
			return Polygon2dBounds.FromMinMax(min, max);
		}
		public static Segment2dList GenerateSegments(I_polygonShape2d shape) {
			Segment2dList result = new Segment2dList();
			Vector2dList points = shape.Points;
			Vector2d buffer = points.LastElement;
			int count = points.Count;

			foreach (Vector2d p in points) {
				Segment2d s = new Segment2d(buffer, p);
				result.Add(s);
				buffer = p;
			}
			return result;
		}
		/// <summary> Returns a new merged Polygon2dBounds </summary>
		public static Polygon2dBounds Combine(Polygon2dBounds a, Polygon2dBounds b) {
			Vector2d minB=b.Min, maxB=b.Max, aa = a.Min.Copy, bb = a.Max.Copy;

			if (minB.X < aa.X)
				aa.SetX(minB.X);
			if (minB.Y < aa.Y)
				aa.SetY(minB.Y);

			if (maxB.X > bb.X)
				bb.SetX(maxB.X);
			if (maxB.Y > bb.Y)
				bb.SetY(maxB.Y);

			return Polygon2dBounds.FromMinMax(aa, bb);			
		}
	}

}
