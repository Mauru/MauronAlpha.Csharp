using MauronAlpha;
using MauronAlpha.ExplainingCode;

using System;

namespace MauronAlpha.Projects {

	//A Project coded in MauronCode
	public class MauronCode_project:MauronCode,
		I_project {

		//The constructor
		public MauronCode_project(ProjectType projecttype, string name):base(CodeType_project.Instance) {
			SetProjectType(projecttype);
		}

		#region I_Project
		//The name
		private string STR_name = "";
		public string Name { 
			get { 
				return STR_name; 
			} 
		}	
		public I_project SetName(string name) {
			STR_name = name;
			return this;
		}

		//Describes the project
		private ProjectType PT_projecttype;
		public ProjectType ProjectType { 
			get { 
				if(PT_projecttype==null){ 
					throw NullError("ProjectType can not be null!,(ProjectType)",this,typeof(ProjectType)); 
				}
				return PT_projecttype;
			}
		}
		public MauronCode_project SetProjectType(ProjectType projectType) {
			PT_projecttype=projectType;
			return this;
		}
		#endregion

	}

	//Code Description
	public sealed class CodeType_project : CodeType {
		#region singleton
		private static volatile CodeType_project instance=new CodeType_project();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_project ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_project();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "project"; } }
	}

}
