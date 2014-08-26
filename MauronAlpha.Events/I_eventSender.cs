using MauronAlpha.HandlingData;

namespace MauronAlpha.Events {

	//A class that can trigger events
	public interface I_eventSender {

		I_eventSender SendEvent(MauronCode_eventClock clock, MauronCode_event e);
	
	}

}
