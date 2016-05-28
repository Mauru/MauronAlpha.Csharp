
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Collections;

namespace MauronAlpha.Events.Units
{
    public class EventUnit_counter:EventComponent_unit, I_sender<Event_counterStep>, I_sender<Event_counterReached> {
		
		long INT_steps = 0;
		long INT_max = 0;

		private Subscriptions<Event_counterStep> SubscriptionsStep;
		private Subscriptions<Event_counterReached> SubscriptionsLimit;

		public bool LimitReached {
			get {
				if (INT_steps < 1)
					return false;
				if (INT_max < 1)
					return false;
				return (INT_max <= INT_steps);
			}
		}
		private bool B_disabled = false;
		public bool Disabled { get { return B_disabled; } }

		private bool B_disableOnLimit = false;
		public bool DisableOnLimit { get { return B_disableOnLimit; } }

		public void Disable(bool status) {
			B_disabled = status;
			if (status) {

			}
			

		}

		public long Steps { get { return INT_steps; } }

		public long Step() {
			if (Disabled)
				return INT_steps;
			INT_steps++;
			if (LimitReached) {
				if (SubscriptionsLimit != null)
					SubscriptionsLimit.ReceiveEvent(new Event_counterReached(this, INT_steps, INT_max));
				if (DisableOnLimit)
					Disable(true);
				return INT_steps;
			}
			else {
				if (SubscriptionsStep != null)
					SubscriptionsStep.ReceiveEvent(new Event_counterStep(this, INT_steps));
			}
			return INT_steps;
		}

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
		public long Steps = 0;
		public EventUnit_counter Counter;

		public Event_counterStep(EventUnit_counter counter, long steps) : base("CounterStep") {
			Counter = counter;
			Steps = steps;
		}
	}
	public class Event_counterReached : EventUnit_event {
		public long Steps = 0;
		public EventUnit_counter Counter;
		public long Limit;

		public Event_counterReached(EventUnit_counter counter, long steps, long limit) : base("CounterReached") {
			Counter = counter;
			Steps = steps;
			Limit = limit;
		}
	}
	public class Event_counterDisabledToggle : EventUnit_event {
		public EventUnit_counter Counter;

		public Event_counterDisabledToggle(EventUnit_counter counter)
			: base("CounterDisabledToggle") {
			Counter = counter;
		}

	}

	public class Event_counterReset : EventUnit_event {
		public EventUnit_counter Counter;

		public Event_counterReset(EventUnit_counter counter)
			: base("CounterReset") {
			Counter = counter;
		}

	}

}
