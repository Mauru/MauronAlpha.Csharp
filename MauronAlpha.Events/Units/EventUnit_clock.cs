using System;
using MauronAlpha.Events.Interfaces;

namespace MauronAlpha.Events.Units {
    
	//A Base element of the event system
    public class EventUnit_clock:EventComponent_unit {

		//constructor
        public EventUnit_clock():base() {
			TIME_created = new EventUnit_timeStamp(SystemClock.Ticks);
			HANDLER_events = new EventHandler(this);
		}

        private EventHandler HANDLER_events;
        public EventHandler EventHandler {
            get {
                if (HANDLER_events == null) {
                    HANDLER_events = new EventHandler(this);
                }
                return HANDLER_events;
            }
        }

        public bool Equals(EventUnit_clock other) {
			if( IsSystemTime != other.IsSystemTime)
				return false;	
            if (TIME_created.Ticks != other.TIME_created.Ticks)
                return false;
            return EventHandler.Subscriptions.Equals(other.EventHandler.Subscriptions);
        }

        private bool B_isSystemTime=true;
        public bool IsSystemTime {
            get {
                return B_isSystemTime;
            }
        }

        private long INT_ticks=0;

        public long Ticks { get {
            if (IsSystemTime) {
                return SystemClock.Ticks;
            }
            return INT_ticks;
        } }

        public EventUnit_timeStamp TimeStamp {
            get { return new EventUnit_timeStamp(this, Ticks); }
        }
        public EventUnit_timeStamp TIME_created;

		public EventUnit_clock SubmitEvent(EventUnit_event e, I_eventSender sender) {
			EventHandler.CheckForTrigger(e, sender, TimeStamp);
			return this;
		}
   
	}

}
