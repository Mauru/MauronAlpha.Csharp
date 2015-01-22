using System;
using MauronAlpha.Interfaces;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;

using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Context {
	
	//A class describing A LayoutUnit's Position in the Action/Reaction-dependency tree	
	public class Layout2d_context:Layout2d_component, IEquatable<Layout2d_context>,I_instantiable<Layout2d_context> {

		//constructors
		public Layout2d_context():base() {}
		public Layout2d_context( Vector2d position, Vector2d size) : base() {
			CONTEXT_position = new Layout2d_position( position );
			CONTEXT_size = new Layout2d_size( size );
		}

		private Layout2d_size CONTEXT_size;
		public Layout2d_size Size {
			get {
				if( CONTEXT_size==null )
					CONTEXT_size = new Layout2d_size();
				return CONTEXT_size.Instance.SetIsReadOnly(true);
			}
		}

		private Layout2d_position CONTEXT_position;
		public Layout2d_position Position {
			get {
				if( CONTEXT_position==null )
					CONTEXT_position = new Layout2d_position();
				return CONTEXT_position.Instance.SetIsReadOnly(true);
			}
		}

		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public bool Equals( Layout2d_context other ) {
			if( !Position.Equals( other.Position ) )
				return false;
			if( !Size.Equals( other.Position ) )
				return false;
			return true;
		}
		
		public Layout2d_context SetIsReadOnly( bool state ) {
			B_isReadOnly = state;
			return this;
		}

		public Layout2d_context Instance {
			get {
				return new Layout2d_context( Position.AsVector, Size.AsVector );
			}
		}
	}

}