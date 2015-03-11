using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events.Collections;

using MauronAlpha.Input.Keyboard.Events;
using MauronAlpha.Input.Keyboard.Units;

using MauronAlpha.ConsoleApp.Interfaces;

namespace MauronAlpha.ConsoleApp.Collections {

	//Event Trigger Registry for ConsoleApp
	public class ConsoleApp_eventTriggers : DataMap_EventTriggers {

		private I_consoleController Controller;

		//Constructor
		public ConsoleApp_eventTriggers (I_consoleController controller)
			: base() {
			Controller=controller;
			base.SetValue( "keyUp", EVENT_keyUp);
		}

		//Events
		public bool EVENT_keyUp (EventUnit_event unit) {
			Event_keyUp e = (Event_keyUp) unit;

			if( Controller == null )
				return false;

			ConsoleApp_commandModel commandModel = Controller.CommandModel;
			
			KeyPress  key = e.KeyPress;
			if( !commandModel.AllowsInput )
				return false;

			commandModel.EvaluateInput();

			return true;
		}
	}



}
