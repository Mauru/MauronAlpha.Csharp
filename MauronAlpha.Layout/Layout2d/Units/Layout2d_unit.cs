using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events;

using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Collections;

namespace MauronAlpha.Layout.Layout2d.Units {	
	
	//A layout unit in 2d space
	public abstract class Layout2d_unit : Layout2d_component, I_layoutUnit {

		//constructor
		public Layout2d_unit(Layout2d_unitType unitType):base(){
			SUB_type=unitType;
		}

		//Type description
		private Layout2d_unitType SUB_type;
		public virtual Layout2d_unitType UnitType {
			get {
				return SUB_type;
			}
		}

		private bool B_isReadOnly = false;
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

		private I_eventHandler EVENT_handler;
		public I_eventHandler EventHandler {
			get {
				if( EVENT_handler == null )
					EVENT_handler = new EventHandler();
				return EVENT_handler;
			}
		}

		public I_layoutUnit SetIsReadOnly( bool status ) {
			B_isReadOnly = status;
			return this;
		}
		public I_layoutUnit SetEventHandler(I_eventHandler handler) {
			if( IsReadOnly )
				throw Error( "Is protected!,(SetEventhandler)", this, ErrorType_protected.Instance );
				
			EVENT_handler = handler;
			return this;
		}
		public I_layoutUnit SetContext( Layout2d_context context ) {
			if( IsReadOnly )
				throw Error( "Is protected!,(SetContext)", this, ErrorType_protected.Instance );

				CONTEXT_layout = context;
			return this;
		}

		private Layout2d_context CONTEXT_layout;
		public Layout2d_context Context {
			get {
				if( CONTEXT_layout == null )
					CONTEXT_layout = new Layout2d_context();
				return CONTEXT_layout.Instance.SetIsReadOnly(true);
			}
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
				return LAYOUT_children.SetIsReadOnly(IsReadOnly);
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
			if(IsReadOnly)
				throw Error("Protected!,(SetParent)",this, ErrorType_protected.Instance);
			LAYOUT_parent = parent;
			return this;
		}
	
	}

}
