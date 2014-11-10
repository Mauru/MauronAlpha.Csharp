using System;

using MauronAlpha.Events.HandlingExceptions;

namespace MauronAlpha.Events.Singletons {

	// Keeps track of the ExceptionHandler and SystemTime
	public sealed class SharedEventSystem : MauronCode_eventComponent {
		#region singleton
		private static volatile SharedEventSystem instance=new SharedEventSystem();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static SharedEventSystem ( ) { }
		public static SharedEventSystem Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new SharedEventSystem();
					}
				}
				return instance;
			}
		}
		#endregion

		Clock_exceptionHandler CLOCK_exceptionHandler;
		public Clock_exceptionHandler ExceptionHandler { get {
			if(CLOCK_exceptionHandler == null) {
				CLOCK_exceptionHandler = new Clock_exceptionHandler(SystemTime);
			}
			return CLOCK_exceptionHandler;
		} }
		Clock_systemTime CLOCK_systemTime;
		public Clock_systemTime SystemTime { 
			get {
				if(CLOCK_systemTime == null) {
					CLOCK_systemTime = new Clock_systemTime(this);
				}
				return CLOCK_systemTime;
			}
		}

	}
}
