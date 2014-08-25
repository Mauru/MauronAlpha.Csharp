using MauronAlpha.Events;
using MauronAlpha.Events.Data;

namespace MauronAlpha.Input.Keyboard.Events {

	public class Event_keyUp:MauronCode_event {

		//constructor
		public Event_keyUp(
			MauronCode_eventClock clock,
			I_eventSender sender,
			KeyPress key
		):base(
			clock,
			sender,
			"keyUp"
		){

		}



	}
}
