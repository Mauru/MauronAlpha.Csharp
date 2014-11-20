using MauronAlpha.Input;
using MauronAlpha.Input.Keyboard;
using MauronAlpha.Input.Keyboard.Events;

using MauronAlpha.Events;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;

namespace MauronAlpha.ConsoleApp {
	
	//A class that reads userInput
	public class ConsoleInput:SystemInterface, I_eventSender {

		//constructor
		public ConsoleInput(MauronConsole console):base(){
			EventHandler = new EventHandler(console.EventHandler);
		}

		private EventHandler EventHandler;
		public ConsoleInput Listen() {
			System.ConsoleKeyInfo key=System.Console.ReadKey(false);
			KeyPress input = new KeyPress ();
			//was the ctrl key pressed
			if( (key.Modifiers&System.ConsoleModifiers.Shift)!=0 ) {
				input.SetIsShiftKeyDown(true);
			}
			//was the alt key pressed
			if( (key.Modifiers&System.ConsoleModifiers.Control)!=0 ) {
				input.SetIsCtrlKeyDown(true);
			}
			//was the ctrl 
			if( (key.Modifiers&System.ConsoleModifiers.Alt)!=0 ) {
				input.SetIsAltKeyDown(true);
			}

			//Set the character 
			input.SetKey(key.KeyChar);

			//throw a new Keyboardevent
			EventHandler.SubmitEvent(new Event_keyUp(this, input), this);
			return this;
		}

		I_eventSender I_eventSender.SendEvent (EventUnit_event e) {
			EventHandler.SubmitEvent(e,this);
			return this;
		}
	}
}