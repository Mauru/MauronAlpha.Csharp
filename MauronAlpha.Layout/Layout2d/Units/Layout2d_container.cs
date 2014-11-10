using System;

namespace MauronAlpha.Layout.Layout2d.Units {
	
	//A display container
	public class Layout2d_container:Layout2d_unit {

		//Constructor
		public Layout2d_container():base(UnitType_container.Instance) {
		
		}



		public override bool Exists {
			get { throw new NotImplementedException(); }
		}

		public override bool CanHaveParent {
			get { return true; }
		}

		public override bool CanHaveChildren {
			get { return true; }
		}

		public override bool HasParent {
			get { throw new NotImplementedException(); }
		}

		public override bool HasChildren {
			get { throw new NotImplementedException(); }
		}

		public override bool IsDynamic {
			get { throw new NotImplementedException(); }
		}

		public override bool IsParent {
			get { throw new NotImplementedException(); }
		}

		public override bool IsChild {
			get { throw new NotImplementedException(); }
		}

		public override HandlingData.MauronCode_dataIndex<Layout2d_unitReference> Children {
			get { throw new NotImplementedException(); }
		}

		public override Layout2d_unitReference Parent {
			get { throw new NotImplementedException(); }
		}

		public override Layout2d_unitReference ChildByIndex (int index) {
			throw new NotImplementedException();
		}

		public override Layout2d_unitReference AsReference {
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

		public override Utility.Layout2d_eventHandler EventHandler {
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
			get { return false; }
		}

		public override bool CanHaveParent {
			get { return false; }
		}

		public override bool CanHide {
			get { return true; }
		}

		public override bool IsDynamic {
			get { return true; }
		}
	}
}
