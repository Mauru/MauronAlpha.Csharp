using MauronAlpha.Events;

namespace MauronAlpha.Events.Interfaces {

	//Interface for a class that implements EventHandler
	public interface I_eventController {
		
		EventHandler EventHandler { get; }
		bool Equals(I_eventController other);

		string Id { get; }


	}

}
