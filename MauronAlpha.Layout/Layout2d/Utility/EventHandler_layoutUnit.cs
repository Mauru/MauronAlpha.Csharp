using MauronAlpha.Events;
using MauronAlpha.Events.Units;

using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.Layout.Layout2d.Utility {
	
	//EventHandler
	public class Layout2d_eventHandler : Layout2d_component, I_eventHandler {

		//Constructor
		public Layout2d_eventHandler (Layout2d_unit unit, MauronCode_eventClock clock) {
			CLOCK_master = clock;
			UNIT_source = unit;
		}

		private Layout2d_unit UNIT_source;
		public Layout2d_unit Source {
			get {
				return UNIT_source;
			}
		}

		private MauronCode_eventClock CLOCK_master;
		public MauronCode_eventClock MasterClock {
			get {
				return CLOCK_master;
			}
		}

		public MauronCode_timeStamp TimeStamp { get { return CLOCK_master.TimeStamp; } }

		public MauronCode_timeUnit Time {
			get {
				return CLOCK_master.Time;
			}
		}

		#region explicit I_eventHandler
		MauronCode_eventClock I_eventHandler.MasterClock {
			get { return MasterClock; }
		}

		MauronCode_timeUnit I_eventHandler.Time {
			get { return Time; }
		}

		MauronCode_timeStamp I_eventHandler.TimeStamp {
			get { return TimeStamp; }
		}
		#endregion
	}
}
