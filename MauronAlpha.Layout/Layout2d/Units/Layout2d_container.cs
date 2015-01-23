using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Collections;
using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Context;

namespace MauronAlpha.Layout.Layout2d.Units {
	
	//A display container
	public class Layout2d_container:Layout2d_unit {

		//Constructor
		public Layout2d_container(I_layoutUnit parent):base(UnitType_container.Instance) {
			LAYOUT_parent = parent;
			EVENT_handler = new EventHandler(parent.EventHandler);

			Layout2d_context context = new Layout2d_context();
			LAYOUT_context = context;
		}

		//Properties
		private I_layoutUnit LAYOUT_parent;
		public override I_layoutUnit Parent {
			get {
				if( LAYOUT_parent==null )
					throw NullError("LAYOUT_parent can not be null!", this, typeof(I_layoutUnit));
				return LAYOUT_parent;
			}
		}
		private Layout2d_container SetParent(I_layoutUnit parent) {
			if(!parent.CanHaveChildren)
				throw Error("Invalid Parent!,{"+parent.UnitType.Name+"},(SetParent)",this,ErrorType_scope.Instance);
			LAYOUT_parent = parent;
			return this;
		}

		private bool B_isReadOnly = false;

		//Implementing Layout2d_unit
		public override bool HasParent {
			get { return (CanHaveParent && LAYOUT_parent!=null); }
		}
		public override bool HasChildren {
			get { return CanHaveChildren && !LAYOUT_children.IsEmpty; }
		}
		public override bool IsParent {
			get { return CanHaveChildren && !LAYOUT_children.IsEmpty; }
		}
		public override bool IsChild {
			get { return (LAYOUT_parent==null); }
		}
  
		private Layout2d_unitCollection LAYOUT_children=new Layout2d_unitCollection();
		public override Layout2d_unitCollection Children {
			get {
				return LAYOUT_children.Instance.SetIsReadOnly(true);
			}
		}

		public override I_layoutUnit ChildByIndex (int index) {
			if(!LAYOUT_children.IsEmpty || !LAYOUT_children.ContainsIndex(index)){
				throw Error("Invalid index!,{"+index+"},(ChildByIndex)",this,ErrorType_index.Instance);
			}
			return LAYOUT_children.UnitByIndex(index);
		}

		public override I_layoutUnit AddChildAtIndex (int index, I_layoutUnit unit) {
			if( LAYOUT_children.ContainsIndex(index) )
				throw Error("Unit allready has a child at index!,{"+index+"},(AddChildAtIndex)",this,ErrorType_index.Instance);
			LAYOUT_children.RegisterUnitAtIndex(index, unit);
			return this;
		}

		private Layout2d_context LAYOUT_context;
		public override Layout2d_context Context {
			get { return LAYOUT_context; }
		}

		private I_eventHandler EVENT_handler;
		public override I_eventHandler EventHandler {
			get {
				return EVENT_handler;
			}
		}
	}

	//Description
	public sealed class UnitType_container : Layout2d_unitType {
		#region singleton
		private static volatile UnitType_container instance=new UnitType_container();
		private static object syncRoot=new System.Object();

		//constructor singleton multithread safe
		static UnitType_container ( ) { }
		public static Layout2d_unitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new UnitType_container();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "container"; } }

		public override bool CanHaveChildren {
			get { return true; }
		}
		public override bool CanHaveParent {
			get { return true; }
		}
		public override bool CanHide {
			get { return true; }
		}
		public override bool IsDynamic {
			get { return true; }
		}
	}

}