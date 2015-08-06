using System;
using MauronAlpha.Interfaces;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Layout.Layout2d.Position {

	//A wrapper for a vector designating a Layout2d Object's Position
	public class Layout2d_position : Layout2d_component,
	I_protectable<Layout2d_position>,
	IEquatable<Layout2d_position>,
	I_instantiable<Layout2d_position> {

		//constructors
		public Layout2d_position( ) : base() {}
		public Layout2d_position( Vector2d vector ):this() {
			V_position = vector;
		}

		//Booleans
		public bool Equals( Layout2d_position other ) {
			return V_position.Equals( other.AsVector );
		}
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		public double Y {
			get {
				return V_position.Y;
			}
		}
		public double X {
			get {
				return V_position.X;
			}
		}

		//As String
		public string AsString {
			get { return V_position.AsString; }
		}

		//As Vector
		private Vector2d V_position = new Vector2d();
		public Vector2d AsVector {
			get {
				if( V_position==null )
					V_position=new Vector2d();
				return V_position.Instance.SetIsReadOnly(true);
			}
		}

		//Methods
		public Layout2d_position Instance { 
			get {
				Layout2d_position result = new Layout2d_position( AsVector );
				return result;
			}
		}
		public Layout2d_position SetIsReadOnly( bool status ) {
			B_isReadOnly = status;
			return this;
		}
	
	}

}
