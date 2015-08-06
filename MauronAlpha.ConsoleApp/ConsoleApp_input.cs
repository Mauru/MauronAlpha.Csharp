using MauronAlpha.ConsoleApp.Interfaces;

using MauronAlpha.Input;
using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Events;

using MauronAlpha.Events;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;

namespace MauronAlpha.ConsoleApp {
	
	//A class that reads userInput
	public class ConsoleApp_input:SystemInterface, 
	I_eventSender,
	I_consoleInput {

		//constructor
		public ConsoleApp_input( I_eventController source ):base(){
			EventHandler = new EventHandler( source.EventHandler );
		}

		private EventHandler EventHandler;

		public ConsoleApp_input Listen() {

			System.ConsoleKeyInfo key = System.Console.ReadKey( true );

			KeyPress input = new KeyPress ();

			//was the ctrl key pressed
			if( ( key.Modifiers & System.ConsoleModifiers.Shift) !=0 )
				input.SetIsShiftKeyDown(true);

			//was the alt key pressed
			if( ( key.Modifiers & System.ConsoleModifiers.Control ) !=0 )
				input.SetIsCtrlKeyDown(true);

			//was the ctrl 
			if( ( key.Modifiers & System.ConsoleModifiers.Alt ) != 0 )
				input.SetIsAltKeyDown(true);

			//Set the character 
			input.SetChar( key.KeyChar );
			input.SetKeyName(key.Key.ToString());

			AppendToSequence(input);
            
			//throw a new Keyboardevent
			EventHandler.SubmitEvent( new Event_keyUp( this, input), this );
			
			return this;

		}

		I_eventSender I_eventSender.SendEvent (EventUnit_event e) {
			EventHandler.SubmitEvent(e, this);
			return this;
		}

		private KeyPressSequence DATA_activeSequence = new KeyPressSequence();
		public KeyPressSequence ActiveSequence  { 
			get {
				return DATA_activeSequence;	
			}
		}
		public I_consoleInput AppendToSequence( KeyPress key ){
			DATA_activeSequence.Add(key);
			return this;
		}

	}
}