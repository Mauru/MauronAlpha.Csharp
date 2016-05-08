
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Collections;

namespace MauronAlpha.Events.Units
{
    public class EventUnit_counter:EventComponent_unit, I_sender<Event_counterStep>, I_sender<Event_counterReached> {
		
		long INT_steps = 0;
		long INT_max = -1;

		private Subscriptions<Event_counterStep> SubscriptionsStep;
		private Subscriptions<Event_counterReached> SubscriptionsLimit;

		public long Steps { get { return INT_steps; } }


		public void Subscribe(I_subscriber<Event_counterStep> s) {
			if (SubscriptionsStep == null)
				SubscriptionsStep = new Subscriptions<Event_counterStep>();
			SubscriptionsStep.Add(s);
		}
		public void UnSubscribe(I_subscriber<Event_counterStep> s) {
			if (SubscriptionsStep == null)
				SubscriptionsStep = new Subscriptions<Event_counterStep>();
			SubscriptionsStep.Remove(s);
		}
		public void Subscribe(I_subscriber<Event_counterReached> s) {
			if (SubscriptionsLimit == null)
				SubscriptionsLimit = new Subscriptions<Event_counterReached>();
			SubscriptionsLimit.Add(s);
		}
		public void UnSubscribe(I_subscriber<Event_counterReached> s) {
			if (SubscriptionsLimit == null)
				SubscriptionsLimit = new Subscriptions<Event_counterReached>();
			SubscriptionsLimit.Remove(s);
		}
	}

	public class Event_counterStep : EventUnit_event {
		public Event_counterStep() : base("CounterStep") { }
	}
	public class Event_counterReached : EventUnit_event {

		public Event_counterReached() : base("CounterReached") { }
	}


}
