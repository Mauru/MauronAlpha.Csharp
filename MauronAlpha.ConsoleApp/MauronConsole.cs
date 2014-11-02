using System;

using MauronAlpha.Projects;
using MauronAlpha.Events;

namespace MauronAlpha.ConsoleApp {

	//A Console Application
	public class MauronConsole : MauronCode_project {
		
		//constructor
		public MauronConsole(string name):base(ProjectType_mauronConsole.Instance, name){
			
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