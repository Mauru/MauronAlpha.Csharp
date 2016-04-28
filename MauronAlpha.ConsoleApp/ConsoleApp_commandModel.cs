using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha.Input.Keyboard.Events;
using MauronAlpha.Input.Keyboard.Units;

using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Collections;

using MauronAlpha.ConsoleApp.Interfaces;
using MauronAlpha.ConsoleApp.Collections;
using MauronAlpha.HandlingData;
using MauronAlpha.Forms.Units;

using System.Collections.Generic;

namespace MauronAlpha.ConsoleApp {

	//The shared code-base for the console window
	public class ConsoleApp_commandModel : ConsoleApp_component,
	I_consoleController, 
	I_eventController,
	I_eventSubscriber {

		//Constructor
		private ConsoleApp_commandModel():base() {}
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
			EventHandler.DELEGATE_trigger trigger = TriggerOfCode(e.Code);
			bool result = trigger(e);
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
				if( DATA_eventTriggers==null )
					DATA_eventTriggers = new ConsoleApp_eventTriggers( this );
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

		//Special Key Map
		private ConsoleApp_keyCommands DATA_commands;
		public ConsoleApp_keyCommands KeyboardCommands {
			get {
				if (DATA_commands == null)
					DATA_commands = new ConsoleApp_keyCommands();
				return DATA_commands;
			}
		}
		public ConsoleApp_commandModel SetKeyboardCommands(ConsoleApp_keyCommands map) {
			DATA_commands = map;
			return this;
		}

		//Methods
		public virtual ConsoleApp_commandModel Initiate() {
			ActivateInput(MAU_console.Input,MAU_console);
			return this;
		}
		public virtual ConsoleApp_commandModel EvaluateInput() {
			KeyPress key = INPUT_keyBoard.ActiveSequence.LastElement;

			MauronCode_dataTree<KeyPressSequence, string> commands = KeyboardCommands.Commands;
			foreach (KeyValuePair<KeyPressSequence, string> command in commands.AsKeyValuePairs) {
				if (command.Key.Equals(key))
					LayoutModel.Member("debug").SetContent(command.Value);
			}
			
			if (KeyboardCommands.IsCommand(key))
				LayoutModel.Member("debug").SetContent("Found command for ("+key.KeyName+")");
			else
				LayoutModel.Member("debug").SetContent("Not a command (" + key.KeyName + ")");

			if (!KeyboardCommands.IsCommand(key) || !key.IsFunction) {
				ActiveInput.Insert(key);
				ActiveInput.CaretPosition.Add(0, 0, 0, 1);
				ActiveInput.RequestRender();
				LayoutModel.Member("footer").PrependContent("Last key pressed: " + key.KeyName,true);
			}
			else {
				LayoutModel.Member("footer").PrependContent("Command key pressed: " + key.KeyName,true);
			}
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

		public virtual I_consoleUnit ActiveInput {
			get {
				return LayoutModel.Member("content");
			}
		}

		public I_consoleController Debug(string message) {
			if (LAYOUT_model.HasMember("Debug"))
				LAYOUT_model.Member("Debug").Content.AsNewLine(message);
			return this;
		}
	}

}