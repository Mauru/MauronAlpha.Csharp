using MauronAlpha.Events.Interfaces;

namespace MauronAlpha.Events.Units {
    
	//A base object for an event
    public class EventUnit_event:EventComponent_unit {
		
		//constructor
		public EventUnit_event(I_eventSender sender, string code) {
			IE_sender = sender;
			STR_code = code;
		}

		//Let an event submit itself to an event clock
		public EventUnit_event Submit (EventUnit_clock clock) {
			clock.SubmitEvent(this);
			return this;
		}

		private I_eventSender IE_sender;
		public I_eventSender Sender {
			get {
				return IE_sender;
			}
		}
		public T SenderAs<T> ( ) {
			return (T) Sender;
		}
		private string STR_code;
		public string Code {
			get {
				return STR_code;
			}
		}

	}

}
