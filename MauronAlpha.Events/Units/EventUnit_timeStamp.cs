using System;

namespace MauronAlpha.Events.Units
{
    public class EventUnit_timeStamp:EventComponent_unit {

        public EventUnit_timeStamp(EventUnit_clock clock, long ticks):base() { 
            CLOCK_source = clock;
			INT_ticks = ticks;
        }
		public EventUnit_timeStamp(long ticks):base() {
			INT_ticks = ticks;
			B_isSystemTime = true;
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
        public bool IsSystemTime {
			get { return B_isSystemTime; }
        }
	
		public bool Equals(EventUnit_timeStamp other) {
			if(Ticks != other.Ticks)
				return false;
			if(IsSystemTime!=other.IsSystemTime)
				return false;
			if(IsSystemTime)
				return true;
			return Clock.Equals(other.Clock);
		}
    
	}
}
