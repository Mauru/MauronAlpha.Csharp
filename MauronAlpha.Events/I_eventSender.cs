using MauronAlpha.HandlingData;

namespace MauronAlpha.Events {

	//A class that can trigger events
	public interface I_eventSender {

		I_eventSender SendEvent(EventUnit_clock clock, MauronCode_event e);
	
	}

}
