using MauronAlpha.Events.Units;

namespace MauronAlpha.Events {
	
	public interface I_eventHandler {

		EventUnit_clock MasterClock { get; }
		EventUnit_time Time { get; }
		EventUnit_timeStamp TimeStamp { get; }

	}

}
