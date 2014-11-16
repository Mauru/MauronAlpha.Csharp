using System;


namespace MauronAlpha.Events.Units
{
    //A Base element of the event system
    public class EventUnit_clock:EventComponent_unit {


        public EventUnit_clock():base() {}

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
            if (TIME_created.Ticks != other.TIME_created.Ticks)
                return false;
            return EventHandler.Subscriptions.Equals(other.EventHandler.Subscriptions);
        }

        private bool B_isSystemTime;
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



    }

}
