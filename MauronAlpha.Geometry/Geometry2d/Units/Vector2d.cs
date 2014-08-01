using System;
using MauronAlpha.Mathematics;



namespace MauronAlpha.Geometry.Geometry2d.Units {

	//coordinates
	public class Vector2d : GeometryComponent2d, I_mathComponent {

		//X
		protected double INT_x=0;
		public double X { get { return INT_x; } }
		public Vector2d SetX(double x) {
			INT_x= x;
			return this;
		}

		//Y
		protected double INT_y=0;
		public double Y { get { return INT_y; } }
		public Vector2d SetY (double y) {
			INT_y=y;
			return this;
		}

		//constructors
		public Vector2d ( ) { }
		public Vector2d (double x, double y) {
			SetX(x);
			SetY(y);
		}
		public Vector2d (double n):this(n,n) {}
		public Vector2d (Vector2d p):this(p.X,p.Y) {}

		//Mirror this point around another
		public Vector2d Mirror(Vector2d v) {
			Vector2d p = (Vector2d) Difference(v).Multiply(-2);
			return Add(p);
		}
		//rotate the point around another
		public Vector2d Rotate (Vector2d v, double angle) {
			double s=Math.Sin(angle);
			double c=Math.Cos(angle);

			Subtract(v);

			SetX(v.X*c-v.Y*s);
			SetY(v.X*s+v.Y*c);

			Add(v);
			return this;
		}

		//return the distance to another point
		public Vector2d Difference (Vector2d v) {
			return this.Instance.Subtract(v);
		}

		//get the Absolute x and y (negative to positive)
		public Vector2d Normalized {
			get {
				return new Vector2d(Math.Abs(X), Math.Abs(Y));
			}
		}
		//Mirror around |0,0|
		public Vector2d Inverted {
			get { return  (Vector2d) Instance.Multiply(-1); }
		}

		//return a copy of the vector
		public Vector2d Instance {
			get { return new Vector2d(this); }
		}

		//string
		public new string ToString {
			get {
				return "{[x:"+X+"],[y:"+Y+"]}";
			}
		}

		//stretch or compress around a point
		public Vector2d Transform(Vector2d v, Vector2d s){
			Vector2d p = Difference(v);
			p.Multiply(s);
			Add(p);
			return this;
		}
		//stretch or compress by [-1+] around a point
		public Vector2d Transform (Vector2d v, Double n) {
			Vector2d p=Difference(v);
			p.Multiply(new Vector2d(n));
			Add(p);
			return this;
		}

		//Set the point
		public Vector2d Set(double x, double y) {
			SetX(x);
			SetY(y);
			return this;
		}
		public Vector2d Set(Vector2d v) {
			SetX(v.X);
			SetY(v.Y);
			return this;
		}
	
		#region I_mathComponent
		//Add
		public Vector2d Add (Vector2d v) {
			SetX(X+v.X);
			SetY(Y+v.Y);
			return this;
		}
		public Vector2d Add (long n) {
			SetX(X+n);
			SetY(Y+n);
			return this;
		}
		I_mathComponent I_mathComponent.Add (long n) {
			return Add(n);
		}
		//subtract
		public Vector2d Subtract (long n) {
			SetX(X-n);
			SetY(Y-n);
			return this;
		}
		public Vector2d Subtract (Vector2d v) {
			SetX(X-v.X);
			SetY(Y-v.Y);
			return this;
		}
		I_mathComponent I_mathComponent.Subtract (long n) {
			return Subtract(n);
		}
		//multiply
		public Vector2d Multiply (long n) {
			SetX(X*n);
			SetY(Y*n);
			return this;
		}
		public Vector2d Multiply (Vector2d v) {
			SetX(X*v.X);
			SetY(Y*v.Y);
			return this;
		}
		I_mathComponent I_mathComponent.Multiply (long n) {
			return Multiply(n);
		}
		//divide
		public Vector2d Divide (long n) {
			if( n!=0 ) {
				SetX(X/n);
				SetY(Y/n);
			}
			return this;
		}
		public Vector2d Divide (Vector2d v) {
			if( v.X!=0 ) {
				SetX(X/v.X);
			}
			if( v.Y!=0 ) {
				SetY(Y/v.Y);
			}
			return this;
		}
		I_mathComponent I_mathComponent.Divide (long n) {
			return Divide(n);
		}
		//comparison
		public bool SmallerOrEqual (long n) {
			return X<=n&&Y<=n;
		}
		public bool SmallerOrEqual (Vector2d n) {
			return (X<=n.X&&Y<=n.Y);
		}
		public bool LargerOrEqual (long n) {
			return X>=n&&Y>=n;
		}
		public bool LargerOrEqual (Vector2d n) {
			return X>=n.X&&Y>=n.Y;
		}
		public object Clone ( ) {
			return Instance;
		}
		public int CompareTo (long other) {
			if( X==other&&Y==other )
				return 0;
			if( X>other&&Y>other )
				return 1;
			return -1;
		}
		public int CompareTo (Vector2d other) {
			if( X==other.X&&Y==other.Y )
				return 0;
			if( X>other.X&&Y>other.Y )
				return 1;
			return -1;
		}
		public bool Equals (Vector2d other) {
			return (other.X==X&&other.Y==Y);
		}
		public bool Equals (long other) {
			return X==other&&Y==other;
		}
		#endregion
	}

}
