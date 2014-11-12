using System;

using MauronAlpha.Projects;

using MauronAlpha.Events;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Singletons;

using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.ConsoleApp {

	//A Console Application
	public class MauronConsole : MauronCode_project {
		
		//constructor
		public MauronConsole(string name):base(ProjectType_mauronConsole.Instance, name){

			//Define Event System
			EventUnit_clock clock = new EventUnit_clock();
			EventUnit_time time = new EventUnit_time(SharedEventSystem.SystemTicks,SharedEventSystem.Instance.SystemClock);

			//Define Window
			//WindowController = new Layout2d_window(name,CLOCK_events);

		}

		private EventUnit_clock CLOCK_events;

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

	}

	//Project Description
	public sealed class ProjectType_mauronConsole : ProjectType {
		#region singleton
		private static volatile ProjectType_mauronConsole instance=new ProjectType_mauronConsole();
		private static object syncRoot=new Object();
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