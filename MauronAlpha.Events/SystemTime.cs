using System;

using MauronAlpha.Events.Units;

namespace MauronAlpha.Events {
	
	//The time of the active computer
	public sealed class SystemTime:MauronCode_eventClock {
		#region singleton
		private static volatile SystemTime instance=new SystemTime();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static SystemTime ( ) { }
		public static MauronCode_eventClock Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new SystemTime();
					}
				}
				return instance;
			}
		}

		//Is this clock the System Time
		public override bool IsSystemTime { get { return true; } }
		public override MauronCode_timeUnit Time { get {
			return new MauronCode_timeUnit(System.DateTime.Now.Ticks, Instance);
		} }
		public override MauronCode_timeStamp TimeStamp { get {
			return new MauronCode_timeStamp(this,Time);
		} }

		#endregion
	}

}
