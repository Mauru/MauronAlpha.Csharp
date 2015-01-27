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

using MauronAlpha.ConsoleApp.Interfaces;

namespace MauronAlpha.ConsoleApp {

	//A Console Application Wrapper
	public class MauronConsole : MauronCode_project, 
	I_layoutController,
	I_consoleController {
		
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
			LAYOUT_console = new ConsoleApp_layout();
			LAYOUT_console.ApplyTo( UNIT_window );

			//5: Define the Command-Model for the Console
			ConsoleApp_commandModel model = new ConsoleApp_commandModel();

			LAYOUT_console.RenderWith( OUTPUT_console );
		}

		//State of the Program
		public static MauronCode_dataList<string> StateModes = 
			new MauronCode_dataList<string>(){ "idle", "busy", "done" };
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

		//The CommandModel for the console
		private ConsoleApp_commandModel HANDLER_commands;
		public ConsoleApp_commandModel CommandModel {
			get {
				if(HANDLER_commands == null) 
					HANDLER_commands = new ConsoleApp_commandModel();
				return HANDLER_commands;
			}
		}

		//This is basically the Contained Data
		public I_consoleData ContentModel {
			get {
				return CommandModel.ContentModel;
			}
		}
		public bool AllowsInput {
			get {
				return CommandModel.AllowsInput;
			}
		}

		//The Layout of the console
		private ConsoleApp_layout LAYOUT_console;
		public I_consoleLayout LayoutModel {
			get {
				if( LAYOUT_console == null )
					LAYOUT_console = new ConsoleApp_layout();
				return LAYOUT_console;
			}
		}

		//The Output window
		private Layout2d_window UNIT_window;

		//Inputs
		private ConsoleApp_input INPUT_console;
		public ConsoleApp_input Input {
			get {

				if( INPUT_console == null ){
					INPUT_console = new ConsoleApp_input( CommandModel );
					CommandModel.ActivateInput( INPUT_console, this );
				}
				return INPUT_console;
			}
		}

		//Outputs
		private I_layoutRenderer OUTPUT_console;
		public I_layoutRenderer Output {
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

		//

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