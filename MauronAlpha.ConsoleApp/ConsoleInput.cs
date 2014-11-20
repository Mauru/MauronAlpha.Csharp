using System;

using MauronAlpha.Input;
using MauronAlpha.Input.Keyboard;
using MauronAlpha.Input.Keyboard.Events;

using MauronAlpha.Events.Units;

namespace MauronAlpha.ConsoleApp {
	
	//A class that reads userInput
	public class ConsoleInput:SystemInterface {

		//constructor
		public ConsoleInput(MauronConsole console):base(){}

		private EventUnit_clock CLOCK_events = new EventUnit_clock();
		public EventUnit_clock Events;

		public ConsoleInput Listen() {
			ConsoleKeyInfo key=System.Console.ReadKey(false);
			KeyPress input = new KeyPress ();
			//was the ctrl key pressed
			if( (key.Modifiers&ConsoleModifiers.Shift)!=0 ) {
				input.SetIsShiftKeyDown(true);
			}
			//was the alt key pressed
			if( (key.Modifiers&ConsoleModifiers.Control)!=0 ) {
				input.SetIsCtrlKeyDown(true);
			}
			//was the ctrl 
			if( (key.Modifiers&ConsoleModifiers.Alt)!=0 ) {
				input.SetIsAltKeyDown(true);
			}

			//Set the character 
			input.SetKey(key.KeyChar);

			//throw a new Keyboardevent
			SendEvent(CLOCK_events, new Event_keyUp(this, input));
			return this;
		}
	
		public ConsoleInput SendEvent(EventUnit_clock clock, MauronCode_event e){
			clock.SubmitEvent (e);
			return this;
		}

		#region The related Console
		private MauronConsole C_target;
		public MauronConsole Target {
			get {
				if( C_target==null ) {
					NullError("Console can not be null!,(Target)", this, typeof(MauronConsole));
				}
				return C_target;
			}
		}
		public ConsoleInput SetTarget (MauronConsole target) {
			C_target=target;
			return this;
		}
		#endregion

		#region I_eventsender
		I_eventSender I_eventSender.SendEvent(EventUnit_clock clock, MauronCode_event e){
			return SendEvent (clock, e);
		}
		#endregion
	}

	//The precision handler for the console input
	public class PrecisionHandler_consoleInput : EventUtility_precision {
		
		//constructor
		public PrecisionHandler_consoleInput():base(EventPrecisionRuleSet.Counter) {}

		public static PrecisionHandler_consoleInput Instance {
			get {
				return new PrecisionHandler_consoleInput();
			}
		}

	}

}
