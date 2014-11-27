using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Collections;

namespace MauronAlpha.Layout.Layout2d.Units {
	
	//A display container
	public class Layout2d_container:Layout2d_unit {

		//Constructor
		public Layout2d_container():base(UnitType_container.Instance) {
			
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
			get { throw new NotImplementedException(); }
		}

		public override bool IsReference {
			get { throw new NotImplementedException(); }
		}

		public override Layout2d_unitReference Instance {
			get { throw new NotImplementedException(); }
		}

		public override Layout2d_unit AddChildAtIndex (Layout2d_unitReference unit, int index) {
			throw new NotImplementedException();
		}

		public override int Index {
			get { throw new NotImplementedException(); }
		}

		public override Context.Layout2d_context Context {
			get { throw new NotImplementedException(); }
		}

		public override I_eventHandler EventHandler {
			get { throw new NotImplementedException(); }
		}
	}

	//Description
	public sealed class UnitType_container : Layout2d_unitType {
		#region singleton
		private static volatile UnitType_container instance=new UnitType_container();
		private static object syncRoot=new Object();
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