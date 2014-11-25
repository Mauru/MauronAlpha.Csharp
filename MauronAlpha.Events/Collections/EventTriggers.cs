using MauronAlpha.HandlingData;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.Collections {
	
	//A class that holds triggers for Event Codes
	public class DataMap_EventTriggers:MauronCode_dataMap<EventHandler.DELEGATE_trigger> {

		public DataMap_EventTriggers():base(){}

		public EventHandler.DELEGATE_trigger DoNothing {
			get { return DELEGATE_doNothing; }
		}

		public static bool DELEGATE_doNothing (EventUnit_event unit) {
			return false;
		}
	
	}

}
