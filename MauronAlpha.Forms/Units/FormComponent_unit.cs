using MauronAlpha.HandlingErrors;

using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Collections;

namespace MauronAlpha.Forms.Units {

	//Base Class for a form component
	public abstract class FormComponent_unit : MauronCode_formComponent, I_layoutUnit {
		//Event Handler
		protected I_eventHandler HANDLER_events;
		public virtual I_eventHandler EventHandler {
			get {
				if( HANDLER_events == null )
					throw NullError( "HANDLER_events can not be null!,(EventHandler)", this, typeof( I_eventHandler ) );
				return HANDLER_events;
			}
		}

		//Parent object
		protected Layout2d_unitReference LAYOUT_parent;
		public virtual Layout2d_unitReference Parent {
			get {
				if( LAYOUT_parent == null ) {
					throw NullError( "LAYOUT_parent can not be null!,(Parent)", this, typeof( Layout2d_unitReference ) );
				}
				return LAYOUT_parent;
			}
		}

		//Children
		protected Layout2d_unitCollection LAYOUT_children = new Layout2d_unitCollection();
		public virtual Layout2d_unitCollection Children {
			get {
				return LAYOUT_children;
			}
		}

		//Unit Description
		protected Layout2d_unitType LAYOUT_unitType;
		public virtual Layout2d_unitType UnitType {
			get {
				return LAYOUT_unitType;
			}
		}

		//Pass unit as a Reference
		public virtual Layout2d_unitReference AsReference {
			get {
				return new Layout2d_unitReference( EventHandler, this );
			}
		}

		//The Context of the unit
		protected Layout2d_context LAYOUT_context;
		public virtual Layout2d_context Context {
			get {
				if( LAYOUT_context == null )
					throw NullError( "LAYOUT_context can not be null!,(Context)", this, typeof( Layout2d_context ) );
				return LAYOUT_context;
			}
		}

		//Booleans
		protected bool B_isReadOnly = false;
		protected bool B_isStatic = false;
		public virtual bool IsStatic {
			get {
				return B_isStatic;
			}
		}
		public virtual bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public bool HasChildren {
			get {
				return false;
			}
		}
		public bool HasParent {
			get {
				return LAYOUT_parent==null;
			}
		}
		public bool IsParent {
			get {
				return LAYOUT_children.Count>0;
			}
		}
		public bool IsChild {
			get {
				return LAYOUT_parent.Exists;
			}
		}
		public virtual bool IsDynamic {
			get {
				return true;
			}
		}
		public virtual bool CanHaveParent {
			get {
				return true;
			}
		}
		public virtual bool CanHaveChildren {
			get {
				return false;
			}
		}
		public virtual bool Exists {
			get {
				return true;
			}
		}

		// Adding Children
		public virtual I_layoutUnit AddChildAtIndex( int index, Layout2d_unitReference unit ) {
			if( IsReadOnly ) {
				throw Error( "Is protected!,(AddChildAtIndex)", this, ErrorType_protected.Instance );
			}
			LAYOUT_children.RegisterUnitAtIndex( index, unit );
			return this;
		}
		public virtual Layout2d_unitReference ChildByIndex( int index ) {
			if( !Children.ContainsIndex( index ) ) {
				throw Error( "Index does not exist!,{"+index+"},(ChildByIndex)", this, ErrorType_index.Instance );
			}
			return Children.UnitByIndex( index );
		}
	}

}
