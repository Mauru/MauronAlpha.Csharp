using System;
using MauronAlpha.Interfaces;
using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;

namespace MauronAlpha.Layout.Layout2d.Position {

	//Proxy unit describing the size of a layout Object
	public class Layout2d_size:Layout2d_component,IEquatable<Layout2d_size>,I_protectable<Layout2d_size>,I_instantiable<Layout2d_size> {

		//constructor
		public Layout2d_size() {}
		public Layout2d_size (Vector2d size)
			: base() {

		}


		private Vector2d V_size = new Vector2d();
		public Vector2d AsVector { get {
			return V_size.Instance.SetIsReadOnly(true);
		} }

		public string AsString {
			get {
				return AsVector.AsString;
			}
		}

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

		public bool Equals(Layout2d_size other) {
			return AsVector.Equals(other.AsVector);
		}

		private bool B_isReadOnly=false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		
        public Layout2d_size SetIsReadOnly (bool b_isReadOnly) {
			B_isReadOnly=b_isReadOnly;
			return this;
		}
        public Layout2d_size Instance { get {
            Layout2d_size result = new Layout2d_size(AsVector);
            return result;
        } }
	}

}
