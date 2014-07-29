using System;

using MauronAlpha.Settings;

namespace MauronAlpha.Events {

	//The default Settings for an event shedule
	public sealed class MauronCode_eventShedule_defaults:MauronCode_defaultSettingsObject {
		#region singleton
			private static volatile MauronCode_eventShedule_defaults instance=new MauronCode_eventShedule_defaults();
			private static object syncRoot=new Object();
			//constructor singleton multithread safe
			static MauronCode_eventShedule_defaults ( ) { }
			public static MauronCode_defaultSettingsObject Instance {
				get {
					if( instance==null ) {
						lock( syncRoot ) {
							instance=new MauronCode_eventShedule_defaults();
							instance.SetName(MyName);
						}
					}
					instance.SetName(MyName);
					return instance;
				}
			}
			public static string MyName {
				get {
					return "eventShedule_defaults";
				}
			} 
			#endregion

		#region MauronCode_defaultSettingsObject
		public override string[] PropertyKeys {
			get { return new string[] {"Clock","Interval","LastChecked","LastExecuted"}; }
		}
		#endregion
	}
}
