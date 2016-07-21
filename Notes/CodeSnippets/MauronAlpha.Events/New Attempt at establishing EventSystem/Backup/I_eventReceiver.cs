namespace MauronAlpha.Events {

	//A class that receives events
	public interface I_eventReceiver {

		//Subscribe to Events
		I_eventReceiver SubscribeToEvents();

		//Subscribe to a single Event
		I_eventReceiver SubscribeToEvent(EventUnit_clock clock, string message);

		//Receive an event
		I_eventReceiver ReceiveEvent(EventUnit_clock clock, MauronCode_event e);
	}

}
