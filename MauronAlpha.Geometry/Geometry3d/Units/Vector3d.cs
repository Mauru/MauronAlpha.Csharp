namespace MauronAlpha.Geometry.Geometry3d.Units {
	using System;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry3d.Transformation;

	public class Vector3d : GeometryComponent3d,IEquatable<Vector3d>,IComparable<Vector3d> {
		double _x=0;
		public double X {
			get { return _x; }
		}
		public float FloatX {
			get {
			return (float) _x;
			}
		}
		public int IntX {
			get {
				return (int) _x;
			}
		}
		public Vector3d SetX(double n) {
			_x = n;
			return this;
		}

		double _y=0;
		public double Y {
			get { return _y; }
		}
		public float FloatY {
			get {
				return (float) _y;
			}
		}
		public float IntY {
			get {
				return (int) _y;
			}
		}
		public Vector3d SetY(double n) {
			_y = n;
			return this;
		}

		double _z=0;
		public double Z {
			get {
				return _z;
			}
		}
		public float FloatZ { get { return (float) _z; } }
		public float IntZ { get { return (int) _z; } }
		public Vector3d SetZ(double n) {
			_z = n;
			return this;
		}

		public Vector3d Set(double x, double y, double z) {
			_x = x;
			_y = y;
			_z = z;
			return this;
		}
		public Vector3d Set(Vector3d o) {
			_x = o.X;
			_y = o.Y;
			_z = o.Z;
			return this;
		}

		public Vector3d(Vector2d v, double z) {
			_x = v.X;
			_y = v.Y;
			_z = z;
		}
		public Vector3d (Vector3d p) {
			_x=p.X;
			_y=p.Y;
			_z=p.Z;
		}
		public Vector3d ( ) { }
		public Vector3d (double x, double y, double z) {
			_x=x;
			_y=y;
			_z=z;
		}

		public Vector3d Copy {
			get {
				return new Vector3d(this);
			}
		}

		public Vector3d Add (Vector3d v) {
			return Add(v.X,v.Y,v.Z);
		}
		public Vector3d Add(double x, double y, double z) {
			_x += x;
			_y += y;
			_z += z;
			return this;
		}
		
		public Vector3d Subtract (Vector3d v) {
			return Subtract(v.X,v.Y,v.Z);
		}
		public Vector3d Subtract(double x, double y, double z) {
			_x -= x;
			_y -= y;
			_z -= z;
			return this;
		}

		public Vector3d Divide(double n) {
			_x = _x / n;
			_y = _y / n;
			_z = _z / n;
			return this;
		}
		public Vector3d Divide(Vector3d o) {
			_x = _x / o.X;
			_y = _y / o.Y;
			_z = _z / o.Z;
			return this;
		}

		public Vector3d Multiply(double n) {
			_x = _x * n;
			_y = _y * n;
			_z = _z * n;
			return this;
		}
		public Vector3d Multiply(Vector3d o) {
			_x = _x * o.X;
			_y = _y * o.Y;
			_z = _z * o.Z;
			return this;
		}
		
		public Vector3d Transform(Matrix3d o) {
			Set(o.Multiply(this));
			return this;
		}
		public Vector3d Transformed(Matrix3d o) {
			return Copy.Transform(o);
		}

		public Vector3d Difference (Vector3d v) {
			return this.Instance.Subtract(v);
		}
		
		public Vector2d AsVector2dXY {
			get { return new Vector2d(_x,_y); }
		}
		public Vector2d AsVector2dYZ {
			get { return new Vector2d(_y,_z);}
		}
		public Vector2d AsVecor2dXZ {
			get {
				return new Vector2d(_x,_z);
			}
		}

		public Vector3d RelativeDirection(Vector3d o) {

			Vector3d result = new Vector3d(
				X.CompareTo(o.X),
				Y.CompareTo(o.Y),
				Z.CompareTo(o.Z)
			);
			return result;

		}

		//get the Absolute x and y (negative to positive)
		public Vector3d Normalized {
			get {
				return new Vector3d(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
			}
		}
		public Vector3d Instance {
			get { return new Vector3d(this); }
		}
		public new string ToString {
			get {
				return "{[x:"+X+"],[y:"+Y+"],[z:"+Z+"]}";
			}
		}
		public bool Equals (Vector3d v) {
			return (v.ToString==this.ToString);
		}

		public int CompareTo(Vector3d other) {
			if (X < other.X)
				return -1;
			else if (X > other.X)
				return 1;

			if (Y < other.Y)
				return -1;
			else if (Y > other.Y)
				return 1;

			if (Z < other.Z)
				return -1;
			else if (Z > other.Z)
				return 1;

			return 0;
		}
	
		/// <summary> Magnitude / Length(from 0,0,0) of a Vector </summary>
		public double Magnitude {
			get {
				double t = _x * _x + _y * _y + _z * _z;
				return t / t;
			}
		}

		public static Vector3d Zero {
			get { return new Vector3d(); }
		}

		public Vector3d Inverted {
			get {
				return new Vector3d(_x*-1,_y*-1,_z*-1);
			}
		}

		public Vector3d Direction {
			get {
				return new Vector3d(
					_x.CompareTo(0),
					_y.CompareTo(0),
					_z.CompareTo(0)
				);
			}
		}
	}

}