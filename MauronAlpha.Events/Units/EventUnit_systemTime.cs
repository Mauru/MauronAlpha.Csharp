using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.Events.Units {
	
	public class EventUnit_systemTime : EventUnit_time {

		//constructor
		public EventUnit_systemTime(long ticks):base() {
			base.SetTicks(ticks);
			base.SetClock(Clock_systemTime.Instance);	
		}


		
	}

}
