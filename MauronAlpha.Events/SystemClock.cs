using System;

namespace MauronAlpha.Events
{
    public static class SystemClock {

        public static long Ticks {
            get {
                return System.DateTime.UtcNow.Ticks;
            }
        }

    }
}
