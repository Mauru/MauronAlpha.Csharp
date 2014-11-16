using System;

namespace MauronAlpha.Events.Units
{
    public class EventUnit_timeStamp:EventComponent_unit {

        public EventUnit_timeStamp(EventUnit_clock clock, long ticks):base() { 
            
        }

        private long INT_ticks= 0;
        public long Ticks { get {
            if (IsSystemTime) {
                return SystemClock.Ticks;
            }
            return INT_ticks;  } }

        private EventUnit_clock CLOCK_source;
        public EventUnit_clock Clock {
            get {
                return CLOCK_source;
            }
        }

        private bool B_isSystemTime = false;
        public bool IsSystemTime
        {
            get
            {
                return B_isSystemTime;
            }
        }
    }
}
