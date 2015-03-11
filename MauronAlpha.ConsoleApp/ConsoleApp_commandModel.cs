using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha.Input.Keyboard.Events;
using MauronAlpha.Input.Keyboard.Units;

using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Collections;

using MauronAlpha.ConsoleApp.Interfaces;
using MauronAlpha.ConsoleApp.Collections;

namespace MauronAlpha.ConsoleApp {

	//The shared code-base for the console window
	public class ConsoleApp_commandModel : ConsoleApp_component,
	I_consoleController, 
	I_eventController,
	I_eventSubscriber {

		//Constructor
		public ConsoleApp_commandModel():base() {}
		public ConsoleApp_commandModel(MauronConsole console){
			MAU_console = console;
		}

		//Booleans
		public bool Equals (I_eventController other) {
			return Id==other.Id;
		}
		public bool Equals (I_eventSubscriber other) {
			return Id==other.Id;
		}
		private bool b_allowsInput = true;
		public bool AllowsInput{ 
			get {
				return b_allowsInput;
			}
		}
		
		//Event Model
		private EventHandler EVENT_handler;
		public EventHandler EventHandler {
			get {
				if( EVENT_handler==null )
					EVENT_handler=new EventHandler();
				return EVENT_handler;
			}
		}

		public bool ReceiveEvent (EventUnit_event e) {
			EventHandler.DELEGATE_trigger trigger=TriggerOfCode(e.Code);
			bool result=trigger(e);
			return result;
		}
		bool I_eventSubscriber.ReceiveEvent (EventUnit_event e) {
			EventHandler.DELEGATE_trigger trigger=TriggerOfCode(e.Code);
			bool result=trigger(e);
			return result;
		}

		private DataMap_EventTriggers DATA_eventTriggers;
		private DataMap_EventTriggers EventTriggers {
			get {
				if( DATA_eventTriggers==null ) {
					DATA_eventTriggers = new ConsoleApp_eventTriggers( this );
				}
				return DATA_eventTriggers;
			}
		}

		private EventHandler.DELEGATE_trigger TriggerOfCode (string code) {
			if( !EventTriggers.ContainsKey(code) )
				return EventTriggers.DoNothing;
			return EventTriggers.Value(code);
		}
		EventHandler.DELEGATE_trigger I_eventSubscriber.TriggerOfCode (string code) {
			return TriggerOfCode(code);
		}

		//The Console
		private MauronConsole MAU_console;
		private ConsoleApp_input INPUT_keyBoard;

		//The Layout
		private I_consoleLayout LAYOUT_model;
		public I_consoleLayout LayoutModel {  
			get {
				if(MAU_console == null)
					LAYOUT_model = new ConsoleApp_layout();
				else
					LAYOUT_model = MAU_console.LayoutModel;
				return LAYOUT_model;
			}
		}

		//Methods
		public virtual ConsoleApp_commandModel Initiate() {
			ActivateInput(MAU_console.Input,MAU_console);
			return this;
		}
		public ConsoleApp_commandModel CommandModel {
			get {
				return this;
			}
		}
		public ConsoleApp_commandModel SubscribeToEvent(string eventCode, EventUnit_subscriptionModel model ) {
			EventHandler.SubscribeToCode(eventCode, this, model);
			return this;
		}
		public ConsoleApp_commandModel ActivateInput( ConsoleApp_input inputController, MauronConsole console ) {
			
			if( MAU_console == null )
				MAU_console = console;

			if( INPUT_keyBoard == null )
				INPUT_keyBoard = inputController;

				SubscribeToEvent("keyUp",EventModels.Continous);

			return this;

		}
		public ConsoleApp_commandModel EvaluateInput() {
			if(INPUT_keyBoard.ActiveSequence.FirstElement)
			return this;	
		}

	}

}