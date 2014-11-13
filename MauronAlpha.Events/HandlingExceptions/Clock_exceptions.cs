using System;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.HandlingExceptions {
	//The time of the active computer
	public class Clock_exceptionHandler : EventUnit_clock {
		
		//constructor
		public Clock_exceptionHandler (Clock_systemTime systemTime ) : base() {
			/*TIME_created = TimeStamp;
			B_isSystemTime = true;
			B_isExceptionHandler = true;
			UTILITY_precision = new EventUtility_precision(EventPrecisionRuleSet.ExceptionHandler);

			return this;*/
		}		

		//Is this clock the System Time
		public override bool IsExceptionClock { get { return true; } }
		public override bool IsSystemTime { get { return true; } }
	}
}
