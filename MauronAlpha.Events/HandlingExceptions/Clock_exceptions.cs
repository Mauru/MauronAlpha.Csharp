using System;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.HandlingExceptions {
	//The time of the active computer
	public class Clock_exceptionHandler : MauronCode_eventClock {
		
		//constructor
		public Clock_exceptionHandler (Clock_systemTime systemTime ) : base(systemTime) {
			SetAsExeceptionCounter();
		}		

		//Is this clock the System Time
		public override bool IsExceptionClock { get { return true; } }
		public override bool IsSystemTime { get { return true; } }
	}
}
