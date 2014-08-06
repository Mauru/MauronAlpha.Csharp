using MauronAlpha.HandlingData;

namespace MauronAlpha.Events {

	//A class that can trigger events
	public interface I_eventSender {

		//Get the eventclock
		MauronCode_eventClock EventClock { get; }

		I_eventSender InitializeEventHandling();

		I_eventSender SendEvent(MauronCode_eventClock clock, string code, MauronCode_dataSet data);
	
	}

}
