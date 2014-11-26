using MauronAlpha.Projects;

using MauronAlpha.HandlingData;

using MauronAlpha.Events;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events.Collections;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.ConsoleApp {

	//A Console Application
	public class MauronConsole : MauronCode_project, I_layoutController, I_eventSubscriber {
		
		//constructor
		public MauronConsole(string name, Layout2d_context context):base(ProjectType_mauronConsole.Instance, name){
			EventUnit_clock clock = new EventUnit_clock();

			HANDLER_events = new MauronAlpha.Events.EventHandler(clock);


			//Define Window
			WindowController = new Layout2d_window(name, this, context);
		}

		private EventHandler HANDLER_events;
		public EventHandler EventHandler {
			get {
				return HANDLER_events;
			}
		}

		private Layout2d_window WindowController;

		private ConsoleApp_input INPUT_console;
		public ConsoleApp_input Input {
			get {
				if(INPUT_console==null){
					INPUT_console = new ConsoleApp_input(this);
                    HANDLER_events.SubscribeToCode("keyUp", this, EventModels.Continous);
				}
				return INPUT_console;
			}
		}

		private ConsoleApp_output OUTPUT_console;
		public ConsoleApp_output Output {
			get {
				if( OUTPUT_console==null ) {
					OUTPUT_console=new ConsoleApp_output(this);
				}
				return OUTPUT_console;
			}
		}

		MauronAlpha.Events.EventHandler I_layoutController.EventHandler {
			get { return HANDLER_events; }
		}

		public static MauronCode_dataList<string> StateModes = new MauronCode_dataList<string>(){"idle","busy","done"};
		private MauronCode_dataMap<EventUnit_subscriptionModel> TREE_states = new MauronCode_dataMap<EventUnit_subscriptionModel>();

		//States
		public ProjectComponent_statusCode Idle() {
            Input.Listen();
			return new ProjectComponent_statusCode(this);
		}

		//I_eventSubscriber
		bool I_eventSubscriber.Equals (I_eventSubscriber other) {
			return Equals(other);
		}

		bool I_eventSubscriber.ReceiveEvent (EventUnit_event e) {
			throw new System.NotImplementedException();
		}

		private DataMap_EventTriggers DATA_eventTriggers;
		private DataMap_EventTriggers EventTriggers {
			get {
				if(DATA_eventTriggers == null) {
					DATA_eventTriggers=new ConsoleApp_eventTriggers(this);
				}
				return DATA_eventTriggers;
			}
		}
		EventHandler.DELEGATE_trigger I_eventSubscriber.TriggerOfCode (string Code) {
			System.Console.WriteLine("Sending Delegate Trigger for "+Code);
			if( !EventTriggers.ContainsKey(Code) )
				return EventTriggers.DoNothing;
			return EventTriggers.Value(Code);
		}

        public bool EVENT_keyUp(EventUnit_event unit) {
			System.Console.WriteLine("Received Event!");
            return true;        
        }


	}

	//Event Trigger Registry for ConsoleApp
	public class ConsoleApp_eventTriggers:DataMap_EventTriggers {

        private MauronConsole Console;

        //constructor
		public ConsoleApp_eventTriggers(MauronConsole instance):base() {
            Console = instance;
            base.SetValue("keyUp", Console.EVENT_keyUp);
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