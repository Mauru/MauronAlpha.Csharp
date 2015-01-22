using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Collections;
using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Context;

namespace MauronAlpha.Layout.Layout2d.Units {
	
	//A display container
	public class Layout2d_container:Layout2d_unit, I_layoutUnit {

		//Constructor
		public Layout2d_container():base(UnitType_container.Instance) {}


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