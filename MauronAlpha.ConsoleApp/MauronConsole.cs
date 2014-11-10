using System;

using MauronAlpha.Projects;
using MauronAlpha.Events;
using MauronAlpha.Events.Singletons;

namespace MauronAlpha.ConsoleApp {

	//A Console Application
	public class MauronConsole : MauronCode_project, I_eventHandler {
		
		//constructor
		public MauronConsole(string name):base(ProjectType_mauronConsole.Instance, name){
			Output.WriteLine(name);
		}

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

		#region Implement I_eventHandler
		MauronCode_eventClock I_eventHandler.MasterClock {
			get { return SharedEventSystem.Instance.SystemTime; }
		}

		Events.Units.MauronCode_timeUnit I_eventHandler.Time {
			get { return SharedEventSystem.Instance.SystemTime.Time; }
		}

		Events.Units.MauronCode_timeStamp I_eventHandler.TimeStamp {
			get { return SharedEventSystem.Instance.SystemTime.TimeStamp; }
		}
		#endregion

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