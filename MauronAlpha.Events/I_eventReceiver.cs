namespace MauronAlpha.Events {

	//A class that receives events
	public interface I_eventReceiver {

		//Subscribe to Events
		I_eventReceiver SubscribeToEvents();

		//Subscribe to a single Event
		I_eventReceiver SubscribeToEvent(MauronCode_event e);

		//Receive an event
		I_eventReceiver ReceiveEvent(MauronCode_event e);

		//Check for an event condition
		bool IsEventCondition (MauronCode_event e);
	}

}
