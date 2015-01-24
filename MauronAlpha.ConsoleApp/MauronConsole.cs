using MauronAlpha.Projects;

using MauronAlpha.HandlingData;

using MauronAlpha.Events;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events.Collections;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Input.Keyboard.Events;

namespace MauronAlpha.ConsoleApp {

	//A Console Application Wrapper
	public class MauronConsole : MauronCode_project, 
	I_layoutController, 
	I_eventSubscriber {
		
		//Constructors
		public MauronConsole(string name, Layout2d_context context):base(
			ProjectType_mauronConsole.Instance,
			name
		){
			//1: Establish an event clock
			EventUnit_clock clock = new EventUnit_clock();
			HANDLER_events = new MauronAlpha.Events.EventHandler( clock );

			//2: Define the output-environment ( Assume a Window )
			UNIT_window = new Layout2d_window( name, this );

			//3: Set Window Event handler and context
			UNIT_window.SetEventHandler(HANDLER_events);
			UNIT_window.SetContext(context);

			//4: Define the "looks" of the console
			ConsoleApp_layout theme = new ConsoleApp_layout();
			theme.ApplyTo( UNIT_window );
		}

		//State of the Program
		public static MauronCode_dataList<string> StateModes = 
			new MauronCode_dataList<string>(){ "idle","busy", "done" };
		private MauronCode_dataMap<EventUnit_subscriptionModel> TREE_states = 
			new MauronCode_dataMap<EventUnit_subscriptionModel>();

		//Event Handler
		private EventHandler HANDLER_events;
		public EventHandler EventHandler {
			get {
				if( HANDLER_events == null )
				HANDLER_events = new EventHandler();
				return HANDLER_events;
			}
		}

		//The Output window
		private Layout2d_window UNIT_window;

		//Inputs
		private ConsoleApp_input INPUT_console;
		public ConsoleApp_input Input {
			get {
				if( INPUT_console == null ){
					INPUT_console = new ConsoleApp_input( this );
                    HANDLER_events.SubscribeToCode( "keyUp", this, EventModels.Continous );
				}
				return INPUT_console;
			}
		}

		//Outputs
		private ConsoleApp_output OUTPUT_console;
		public ConsoleApp_output Output {
			get {
				if( OUTPUT_console==null ) {
					OUTPUT_console=new ConsoleApp_output(this);
				}
				return OUTPUT_console;
			}
		}
		
		//This is basically a process state trigger
		public ProjectComponent_statusCode Idle() {
            Input.Listen();
			return new ProjectComponent_statusCode(this);
		}

		//Booleans
		bool I_eventSubscriber.Equals (I_eventSubscriber other) {
			return Id==other.Id;
		}
		bool I_eventSubscriber.ReceiveEvent (EventUnit_event e) {
			EventHandler.DELEGATE_trigger trigger = TriggerOfCode(e.Code);
			bool result = trigger(e);
			return result;
		}

		//Events
        public bool EVENT_keyUp(EventUnit_event unit) {
			Event_keyUp e = (Event_keyUp) unit;
			System.Console.WriteLine("Key pressed! "+e.KeyPress.Key);
            return true;        
        }

		private DataMap_EventTriggers DATA_eventTriggers;
		private DataMap_EventTriggers EventTriggers {
			get {
				if( DATA_eventTriggers==null ) {
					DATA_eventTriggers=new ConsoleApp_eventTriggers(this);
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

	}

	//Event Trigger Registry for ConsoleApp
	public class ConsoleApp_eventTriggers:DataMap_EventTriggers {

        private MauronConsole Console;

        //Constructor
		public ConsoleApp_eventTriggers( MauronConsole instance ):base() {
            Console = instance;
            base.SetValue( "keyUp", Console.EVENT_keyUp );
        }

	}

	//Project Description
	public sealed class ProjectType_mauronConsole : ProjectType {
		#region singleton
		private static volatile ProjectType_mauronConsole instance=new ProjectType_mauronConsole();
		private static object syncRoot=new System.Object();
		//constructor singleton multithread safe
		static ProjectType_mauronConsole ( ) { }
		public static ProjectType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ProjectType_mauronConsole();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "mauronConsole"; } }

	}

}