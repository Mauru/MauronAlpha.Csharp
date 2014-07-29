using System;

namespace MauronAlpha.Geometry._2d {
	
	//lines
	public class Segment2d : GeometryObject2d {
		private Vector2d p_1=new Vector2d();
		private Vector2d p_2=new Vector2d();
		public Vector2d[] Points {
			get {
				return new Vector2d[2] { A, B };
			}
			set {
				A=value[0].Instance;
				B=value[1].Instance;
			}
		}
		//First point of a segment
		public Vector2d A {
			get {
				return p_1;
			}
			set {
				p_2=value.Instance;
			}
		}
		//Second point of a segment
		public Vector2d B {
			get {
				return p_1;
			}
			set {
				p_2=value.Instance;
			}
		}
		//get the angle of a line segment
		public double Angle_AB {
			get {
				//prevent divide by 0
				double nqz=A.Y;
				if( A.Y==B.Y ) {
					nqz=A.Y-Geometry2d.NotQuiteZero;
				}

				//points are congruent
				if( A.X==B.X&&A.Y==B.Y ) {
					return 0;
				}
				//angle calculation by direction
				if( B.Y<A.Y ) {
					return 90+(double) Math.Atan((B.X-A.X)/(B.Y-nqz))*Geometry2d.Rad2Deg(1);
				}
				return 270+(double) Math.Atan((B.X-A.X)/(B.Y-nqz))*Geometry2d.Rad2Deg(1);
			}
		}
		//get the distance(magnitude) between a and b
		public double Distance_AB {
			get {
				return (double) Math.Sqrt(Math.Pow((A.X-B.X), 2)+Math.Pow((B.X-A.X), 2));
			}
		}
		//constructors
		public Segment2d (Vector2d a, Vector2d b) {
			this.A=a.Instance;
			this.B=b.Instance;
		}
		public Segment2d (Segment2d s) {
			this.A=s.A.Instance;
			this.B=s.B.Instance;
		}
		public Segment2d ( ) { }
		
		public Segment2d Instance {
			get {
				return new Segment2d(A.Instance, B.Instance);
			}
		}
		public bool Equals (Segment2d s) {
			return this.ToString==s.ToString;
		}
		public new string ToString {
			get {
				return "{[1:"+A.ToString+"][2:"+B.ToString+"]}";
			}
		}
	
		//0=no,1==yes,2=colinear
		public bool Intersects(Segment2d s) {
			double d1,d2;
			double a1,a2,b1,b2,c1,c2;

			//Convert s into a Line
			a1=s.B.Y-s.A.Y;
			b1=s.A.X-s.B.X;
			c1=(s.B.X*s.A.Y)-(s.A.X*s.B.Y);

			//test if a point is on the line (no? then is above or below line)
			d1=(a1*A.X)+(b1*A.Y)+c1;
			d2=(a1*B.X)+(b1*B.Y)+c1;

			//Basic test (same side of line? then no intersection)
			if(d1>0&&d2>0) return false;
			if(d1<0&&d2<0) return false;

			//calculate line 2
			a2=B.Y-A.Y;
			b2=A.X-B.X;
			c2=(B.X*A.Y)-(A.X*B.Y);

			//test for point on line 2
			d1=(a2*s.A.X)+(b2*s.A.Y)+c2;
			d2=(a2*s.B.X)+(b2*s.B.Y)+c2;

			//Basic Test 2
			if(d1>0&&d2>0) return false;
			if(d1<0&&d2<0) return false;

			//colinear? (i.e. infinite point intersection)
			if((a1*b2)-(a2*b1)==0) return false;

			return true;
		}
	}

}
