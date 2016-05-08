using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Input.Keyboard.Units;

namespace MauronAlpha.Input.Keyboard.Events {

	public class Event_keyUp:EventUnit_event {

		//The Event code
		public static string EventCode="keyUp";
		public I_sender<Event_keyUp> Sender;

		//constructor
		public Event_keyUp(I_sender<Event_keyUp> sender,KeyPress key):base(Event_keyUp.EventCode){
			SetKeyPress (key);
			Sender = sender;
		}

		//The keypress
		private KeyPress K_key;
		public KeyPress KeyPress {
			get { 
				if (K_key == null)
					NullError ("KeyPress can not be null!", this,typeof(KeyPress));
				return K_key;
			}
		}
		public Event_keyUp SetKeyPress(KeyPress key){
			K_key = key;
			return this;
		}

	}
}
