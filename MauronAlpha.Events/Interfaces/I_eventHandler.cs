using System;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.Interfaces {
    
	//A element that uses the eventHandler
	public interface I_eventHandler {
		
		bool Equals(I_eventHandler other);

		EventUnit_timeStamp TimeStamp { get; }

    }

}
