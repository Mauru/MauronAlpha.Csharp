using System;
namespace MauronAlpha {

	//ProjectTypes 
    public abstract class ProjectType:MauronCode_subtype {
		public ProjectType(){}
		public abstract string Name { get; }
		
		//Presets
		public static ProjectType Generic {
			get {
				return ProjectType_generic.Instance;
			}
		}
	}

	//A generic Project
	public sealed class ProjectType_generic : ProjectType {
		#region Singleton
		private static volatile ProjectType_generic instance=new ProjectType_generic();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static ProjectType_generic ( ) { }
		public static ProjectType_generic Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new ProjectType_generic();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "generic"; }
		}
	}
}
