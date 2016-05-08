using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;

namespace MauronAlpha.Events.Units {
	
	public class Subscription<T>:MauronCode_eventComponent where T:EventUnit_event {

		I_subscriber<T> subscriber;

		public Subscription(I_subscriber<T> source) :base() {
			subscriber = source;
		}

	}
}
