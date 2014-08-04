namespace MauronAlpha.Events {

	//A class that receives events
	public interface I_eventReceiver {

		I_eventReceiver ReceiveEvent(MauronCode_event e);

		//Check for an event condition
		bool IsEventCondition (MauronCode_event e);
	}

}
