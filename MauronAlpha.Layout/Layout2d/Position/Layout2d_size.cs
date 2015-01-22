using System;
using MauronAlpha.Interfaces;

using MauronAlpha.Geometry.Geometry2d.Units;
using MauronAlpha.Geometry.Geometry2d.Shapes;

namespace MauronAlpha.Layout.Layout2d.Position {

	//Proxy unit describing the size of a layout Object
	public class Layout2d_size:Layout2d_component, I_protectable<Layout2d_size>,IEquatable<Layout2d_size>, I_instantiable<Layout2d_size> {

		//constructor
		public Layout2d_size():base() {}
		public Layout2d_size (Vector2d size) : this() {
			V_size = size;
		}

		private Vector2d V_size;
		public Vector2d AsVector {
			get {
				if( V_size==null )
					V_size = new Vector2d();
				return V_size.Instance.SetIsReadOnly(true);
			}
		}

		public Layout2d_size Instance { get { return new Layout2d_size(AsVector); } }
		public Layout2d_size SetIsReadOnly( bool status ) {
			B_isReadOnly = status;
			return this;
		}

		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		public bool Equals(Layout2d_size other) {
			return V_size.Equals( other.AsVector );
		}
	}

}
