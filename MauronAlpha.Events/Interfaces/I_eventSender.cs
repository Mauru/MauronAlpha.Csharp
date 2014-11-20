using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.Interfaces {
	
	//Interface for classes that can send events
	public interface I_eventSender {

		I_eventSender SendEvent(EventUnit_event e);
	}

}
