using System;

using MauronAlpha.Events.Units;
using MauronAlpha.Events.HandlingExceptions;
using MauronAlpha.Events.Singletons;

namespace MauronAlpha.Events {
	
	//The time of the active computer
	public class Clock_systemTime : MauronCode_eventClock {
		
		private SharedEventSystem SYSTEM_clocks;

		//constructor
		public Clock_systemTime(SharedEventSystem eventSystem):base(true) {
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
	
		public MauronCode_timeUnit TimeFor(MauronCode_eventClock clock) {
			return new MauronCode_timeUnit(System.DateTime.Now.Ticks,this);
		}
		public MauronCode_timeStamp TimeStampFor (MauronCode_eventClock clock) {
			return new MauronCode_timeStamp(this, TimeFor(clock));
		}

	}

}
