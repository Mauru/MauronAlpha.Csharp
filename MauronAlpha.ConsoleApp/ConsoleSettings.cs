using System;
using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;
using MauronAlpha.Projects;

namespace MauronAlpha.ConsoleApp {
	
	public class ConsoleSettings:ProjectSettings {

		private MauronConsole MC_parent;
		private MauronConsole Parent {
			get { 
				if (MC_parent == null) {
					NullError ("Parent can not be null", this, typeof(MauronConsole));
				}
				return MC_parent;
			}
		}
		private ConsoleSettings SetParent(MauronConsole parent){
			MC_parent = parent;
			return this;
		}

		//constructor
		public ConsoleSettings(MauronConsole console){
			SetParent (console);
		}

		#region How to treat whitespaces and other special characters

		private bool B_visualizeStrings=false;
		public bool VisualizeStrings {
			get {
				return B_visualizeStrings;
			}
		}
		public ConsoleSettings SetVisualizeStrings (bool status) {
			B_visualizeStrings=status;
			return this;
		}

		#endregion

		#region The Environment (i.e. Console stays open)

		//Is the applicaton a one time only process?
		private bool B_isEnvironment=false;
		public bool IsEnvironment { get { return B_isEnvironment; } }
		public ConsoleSettings SetIsEnvironment (bool status) {
			B_isEnvironment=status;
			return this;
		}

		#endregion

		#region Show Window title

		//show the title in window at all times
		private bool B_titleVisible=false;
		public bool TitleVisible {
			get {
				return B_titleVisible;
			}
		}
		public ConsoleSettings SetTitleVisible (bool visible) {
			B_titleVisible=visible;
			return this;
		}

		#endregion
	
	
	}

}
