using MauronAlpha.Events;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.Layout.Layout2d.Events {
	
	//EventHandler
	public class Layout2d_eventHandler : Layout2d_component, I_eventHandler {

		//Constructor
		public Layout2d_eventHandler (Layout2d_unit unit, EventHandler parentHandler) {
			EVENTS_parent = parentHandler;
		}

		private EventHandler EVENTS_parent;

		public bool Equals(Layout2d_eventHandler other){
			return true;
		}

		bool I_eventHandler.Equals (I_eventHandler other) {
			return Equals(other);
		}

		public EventUnit_timeStamp TimeStamp {
			get {
				return EVENTS_parent.TimeStamp;
			}
		}
	}
}
