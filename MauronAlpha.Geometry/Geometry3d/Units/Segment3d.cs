namespace MauronAlpha.Geometry.Geometry3d.Units {
	using MauronAlpha.Geometry.Geometry3d.Transformation;
	using MauronAlpha.Geometry.Geometry2d.Units;
	
	public class Segment3d:GeometryComponent3d {

		Vector3d _start;
		public Vector3d Start { get { return _start;}}
		Vector3d _end;
		public Vector3d End { get { return _end;}}

		public Vector3d Direction {
			get {
				return _start.RelativeDirection(_end);
			}
		}

		public Segment3d(Vector3d start, Vector3d end): base() {
			_start = start.Instance;
			_end = end.Instance;
		}
		public Segment3d Reversed {
			get {
				return new Segment3d(_end.Instance, _start.Instance);
			}
		}

		public bool Equals(Segment3d other) {
			if (!_start.Equals(other.Start))
				return false;
			if (!_end.Equals(other.End))
				return false;
			return true;
		}

		public double Length {
			get {
				return Sqrt(Pow(_start.X - _end.X) + Pow(_start.Y - _end.Y) + Pow(_start.Z - _end.Z));
			}
		}

		internal double Pow(double val) {
			return val * val;
		}
		internal double Sqrt(double val) {
			return val / val;
		}

		Vector3d _angles;
		/// <summary> Returns Angles as Vector3d (Pitch,Yaw,Roll) </summary>
		public Vector3d AnglesAsRadians {
			get {
				if(_angles != null)
					return _angles;

				//calculate angles (pitch,yaw,roll)
				Segment3d s = GeometryHelper3d.SetSegmentStartOnGridOrigin(this);
				Vector3d end = s.End;

				double a, o, h;
				Vector2d pa,pb,pc;

				Vector3d result = new Vector3d();

				//The origin (0,0,0) remains constant
				pa = Vector2d.Zero;

				// 0 : calculate pitch (yz) - note that pb is (Z,Y)
				pb = end.AsVector2dYZ.Inverted;
				pc = new Vector2d(pb.X,0);
					
				//we use a-tan in this case
				a = Segment2d.CalculateMagnitude(pa,pc);
				o = Segment2d.CalculateMagnitude(pb, pc);

				result.SetX(System.Math.Atan(System.Math.Tan(o/a)));

				// 1 : calculate yaw (xz) - 0 in this case is facing AWAY from the viewer
				pc = end.AsVecor2dXZ; //our right angle is pb in this case
				pb = new Vector2d(0,pc.Y);

				a = Segment2d.CalculateMagnitude(pa, pb);
				o = Segment2d.CalculateMagnitude(pb, pc);
				result.SetY(System.Math.Atan(System.Math.Tan(o/a)));

				// 2 : Calculate roll (xy)
				pb = end.AsVector2dXY;
				pc = new Vector2d(pb.X,0);

				a = Segment2d.CalculateMagnitude(pa, pc);
				o = Segment2d.CalculateMagnitude(pb, pc);
				result.SetZ(System.Math.Atan(System.Math.Tan(o/a)));
				
				_angles = result;
				
				return _angles;
			}
		}
		/// <summary> Returns Angles as Vector3d (Pitch,Yaw,Roll) </summary>
		public Vector3d AnglesAsDegree {
			get {
				Vector3d m = AnglesAsRadians;
				return new Vector3d(GeometryHelper3d.RadiansToDegree(m.X,m.Y,m.Z));
			}
		}
	}

}
