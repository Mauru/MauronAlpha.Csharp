using System;

using MauronAlpha.ExplainingCode;
using MauronAlpha.Interfaces;
using MauronAlpha.Projects;

namespace MauronAlpha.Projects {

	public class ProjectComponent_statusCode:MauronCode_component,
	IEquatable<ProjectComponent_statusCode>,
	I_protectable<ProjectComponent_statusCode> {
		
		//Constructor
		public ProjectComponent_statusCode( I_project project ) {
			OBJ_project = project;
		}
		
		//Project
		I_project OBJ_project;
		public I_project Project {
			get {
				return OBJ_project;
			}
		}

		//Booleans
		public bool CanExit {
			get { return false; }
		}
		public bool Equals( ProjectComponent_statusCode other ) {
			return Id.Equals( other.Id );
		}
		public bool Equals( I_project other ) {
			return Id.Equals( other.Id );
		}
		public bool IsReadOnly {
			get {
				return true;
			}
		}
	
		//Methods
		public ProjectComponent_statusCode SetIsReadOnly(bool status) {
			return this;
		}
	}

}
