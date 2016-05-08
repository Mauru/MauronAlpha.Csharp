using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.Interfaces {
	
	//Interface for classes that can send events
	public interface I_eventSender {

		I_eventSender SendEvent(EventUnit_event e);
	}

	public interface I_sender<T> where T:EventUnit_event {

		void Subscribe(I_subscriber<T> s);
		void UnSubscribe(I_subscriber<T> s);

		string Id { get; }

	}
}
