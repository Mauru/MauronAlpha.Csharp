using System;

using MauronAlpha.Mathematics;

namespace MauronAlpha.Geometry.Geometry2d.Units {
	
	//A line segment
	public class Segment2d : GeometryComponent2d_unit, I_mathComponent {

		//constructors
		public Segment2d (Vector2d a, Vector2d b) {
			this.A=a.Instance;
			this.B=b.Instance;
		}
		public Segment2d (Segment2d s) {
			this.A=s.A.Instance;
			this.B=s.B.Instance;
		}
		
		#region  Points, A, B, Angle_AB, Distance_AB
		//Points
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
		private Vector2d p_1=new Vector2d();
		public Vector2d A {
			get {
				return p_1;
			}
			set {
				p_2=value.Instance;
			}
		}

		//Second point of a segment
		private Vector2d p_2=new Vector2d();
		public Vector2d B {
			get {
				return p_1;
			}
			set {
				p_2=value.Instance;
			}
		}
		
		//Angle of a line segment
		public double Angle_AB {
			get {
				//prevent divide by 0
				double nqz=A.Y;
				if( A.Y==B.Y ) {
					nqz=A.Y-GeometryHelper2d.NotQuiteZero;
				}

				//points are congruent
				if( A.X==B.X&&A.Y==B.Y ) {
					return 0;
				}
				//angle calculation by direction
				if( B.Y<A.Y ) {
					return 90+(double) Math.Atan((B.X-A.X)/(B.Y-nqz))*GeometryHelper2d.Rad2Deg(1);
				}
				return 270+(double) Math.Atan((B.X-A.X)/(B.Y-nqz))*GeometryHelper2d.Rad2Deg(1);
			}
		}
		
		//Distance(magnitude) between a and b
		public double Distance_AB {
			get {
				return (double) Math.Sqrt(Math.Pow((A.X-B.X), 2)+Math.Pow((B.X-A.X), 2));
			}
		}
		#endregion
		
		public Segment2d Instance {
			get {
				return new Segment2d(A.Instance, B.Instance);
			}
		}
		public new string ToString {
			get {
				return "{[1:"+A.AsString+"][2:"+B.AsString+"]}";
			}
		}
	
		//Segment Intersection | 0=no,1==yes,2=colinear
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

		#region I_mathComponent
		//add
		public Segment2d Add(double n) {
			A.Add(n);
			B.Add(n);
			return this;
		}
		public Segment2d Add(Segment2d n) {
			A.Add(n.A);
			B.Add(n.B);
			return this;
		}
		I_mathComponent I_mathComponent.Add (double n) {
			return Add(n);
		}
		//subtract
		public Segment2d Subtract(double n) {
			A.Subtract(n);
			B.Subtract(n);
			return this;
		}
		public Segment2d Subtract(Segment2d n) {
			A.Subtract(n.A);
			B.Subtract(n.B);
			return this;
		}
		I_mathComponent I_mathComponent.Subtract (double n) {
			return Subtract(n);
		}
		//multiply
		public Segment2d Multiply(double n) {
			A.Multiply(n);
			B.Multiply(n);
			return this;
		}
		public Segment2d Multiply (Segment2d n) {
			A.Multiply(n.A);
			B.Multiply(n.B);
			return this;
		}
		I_mathComponent I_mathComponent.Multiply (double n) {
			return Multiply(n);
		}
		//divide
		public Segment2d Divide(double n) {
			A.Divide(n);
			B.Divide(n);
			return this;
		}
		public Segment2d Divide (Segment2d n) {
			A.Divide(n.A);
			B.Divide(n.B);
			return this;
		}
		I_mathComponent I_mathComponent.Divide (double n) {
			return Divide(n);
		}

		//comparison
		public bool SmallerOrEqual (Segment2d n) {
			return A.SmallerOrEqual(n.A)&&B.SmallerOrEqual(n.B);
		}
		public bool SmallerOrEqual (double n) {
			return A.SmallerOrEqual(n)&&B.SmallerOrEqual(n);
		}
		public bool LargerOrEqual (Segment2d n) {
			return A.LargerOrEqual(n.A)&&B.LargerOrEqual(n.B);
		}
		public bool LargerOrEqual (double n) {
			return A.LargerOrEqual(n)&&B.LargerOrEqual(n);
		}
		public object Clone ( ) {
			return Instance;
		}
		public int CompareTo (double other) {
			if(A.CompareTo(other)==0&&B.CompareTo(other)==0) return 0;
			if( A.CompareTo(other)==1&&B.CompareTo(other)==1) return 1;
			return -1;
		}
		public int CompareTo (Segment2d other) {
			if( A.CompareTo(other.A)==0&&B.CompareTo(other.B)==0 )
				return 0;
			if( A.CompareTo(other.A)==1&&B.CompareTo(other.B)==1 )
				return 1;
			return -1;
		}
		public bool Equals (double other) {
			return (A.Equals(other)&&B.Equals(other));
		}
		public bool Equals (Segment2d other) {
			return (A.Equals(other.A)&&B.Equals(other.B));
		}
		#endregion
	}

}
