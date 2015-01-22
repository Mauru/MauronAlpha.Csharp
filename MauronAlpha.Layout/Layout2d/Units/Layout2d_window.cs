using System;

using MauronAlpha.Layout.Layout2d.Collections;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.HandlingErrors;

using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Units {
	
	//Describes a window
	public class Layout2d_window : Layout2d_unit, I_layoutUnit {

		//constructor
		public Layout2d_window(string name, I_layoutController controller):base(UnitType_window.Instance) {
			STR_name = name;
			I_eventHandler handler = new MauronAlpha.Events.EventHandler( controller.EventHandler);
			SetEventHandler( handler );
		}

		private string STR_name;
		public string Name {
			get {
				return STR_name;
			}
		}
	}

	//Description
	public sealed class UnitType_window : Layout2d_unitType {
		#region singleton
		private static volatile UnitType_window instance=new UnitType_window();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static UnitType_window ( ) { }
		public static Layout2d_unitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new UnitType_window();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "window"; } }

		public override bool CanHaveChildren {
			get { return true; }
		}

		public override bool CanHaveParent {
			get { return false; }
		}

		public override bool CanHide {
			get { return false; }
		}

		public override bool IsDynamic {
			get { return true; }
		}
	}

}
