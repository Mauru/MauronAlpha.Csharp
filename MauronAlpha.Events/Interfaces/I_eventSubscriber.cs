using System;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.Interfaces {

	//A subscriber to an event
    public interface I_eventSubscriber {

		bool Equals(I_eventSubscriber other);

		bool ReceiveEvent(EventUnit_event e);
		
    }

}
