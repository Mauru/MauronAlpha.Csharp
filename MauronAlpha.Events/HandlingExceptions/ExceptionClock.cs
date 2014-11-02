using System;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.HandlingExceptions {
	//The time of the active computer
	public sealed class ExceptionClock : MauronCode_eventClock {
		#region singleton
		private static volatile ExceptionClock instance=new ExceptionClock();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ExceptionClock () { }

		public static ExceptionClock Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ExceptionClock();
					}
				}
				return instance;
			}
		}

		//Is this clock the System Time
		public override bool IsExceptionClock { get { return true; } }
		public override bool IsSystemTime { get { return true; } }
		public override MauronCode_timeUnit Time {
			get {
				return new MauronCode_timeUnit(System.DateTime.Now.Ticks, Instance);
			}
		}

		#endregion
	}
}
