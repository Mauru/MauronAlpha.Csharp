using System;

namespace MauronAlpha.Events.Interfaces {

	//A subscriber to an event
    public interface I_eventSubscriber {

		bool Equals(I_eventSubscriber other);
		
    }

}
