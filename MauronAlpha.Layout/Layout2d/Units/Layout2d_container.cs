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
		public Layout2d_container(I_layoutParent parent):base(UnitType_container.Instance) {
			LAYOUT_parent = parent.AsReference;
			EVENT_handler = new EventHandler(parent.EventHandler);

			Layout2d_context context = new Layout2d_context(LAYOUT_parent);
			LAYOUT_context = context;
		}

		//Properties
		private Layout2d_unitReference LAYOUT_parent;
		public override Layout2d_unitReference Parent {
			get {
				if( LAYOUT_parent==null ) {
					throw NullError("LAYOUT_parent can not be null!", this, typeof(Layout2d_unitReference));
				}
				return LAYOUT_parent;
			}
		}
		private Layout2d_container SetParent(Layout2d_unitReference parent) {
			LAYOUT_parent = parent;
			return this;
		}

		private bool B_isReadOnly = false;

		//Implementing Layout2d_unit
		public override bool Exists {
			get { return true; }
		}
		public override bool CanHaveParent {
			get { return true; }
		}
		public override bool CanHaveChildren {
			get { return true; }
		}
		public override bool HasParent {
			get { return (LAYOUT_parent!=null && LAYOUT_parent.Exists); }
		}
		public override bool HasChildren {
			get { return LAYOUT_children.IsEmpty; }
		}
		public override bool IsDynamic {
			get { return true; }
		}
		public override bool IsParent {
			get { return !LAYOUT_children.IsEmpty; }
		}
		public override bool IsChild {
			get { return (LAYOUT_parent==null); }
		}
		public override bool IsReference {
			get { return false; }
		}
		public override bool IsReadOnly {
			get { return B_isReadOnly; }
		}


		private Layout2d_unitCollection LAYOUT_children=new Layout2d_unitCollection();
		public override Layout2d_unitCollection Children {
			get {
				return LAYOUT_children.Instance.SetIsReadOnly(true);
			}
		}

		public override Layout2d_unitReference ChildByIndex (int index) {
			if(!LAYOUT_children.IsEmpty || !LAYOUT_children.ContainsIndex(index)){
				throw Error("Invalid index!,{"+index+"},(ChildByIndex)",this,ErrorType_index.Instance);
			}
			return LAYOUT_children.UnitByIndex(index);
		}
		public override Layout2d_unitReference AsReference {
			get { return new Layout2d_unitReference(this.EventHandler, this); }
		}
		public override Layout2d_unitReference Instance {
			get { 
				return AsReference;
			}
		}

		public override Layout2d_unit AddChildAtIndex (Layout2d_unitReference unit, int index) {
			if( LAYOUT_children.ContainsIndex(index) ) {
				throw Error("Unit allready has a child at index!,{"+index+"},(AddChildAtIndex)",this,ErrorType_index.Instance);
			}
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