using MauronAlpha.Events.Data;

namespace MauronAlpha.Events {

	//A class that can trigger events
	public interface I_eventSender {

		//Get the eventclock
		MauronCode_eventClock EventClock { get; }

		I_eventSender SendEvent(MauronCode_eventClock clock, string code, EventData data);
	
	}

}
