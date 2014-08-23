using System;
using MauronAlpha.HandlingData;

namespace MauronAlpha.ConsoleApp {
	
	public class ConsoleSettings:ProjectSettings {

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

		#region Window title

		private static string STR_title="MauronConsole Application";
		public string Title { get { return STR_title; } }
		public ConsoleSettings SetTitle (string title) {
			STR_title=title;
			return this;
		}

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
