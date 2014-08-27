using System;
using MauronAlpha.Input;
using MauronAlpha.Input.Keyboard;
using MauronAlpha.Input.Keyboard.Events;
using MauronAlpha.Events;

namespace MauronAlpha.ConsoleApp {
	
	//A class that reads userInput
	public class ConsoleInput:SystemInterface, I_eventSender {

		//constructor
		public ConsoleInput(MauronConsole console){
			SetTarget (console);
		}

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
			new Event_keyUp(this,input).Submit(Target.KeyPressCounter);
			return this;
		}
	
		public ConsoleInput SendEvent(MauronCode_eventClock clock, MauronCode_event e){
			clock.SubmitEvent (e);
			return this;
		}


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

		#region I_eventsender
		I_eventSender I_eventSender.SendEvent(MauronCode_eventClock clock, MauronCode_event e){
			return SendEvent (clock, e);
		}
		#endregion
	}

}
