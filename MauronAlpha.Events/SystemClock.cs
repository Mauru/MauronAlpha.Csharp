using System;

using MauronAlpha.Events.Units;

namespace MauronAlpha.Events
{
    public static class SystemClock {

        public static long Ticks {
            get {
                return System.DateTime.UtcNow.Ticks;
            }
        }

		public static EventUnit_timeStamp TimeStamp {
			get {
				return new EventUnit_timeStamp(Ticks);
			}
		}

    }
}
