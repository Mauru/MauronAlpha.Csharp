using MauronAlpha.Events.Units;

namespace MauronAlpha.Events {
	
	public interface I_eventHandler {

		MauronCode_eventClock MasterClock { get; }
		MauronCode_timeUnit Time { get; }
		MauronCode_timeStamp TimeStamp { get; }

	}

}
