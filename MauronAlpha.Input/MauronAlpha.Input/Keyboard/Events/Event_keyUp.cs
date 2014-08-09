using MauronAlpha.Events;
using MauronAlpha.Events.Defaults;

namespace MauronAlpha.Input.Keyboard.Events {

	public class Event_keyUp:MauronCode_event {
		
		public Event_keyUp(MauronCode_eventClock clock, I_eventSender sender):base(clock,sender,EventCondition_never.Delegate,EventTrigger_nothing.Delegate){
			
		}



	}
}
