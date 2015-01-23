using MauronAlpha.HandlingData;

using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Collections;

namespace MauronAlpha.Layout.Layout2d.Units {	
	
	//A layout unit in 2d space
	public abstract class Layout2d_unit:Layout2d_component, I_layoutUnit {

		//constructor
		public Layout2d_unit(Layout2d_unitType unitType):base(){
			SUB_type=unitType;
		}

		//Unit type
		private Layout2d_unitType SUB_type;
		public virtual Layout2d_unitType UnitType {
			get {
				return SUB_type;
			}
		}

		public bool CanHaveParent { get {
			return UnitType.CanHaveParent;
		} }
		public bool CanHaveChildren { get {
			return UnitType.CanHaveChildren;
		} }
		public virtual bool Equals(Layout2d_unit other) {
			if(!UnitType.Equals(other.UnitType))
				return false;
			if(!Context.Equals(other.Context))
				return false;
			if(!Parent.Equals(other.Parent))
				return false;
			if(!Children.Equals(other.Children))
				return false;
			if(!EventHandler.Equals(other.EventHandler))
				return false;
			return true;
		}
		public bool Equals(I_layoutUnit other) {
			if( !UnitType.Equals(other.UnitType) )
				return false;
			if( !Context.Equals(other.Context) )
				return false;
			if( !Parent.Equals(other.Parent) )
				return false;
			if( !Children.Equals(other.Children) )
				return false;
			if( !EventHandler.Equals(other.EventHandler) )
				return false;
			return true;
		}

		public abstract bool HasParent { get; }
		public abstract bool HasChildren { get; }

		public abstract bool IsParent { get; }
		public abstract bool IsChild { get; }
		
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		public abstract Layout2d_unitCollection Children { get; }
		
		public abstract I_layoutUnit Parent { get; }
		public abstract I_layoutUnit ChildByIndex (int index);

		public abstract I_layoutUnit AddChildAtIndex (int index, I_layoutUnit unit);

		public abstract I_eventHandler EventHandler { get; }

		public abstract Layout2d_context Context {get;}
	}

}
