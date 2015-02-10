using System;

using MauronAlpha.Interfaces;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;

namespace MauronAlpha.Layout.Layout2d.Position {

	//Proxy unit describing the size of a layout Object
	public class Layout2d_size : Layout2d_component,
	I_protectable<Layout2d_size>,
	IEquatable<Layout2d_size>,
	I_instantiable<Layout2d_size> {

		//constructor
		public Layout2d_size():base() {}
		public Layout2d_size (Vector2d size) : this() {
			V_size = size;
		}

		//As Vector
		private Vector2d V_size;
		public Vector2d AsVector {
			get {
				if( V_size==null )
					V_size = new Vector2d();
				return V_size.SetIsReadOnly(true);
			}
		}

		//Properties
		public double Height { 
			get {
				return V_size.Y;
			}
		}
		public double Width {
			get {
				return V_size.X;
			}
		}

		//Booleans
		public bool Equals(Layout2d_size other) {
			return V_size.Equals(other.AsVector);
		}
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		//Methods
        public Layout2d_size Instance { 
			get {
				Layout2d_size result = new Layout2d_size(AsVector);
				return result;
			}
		}
		public Layout2d_size SetIsReadOnly( bool status ) {
			B_isReadOnly = status;
			return this;
		}
		public Layout2d_size SetHeight(double n) {
			if(IsReadOnly)
				throw Error("Is protected!,(SetHeight)",this, ErrorType_protected.Instance);
			V_size.SetY(n);
			return this;
		}
		public Layout2d_size SetWidth (double n) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetWidth)", this, ErrorType_protected.Instance);
			V_size.SetX(n);
			return this;
		}

	}

}
