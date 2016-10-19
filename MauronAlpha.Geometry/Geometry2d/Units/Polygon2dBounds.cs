using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Collections;
using MauronAlpha.Geometry.Geometry2d.Interfaces;
using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.Geometry.Shapes;

namespace MauronAlpha.Geometry.Geometry2d.Units {
	using MauronAlpha.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

public class Polygon2dBounds : GeometryComponent2d, I_polygonShape2d {
	//Constructors
	private Polygon2dBounds():base() {}


	Vector2d _min;
	Vector2d _max;
	Vector2d _center;

	//Relative constructors
	public Polygon2dBounds( I_polygonShape2d parentShape ) : this() {

		Vector2dList pp = parentShape.Points.MinMax;
		_min = pp[0];
		_max = pp[1];
		_center = Min.Difference(_max);

	}

	public string AsString {
		get { return _min.AsString + "|" + _max.AsString; }
	}

	//Absolute constructors
	private Polygon2dBounds( Vector2d center, Vector2d min, Vector2d max )	: this() {

		_min = min;
		_max = max;
		_center = center;

	}
	public Polygon2dBounds( Vector2dList points )	: this() {

		Vector2dList minmax = points.MinMax;
		_min = minmax[0];
		_max = minmax[1];
		_center = _min.Difference(_max);

	}
	public Polygon2dBounds(double width, double height)	: this() {
		_min = new Vector2d(0, 0);
		_max = new Vector2d(width, height);
		_center = _min.Difference( _max );
	}
	public Polygon2dBounds(Vector2d size) : this(size.X, size.Y) { }

	public Vector2d Position {
		get {
			return _min;
		}
	}

	public bool Equals(Polygon2dBounds other) {
		return Min.Equals(other.Min) && Max.Equals(other.Max);
	}
	public bool Equals(I_polygonShape2d other) {
		return Points.Equals(other.Points);
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
			return _center;
		}
	}
	public Vector2d Size {
		get {
			return Max.Difference(Min).Normalized;
		}
	}

	//Instancing
	public Polygon2dBounds Instance {
		get {
			return new Polygon2dBounds(_min,_max,_center);
		}
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

	public void SetBounds(Polygon2dBounds bounds) {
		return;
	}
	public Polygon2dBounds Bounds {
		get {
			return this;
		}
	}
	Segment2dList _segments;
	public Segment2dList Segments {
		get {
			if (_segments != null)
				return _segments;

			Segment2dList result = new Segment2dList();
			Vector2dList points = Points;
			Vector2d buffer = Points.LastElement;
			int count = points.Count;

			foreach (Vector2d p in points) {
				Segment2d s = new Segment2d(buffer, p);
				result.Add(s);
				buffer = p;
			}
			_segments = result;
			return _segments;
		}
	}

	public I_polygonShape2d Copy {
		get {
			return Instance;
		}
	}

	public static Polygon2dBounds FromMinMax(Vector2d min, Vector2d max) {
		return new Polygon2dBounds(min.Difference(max), min, max);
	}
}

}
