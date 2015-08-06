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
	I_layoutController {
		
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

			//4: Define the Command-Model for the Console
			ConsoleApp_commandModel model = new ConsoleApp_commandModel(this);

			//5: Define the console Output
			OUTPUT_console = new ConsoleApp_output( this );

			//6: Define the "looks" of the console
			LAYOUT_console = new ConsoleApp_layout( this );
			LAYOUT_console.ApplyTo( UNIT_window );
			OUTPUT_console.SetScreenBufferSize(UNIT_window.Size.AsVector);

			LAYOUT_console.Member("header").SetContent(name);
			
			LAYOUT_console.Draw();

			//7: Set initial caret position
			SetFocus("header");
			SetFocus("content");

			//8: Activate Input
			CommandModel.Initiate();
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
					HANDLER_commands = new ConsoleApp_commandModel(this);
				return HANDLER_commands;
			}
		}

		//Booleans
		public bool AllowsInput {
			get {
				return CommandModel.AllowsInput;
			}
		}
		private bool B_canExit = false;
		public bool CanExit {
			get { 
				return B_canExit;
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

		public Layout2d_context Context {
			get {
				if( UNIT_window == null )
					return new Layout2d_context();
				return UNIT_window.Context;
			}
		}
		//Inputs
		private ConsoleApp_input INPUT_console;
		public ConsoleApp_input Input {
			get {
				if( INPUT_console == null ){
					INPUT_console = new ConsoleApp_input( CommandModel );
					//CommandModel.ActivateInput( INPUT_console, this );
				}
				return INPUT_console;
			}
		}

		private I_consoleOutput OUTPUT_console;
		//Outputs
		public I_consoleOutput Output {
			get {
				if( OUTPUT_console==null )
					OUTPUT_console=new ConsoleApp_output(this);
				return OUTPUT_console;
			}
		}
		I_layoutRenderer I_layoutController.Output {
			get {
				if( OUTPUT_console==null )
					OUTPUT_console=new ConsoleApp_output(this);
				return (I_layoutRenderer) OUTPUT_console;
			}
		}

		public I_layoutUnit MainScreen { get { return UNIT_window; } }

		//Methods
		public MauronConsole SetFocus(string member) {
			I_consoleUnit unit = LayoutModel.Member(member);
			Output.SetCaretPosition(unit,unit.CaretPosition);
			return this;
		}
		
		//This is basically a process state trigger
		public ProjectComponent_statusCode Idle() {
            Input.Listen();
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