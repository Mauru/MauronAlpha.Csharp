using System;

using MauronAlpha.Events.Units;
using MauronAlpha.Events.HandlingExceptions;
using MauronAlpha.Events.Singletons;

namespace MauronAlpha.Events {
	
	//The time of the active computer
	public class Clock_systemTime : MauronCode_eventComponent {
		
		private SharedEventSystem SYSTEM_clocks;

		//constructor
		public Clock_systemTime(SharedEventSystem eventSystem):base() {
			SYSTEM_clocks = eventSystem;
		}

		//generate a exceptionhandler
		private Clock_exceptionHandler CLOCK_exceptions;
		public Clock_exceptionHandler ExceptionHandler {
			get {
				if(CLOCK_exceptions == null) {
					CLOCK_exceptions = new Clock_exceptionHandler(this);
				}
				return CLOCK_exceptions;
			}
		}

		public static long Ticks {
			get {
				return System.DateTime.Now.Ticks;
			}
		}

		public static EventUnit_timeStamp TimeStamp {
			get {
				return new EventUnit_timeStamp(Time);
			}
		}
		public static EventUnit_time Time {
			get {
				return new EventUnit_time(Ticks);
			}
		}
	
		public static Clock_systemTime Instance {
			get {
				return new Clock_systemTime(SharedEventSystem.Instance);
			}
		}

	}

}
