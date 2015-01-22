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

		private Layout2d_unitType SUB_type;
		public virtual Layout2d_unitType UnitType {
			get {
				return SUB_type;
			}
		}

		public abstract bool Exists { get; }

		public abstract bool CanHaveParent { get; }
		public abstract bool CanHaveChildren { get; }

		public abstract bool HasParent { get; }
		public abstract bool HasChildren { get; }

		public abstract bool IsDynamic { get; }
		public abstract bool IsParent { get; }
		public abstract bool IsChild { get; }
		public abstract bool IsReference { get; }
		public abstract bool IsReadOnly { get; }
        public abstract bool IsStatic { get; }

		public abstract Layout2d_unitCollection Children { get; }
		
		public abstract Layout2d_unitReference Parent { get; }
		public abstract Layout2d_unitReference ChildByIndex (int index);
		public abstract Layout2d_unitReference AsReference { get; }

		public abstract Layout2d_unitReference Instance { get; }
		
		public abstract I_layoutUnit AddChildAtIndex (int index, I_layoutUnit unit);
		public virtual I_layoutUnit AsOriginal {
			get {
				return this;
			}
		}

		public abstract I_eventHandler EventHandler { get; }

		public abstract Layout2d_context Context {get;}
	}

}
