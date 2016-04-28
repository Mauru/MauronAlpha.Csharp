using System;
using MauronAlpha.Events.Interfaces;

namespace MauronAlpha.Events.Units
{
    public class EventUnit_counter:EventComponent_unit,I_eventSender {

		long INT_steps = 0;

		public long Steps { get { return INT_steps; } }

		EventHandler Events = new EventHandler();

		public EventUnit_counter Step() {
			INT_steps++;
			SendEvent(new EventUnit_event(this, "step"));
			return this;
		}

		public I_eventSender SendEvent(EventUnit_event e) {
			Events.SubmitEvent(e, this);
			return this;
		}
	}
}
