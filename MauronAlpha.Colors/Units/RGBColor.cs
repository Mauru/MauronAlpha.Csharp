using System;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Colors.Units {

	public class RGBColor : MauronCode_colorComponent, 
	IEquatable<RGBColor>,
	I_instantiable<RGBColor>,
	I_protectable<RGBColor> {

		//Constructors
		public RGBColor ( ) : base() { }
		public RGBColor (int alpha, int red, int green, int blue)
			: this() {
			INT_alpha=alpha;
			INT_red=red;
			INT_green=green;
			INT_blue=blue;
		}
		public RGBColor (int red, int green, int blue) : this(100, red, green, blue) { }

		private int INT_alpha=100;
		public int Alpha {
			get {
				return INT_alpha;
			}
		}
		private int INT_red=0;
		public int Red {
			get { return INT_red; }
		}
		private int INT_green=0;
		public int Green {
			get {
				return INT_green;
			}
		}
		private int INT_blue=0;
		public int Blue {
			get {
				return INT_blue;
			}
		}

		//Booleans
		public bool Equals (RGBColor other) {
			return INT_red==other.Red
			&&INT_green==other.Green
			&&INT_blue==other.Blue
			&&INT_alpha==other.Alpha;
		}
		private bool B_isReadOnly=false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		public bool IsTransparent {
			get {
				return INT_alpha==0;
			}
		}
		public bool IsSolid {
			get {
				return INT_alpha==100;
			}
		}

		//Methods
		public RGBColor Instance {
			get {
				return new RGBColor(INT_alpha, INT_red, INT_green, INT_blue);
			}
		}
		public RGBColor SetIsReadOnly (bool state) {
			B_isReadOnly=state;
			return this;
		}
		public RGBColor SetAlpha (int alpha) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetAlpha)", this, ErrorType_protected.Instance);
			INT_alpha=alpha;
			return this;
		}
		public RGBColor SetRed (int n) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetRed)", this, ErrorType_protected.Instance);
			INT_red=n;
			return this;
		}
		public RGBColor SetGreen (int n) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetGreen)", this, ErrorType_protected.Instance);
			INT_green=n;
			return this;
		}
		public RGBColor SetBlue (int n) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetBlue)", this, ErrorType_protected.Instance);
			INT_blue=n;
			return this;
		}

	}

}
