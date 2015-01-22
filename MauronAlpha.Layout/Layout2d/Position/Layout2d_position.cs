using System;
using MauronAlpha.Interfaces;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Layout.Layout2d.Position {

	//A Wrapper for The Geometry class
	public class Layout2d_position : Layout2d_component, I_protectable<Layout2d_position>,IEquatable<Layout2d_position>,I_instantiable<Layout2d_position> {

		//constructor
		public Layout2d_position( ) : base() {}

		public Layout2d_position( Vector2d vector ):this() {
			V_position = vector;
		}

		private Vector2d V_position;
		public Vector2d AsVector {
			get {
				if( V_position==null )
					V_position = new Vector2d();
				return V_position.Instance.SetIsReadOnly(true);
			}
		}

		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		public Layout2d_position Instance {
			get {
				return new Layout2d_position( AsVector );
			}
		}
		public Layout2d_position SetIsReadOnly( bool status ) {
			B_isReadOnly = status;
			return this;
		}

		public bool Equals( Layout2d_position other ) {
			return V_position.Equals( other.AsVector );
		}

	}

}
