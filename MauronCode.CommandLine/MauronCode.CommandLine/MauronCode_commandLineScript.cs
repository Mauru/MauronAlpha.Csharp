using MauronAlpha;
using MauronAlpha.Projects;

using System;

namespace MauronCode.CommandLine {

	//A script that is run either via command line or the console
	public class MauronCode_commandLineScript:MauronCode_project {
		public MauronCode_commandLineScript(string name):base(ProjectType_commandLineScript.Instance, name) {}
	}

	//Project Description
	public sealed class ProjectType_commandLineScript:ProjectType {
		#region singleton
		private static volatile ProjectType_commandLineScript instance=new ProjectType_commandLineScript();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ProjectType_commandLineScript ( ) { }
		public static ProjectType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ProjectType_commandLineScript();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "commandLineScript"; } }
	}

}
