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

		//Unit Description
		protected Layout2d_unitType LAYOUT_unitType;
		public virtual Layout2d_unitType UnitType {
			get {
				return LAYOUT_unitType;
			}
		}

		//The Context of the unit
		protected Layout2d_context LAYOUT_context;
		public virtual Layout2d_context Context {
			get {
				if(LAYOUT_context == null)
					LAYOUT_context = new Layout2d_context();
				return LAYOUT_context.SetIsReadOnly(IsReadOnly);
			}
		}

		//Booleans
		protected bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public bool Equals( I_layoutUnit other ) {
			if( !UnitType.Equals( other.UnitType ) )
				return false;
			if( !Context.Equals( other.Context ) )
				return false;
			if( !EventHandler.Equals( other.EventHandler ) )
				return false;
			if( CanHaveChildren )
				return Children.Equals( other.Children );
			if( CanHaveParent )
				return Parent.Equals( other.Parent );
			return true;
		}

		public I_layoutUnit SetEventHandler( I_eventHandler handler ) {
			HANDLER_events = handler;
			return this;
		}
		public I_layoutUnit SetContext( Layout2d_context context ) {
			LAYOUT_context = context;
			return this;
		}
		public I_layoutUnit SetIsReadOnly( bool state ) {
			B_isReadOnly = state;
			return this;
		}

		public bool CanHaveChildren {
			get {
				return UnitType.CanHaveChildren;
			}
		}
		public bool CanHaveParent {
			get {
				return UnitType.CanHaveParent;
			}
		}

		private Layout2d_unitCollection LAYOUT_children;
		public Layout2d_unitCollection Children {
			get {
				if( !CanHaveChildren )
					return new Layout2d_unitCollection();
				if( LAYOUT_children==null )
					LAYOUT_children = new Layout2d_unitCollection();
				return LAYOUT_children.SetIsReadOnly( IsReadOnly );
			}
		}

		private I_layoutUnit LAYOUT_parent;
		public I_layoutUnit Parent {
			get {
				if( !CanHaveParent )
					throw Error( "Unit can not have a parent!", this, ErrorType_scope.Instance );
				return LAYOUT_parent;
			}
		}
		public I_layoutUnit SetParent( I_layoutUnit parent ) {
			if( !parent.CanHaveChildren )
				throw Error( "Unit can not have any children!,(SetParent)", this, ErrorType_scope.Instance );
			if( IsReadOnly )
				throw Error( "Protected!,(SetParent)", this, ErrorType_protected.Instance );
			LAYOUT_parent = parent;
			return this;
		}

	}

}
