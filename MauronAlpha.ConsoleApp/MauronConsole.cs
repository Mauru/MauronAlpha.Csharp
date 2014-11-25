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

		Events.EventHandler I_layoutController.EventHandler {
			get { return HANDLER_events; }
		}

		public static MauronCode_dataList<string> StateModes = new MauronCode_dataList<string>(){"idle","busy","done"};
		private MauronCode_dataMap<EventUnit_subscriptionModel> TREE_states = new MauronCode_dataMap<EventUnit_subscriptionModel>();

		//States
		public ProjectComponent_statusCode Idle() {
			HANDLER_events.SubscribeToCode("keyUp",this,TREE_states.Value("idle"));
			return new ProjectComponent_statusCode(this);
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