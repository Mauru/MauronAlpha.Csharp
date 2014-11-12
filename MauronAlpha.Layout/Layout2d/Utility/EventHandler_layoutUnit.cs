using MauronAlpha.Events;
using MauronAlpha.Events.Units;

using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.Layout.Layout2d.Utility {
	
	//EventHandler
	public class Layout2d_eventHandler : Layout2d_component, I_eventHandler {

		//Constructor
		public Layout2d_eventHandler (Layout2d_unit unit, EventUnit_clock clock) {
			CLOCK_master = clock;
			UNIT_source = unit;
		}

		private Layout2d_unit UNIT_source;
		public Layout2d_unit Source {
			get {
				return UNIT_source;
			}
		}

		private EventUnit_clock CLOCK_master;
		public EventUnit_clock MasterClock {
			get {
				return CLOCK_master;
			}
		}

		public EventUnit_timeStamp TimeStamp { get { return CLOCK_master.TimeStamp; } }

		public EventUnit_time Time {
			get {
				return CLOCK_master.Time;
			}
		}

		#region explicit I_eventHandler
		EventUnit_clock I_eventHandler.MasterClock {
			get { return MasterClock; }
		}

		EventUnit_time I_eventHandler.Time {
			get { return Time; }
		}

		EventUnit_timeStamp I_eventHandler.TimeStamp {
			get { return TimeStamp; }
		}
		#endregion
	}
}
