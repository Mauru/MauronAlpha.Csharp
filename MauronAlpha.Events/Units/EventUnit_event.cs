using MauronAlpha.Events.Interfaces;

namespace MauronAlpha.Events.Units {
    
	//A base object for an event
    public class EventUnit_event:EventComponent_unit {
		
		//constructor
		public EventUnit_event(string code) {
			STR_code = code;
		}

		private string STR_code;
		public string Code {
			get {
				return STR_code;
			}
		}

	}

}
