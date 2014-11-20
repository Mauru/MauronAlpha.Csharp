using MauronAlpha.Projects;

using MauronAlpha.Events;
using MauronAlpha.Events.Units;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.ConsoleApp {

	//A Console Application
	public class MauronConsole : MauronCode_project, I_layoutController {
		
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

		private ConsoleInput INPUT_console;
		public ConsoleInput Input {
			get {
				if(INPUT_console==null){
					INPUT_console = new ConsoleInput(this);
				}
				return INPUT_console;
			}
		}

		private ConsoleOutput OUTPUT_console;
		public ConsoleOutput Output {
			get {
				if( OUTPUT_console==null ) {
					OUTPUT_console=new ConsoleOutput(this);
				}
				return OUTPUT_console;
			}
		}


		Events.EventHandler I_layoutController.EventHandler {
			get { return HANDLER_events; }
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