using System;
using MauronAlpha.Mathematics;
using MauronAlpha.HandlingErrors;



namespace MauronAlpha.Geometry.Geometry2d.Units {

	//coordinates
	public class Vector2d : GeometryComponent2d_unit, I_mathComponent {

		#region Constructors
		public Vector2d ( ) { }
		public Vector2d (double x, double y) {
			SetX(x);
			SetY(y);
		}
		public Vector2d (double n) : this(n, n) { }
		public Vector2d (Vector2d p) : this(p.X, p.Y) { }
		#endregion

		public bool IsZero {
			get {
				return (INT_x == 0 && INT_y == 0);
			}
		}

		#region Instancing, Cloning
		public Vector2d Instance {
			get {
				return new Vector2d( this );
			}
		}
		public Vector2d Cloned {
			get {
				return new Vector2d( this );
			}
		}
		Object ICloneable.Clone() {
			return new Vector2d( this );
		}
		#endregion

		#region Coordinates
		#region X
		protected double INT_x=0;
		public double X { get { return INT_x; } }
		public Vector2d SetX(double x) {
			INT_x= x;
			return this;
		}
		#endregion
		#region Y
		protected double INT_y=0;
		public double Y { get { return INT_y; } }
		public Vector2d SetY (double y) {
			INT_y=y;
			return this;
		}
		#endregion
		#endregion
		#region Set the point
		public Vector2d Set (double x, double y) {
			#region ReadOnly Check
			if( IsReadOnly ) {
                throw Error("Is protected!,(Set)", this, ErrorType_protected.Instance);
			}
			#endregion
			SetX(x);
			SetY(y);
			return this;
		}
		public Vector2d Set (Vector2d v) {
			#region ReadOnly Check
			if( IsReadOnly ) {
                throw Error("Is protected!,(Set)", this, ErrorType_protected.Instance);
			}
			#endregion
			SetX(v.X);
			SetY(v.Y);
			return this;
		}
		#endregion

		#region Set ReadOnlyStatus
		private bool B_isReadOnly=false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public Vector2d SetIsReadOnly(bool status) {
			B_isReadOnly=status;
			return this;
		}
		#endregion

		#region Modified Instances

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
			get { return (Vector2d) Instance.Multiply(-1); }
		}
	
		#endregion

		//string
		public string AsString {
			get {
				return "["+X+"|"+Y+"]";
			}
		}

		#region Transformation
		//stretch or compress around a point
		public Vector2d Transform(Vector2d v, Vector2d s){
			#region ReadOnly Check
			if( IsReadOnly ) {
                throw Error("Is protected!,(Transform)", this, ErrorType_protected.Instance);
			}
			#endregion
			Vector2d p = Difference(v);
			p.Multiply(s);
			Add(p);
			return this;
		}
		
		//stretch or compress by [-1+] around a point
		public Vector2d Transform (Vector2d v, Double n) {
			#region ReadOnly Check
			if( IsReadOnly ) {
                throw Error("Is protected!,(Transform)", this, ErrorType_protected.Instance);
			}
			#endregion
			Vector2d p=Difference(v);
			p.Multiply(new Vector2d(n));
			Add(p);
			return this;
		}
		
		//Mirror this point around another
		public Vector2d Mirror (Vector2d v) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("Is protected!,(Mirror)", this, ErrorType_protected.Instance);
			}
			#endregion
			Vector2d p=(Vector2d) Difference(v).Multiply(-2);
			return Add(p);
		}
		
		//rotate the point around another
		public Vector2d Rotate (Vector2d v, double angle) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("Is protected!,(Rotate)", this, ErrorType_protected.Instance);
			}
			#endregion
			double s=Math.Sin(angle);
			double c=Math.Cos(angle);

			Subtract(v);

			SetX(v.X*c-v.Y*s);
			SetY(v.X*s+v.Y*c);

			Add(v);
			return this;
		}
		#endregion

		#region Math Operations
		//Add
		public Vector2d Add (Vector2d v) {
			#region ReadOnly Check
			if( IsReadOnly ) {
                throw Error("Is protected!,(Add)", this, ErrorType_protected.Instance);
			}
			#endregion
			SetX(X+v.X);
			SetY(Y+v.Y);
			return this;
		}
		public Vector2d Add (long n) {
			#region ReadOnly Check
			if( IsReadOnly ) {
                throw Error("Is protected!,(Add)", this, ErrorType_protected.Instance);
			}
			#endregion
			SetX(X+n);
			SetY(Y+n);
			return this;
		}
		//subtract
		public Vector2d Subtract (long n) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("Is protected!,(Subtract)", this, ErrorType_protected.Instance);
			}
			#endregion
			SetX(X-n);
			SetY(Y-n);
			return this;
		}
		public Vector2d Subtract (Vector2d v) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("Is protected!,(Subtract)", this, ErrorType_protected.Instance);
			}
			#endregion
			SetX(X-v.X);
			SetY(Y-v.Y);
			return this;
		}
        public Vector2d Subtract(long x, long y)
        {
            #region ReadOnly Check
            if (IsReadOnly)
            {
                throw Error("Is protected!,(Subtract)", this, ErrorType_protected.Instance);
            }
            #endregion
            SetX(X - x);
            SetY(Y - y);
            return this;
        }
		
        //multiply
		public Vector2d Multiply (long n) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("Is protected!,(Multiply)", this, ErrorType_protected.Instance);
			}
			#endregion
			SetX(X*n);
			SetY(Y*n);
			return this;
		}
		public Vector2d Multiply (Vector2d v) {
			#region ReadOnly Check
			if( IsReadOnly ) {
                throw Error("Is protected!,(Multiply)", this, ErrorType_protected.Instance);
			}
			#endregion
			SetX(X*v.X);
			SetY(Y*v.Y);
			return this;
		}
		//divide
		public Vector2d Divide (long n) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("Is protected!,(Divide)", this, ErrorType_protected.Instance);
			}
			#endregion
			if( n!=0 ) {
				SetX(X/n);
				SetY(Y/n);
			}
			return this;
		}
		public Vector2d Divide (Vector2d v) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				throw Error("Is protected!,(Divide)", this, ErrorType_protected.Instance);
			}
			#endregion
			if( v.X!=0 ) {
				SetX(X/v.X);
			}
			if( v.Y!=0 ) {
				SetY(Y/v.Y);
			}
			return this;
		}
		#endregion
		
		#region Comparison
		//boolean
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
		public bool Equals (Vector2d other) {
			return (other.X==X&&other.Y==Y);
		}
		public bool Equals (long other) {
			return X==other&&Y==other;
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

		public static int Compare( Vector2d source, Vector2d other ) {
			return source.CompareTo( other );
		}
		#endregion

		#region I_mathComponent
		I_mathComponent I_mathComponent.Add (long n) {
			return Add(n);
		}
		I_mathComponent I_mathComponent.Subtract (long n) {
			return Subtract(n);
		}
		I_mathComponent I_mathComponent.Multiply (long n) {
			return Multiply(n);
		}
		I_mathComponent I_mathComponent.Divide (long n) {
			return Divide(n);
		}
		#endregion

	}

}
