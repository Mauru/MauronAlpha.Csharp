namespace MauronAlpha.Geometry.Geometry2d.Units {
	using MauronAlpha.Mathematics;
	using MauronAlpha.Geometry.Geometry2d.Collections;

	using MauronAlpha.Geometry.Geometry2d.Utility;
	//A line segment
	public class Segment2d : GeometryComponent2d_unit, I_mathComponent {

		//constructors
		public Segment2d (Vector2d a, Vector2d b) {
			_start = a;
			_end = b;
		}
		public Segment2d(Segment2d s) : this(s.Start, s.End) { }
		public Segment2d(double x1, double y1, double x2, double y2):this(new Vector2d(x1,y1), new Vector2d(x2,y2)) {}
		
		#region  Points, A, B, Angle_AB, Distance_AB
		//Points
		public Vector2dList Points {
			get {
				return new Vector2dList() { _start, _end };
			}
		}

		//First point of a segment
		private Vector2d _start = new Vector2d();
		public Vector2d Start {
			get { return _start; }
		}
		public Segment2d SetStart(Vector2d v) {
			_start = v;
			return this;
		}

		//Second point of a segment
		private Vector2d _end = new Vector2d();
		public Vector2d End {
			get {
				return _end;
			}
		}
		public Segment2d SetEnd(Vector2d v) {
			_end = v;
			return this;
		}

		//Angle of a line segment
		public double AngleDegree {
			get {
				//prevent divide by 0
				double nqz=_start.Y;
				if (_start.Y == _end.Y) {
					nqz = _start.Y - GeometryHelper2d.NotQuiteZero;
				}

				//points are congruent
				if (_start.X == _end.X && _start.Y == _end.Y) {
					return 0;
				}
				//angle calculation by direction
				if (_end.Y < _start.Y) {
					return 90 + GeometryHelper2d.Rad2Deg(
						GeometryHelper2d.Atan((_end.X - _start.X) / (_end.Y - nqz))
					);
				}
				return 270 + GeometryHelper2d.Rad2Deg(
					GeometryHelper2d.Atan((_end.X - _start.X) / (_end.Y - nqz))
				);
			}
		}
		public double AngleRad {
			get {
				//prevent divide by 0
				double nqz = _start.Y;
				if (_start.Y == _end.Y)
					nqz = _start.Y - GeometryHelper2d.NotQuiteZero;


				//points are congruent
				if (_start.X == _end.X && _start.Y == _end.Y) {
					return 0;
				}
				//angle calculation by direction
				if (_end.Y < _start.Y) {
					return GeometryHelper2d.Atan((_end.X - _start.X) / (_end.Y - nqz)) + GeometryHelper2d.Deg2Rad(90);
				}
				return GeometryHelper2d.Atan((_end.X - _start.X) / (_end.Y - nqz)) + GeometryHelper2d.Deg2Rad(270);
			}
		}

		//Distance(magnitude) between a and b
		public double Magnitude {
			get { return Segment2d.CalculateMagnitude(_start, _end); }
		}
		#endregion
		
		///<summary>Returns new Segment2d(a,b) </summary>
		public Segment2d Copy {
			get {
				return new Segment2d(_start, _end);
			}
		}
		public string AsString {
			get {
				return "{["+_start.AsString+"],["+_end.AsString+"]}";
			}
		}
	
		//Segment Intersection | 0=no,1==yes,2=colinear
		public bool Intersects(Segment2d s) {
			double d1,d2;
			double a1,a2,b1,b2,c1,c2;

			//Convert s into a Line
			a1 = s.End.Y - s.Start.Y;
			b1 = s.Start.X - s.End.X;
			c1 = (s.End.X * s.Start.Y) - (s.Start.X * s.End.Y);

			//test if a point is on the line (no? then is above or below line)
			d1 = (a1 * _start.X) + (b1 * _start.Y) + c1;
			d2 = (a1 * _end.X) + (b1 * _end.Y) + c1;

			//Basic test (same side of line? then no intersection)
			if(d1>0&&d2>0) return false;
			if(d1<0&&d2<0) return false;

			//calculate line 2
			a2 = _end.Y - _start.Y;
			b2 = _start.X - _end.X;
			c2 = (_end.X * _start.Y) - (_start.X * _end.Y);

			//test for point on line 2
			d1 = (a2 * s.Start.X) + (b2 * s.Start.Y) + c2;
			d2 = (a2 * s.End.X) + (b2 * s.End.Y) + c2;

			//Basic Test 2
			if(d1>0&&d2>0) return false;
			if(d1<0&&d2<0) return false;

			//colinear? (i.e. infinite point intersection)
			if((a1*b2)-(a2*b1)==0) return false;

			return true;
		}

		//static helper functions
		public static double CalculateMagnitude(Vector2d a, Vector2d b) {
			return GeometryHelper2d.Sqrt(GeometryHelper2d.Pow((b.X - a.X), 2) + GeometryHelper2d.Pow((b.Y - a.Y), 2));
		}

		#region I_mathComponent
		//add
		public Segment2d Add(double n) {
			_start.Add(n);
			_end.Add(n);
			return this;
		}
		public Segment2d Add(Segment2d n) {
			_start.Add(n.Start);
			_end.Add(n.End);
			return this;
		}
		public Segment2d Add(Vector2d v) {
			_start.Add(v);
			_end.Add(v);
			return this;
		}
		I_mathComponent I_mathComponent.Add (double n) {
			return Add(n);
		}
		//subtract
		public Segment2d Subtract(double n) {
			_start.Subtract(n);
			_end.Subtract(n);
			return this;
		}
		public Segment2d Subtract(Segment2d n) {
			_start.Subtract(n.Start);
			_end.Subtract(n.End);
			return this;
		}
		I_mathComponent I_mathComponent.Subtract (double n) {
			return Subtract(n);
		}
		//multiply
		public Segment2d Multiply(double n) {
			_start.Multiply(n);
			_end.Multiply(n);
			return this;
		}
		public Segment2d Multiply (Segment2d n) {
			_start.Multiply(n.Start);
			_end.Multiply(n.End);
			return this;
		}
		I_mathComponent I_mathComponent.Multiply (double n) {
			return Multiply(n);
		}
		//divide
		public Segment2d Divide(double n) {
			_start.Divide(n);
			_end.Divide(n);
			return this;
		}
		public Segment2d Divide (Segment2d n) {
			_start.Divide(n.Start);
			_end.Divide(n.End);
			return this;
		}
		I_mathComponent I_mathComponent.Divide (double n) {
			return Divide(n);
		}

		//comparison
		public bool SmallerOrEqual (Segment2d n) {
			return _start.SmallerOrEqual(n.Start)&&_end.SmallerOrEqual(n.End);
		}
		public bool SmallerOrEqual (double n) {
			return _start.SmallerOrEqual(n)&&_end.SmallerOrEqual(n);
		}
		public bool LargerOrEqual (Segment2d n) {
			return _start.LargerOrEqual(n.Start)&&_end.LargerOrEqual(n.End);
		}
		public bool LargerOrEqual (double n) {
			return _start.LargerOrEqual(n)&&_end.LargerOrEqual(n);
		}
		public object Clone ( ) {
			return Copy;
		}
		public int CompareTo (double other) {
			if(_start.CompareTo(other)==0&&_end.CompareTo(other)==0) return 0;
			if( _start.CompareTo(other)==1&&_end.CompareTo(other)==1) return 1;
			return -1;
		}
		public int CompareTo (Segment2d other) {
			if( _start.CompareTo(other.Start)==0&&_end.CompareTo(other.End)==0 )
				return 0;
			if( _start.CompareTo(other.Start)==1&&_end.CompareTo(other.End)==1 )
				return 1;
			return -1;
		}
		public bool Equals (double other) {
			return (_start.Equals(other) && _end.Equals(other));
		}
		public bool Equals (Segment2d other) {
			return (_start.Equals(other.Start) && _end.Equals(other.End));
		}
		#endregion
	}

}
