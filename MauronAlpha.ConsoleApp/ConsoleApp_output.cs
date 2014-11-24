using MauronAlpha.Events;

namespace MauronAlpha.ConsoleApp {
	
	//Class that controls Output to the system
	public class ConsoleApp_output:SystemInterface {

		//constructor
		public ConsoleApp_output (MauronConsole console) {
			SetTarget (console);
		}

		#region The related MauronConsole
		private MauronConsole C_target;
		public MauronConsole Target {
			get {
				if( C_target==null ) {
					NullError("Console can not be null!,(Target)", this, typeof(MauronConsole));
				}
				return C_target;
			}
		}
		public ConsoleApp_output SetTarget (MauronConsole target) {
			C_target=target;
			return this;
		}
		#endregion

		#region Clear the window
		public ConsoleApp_output Clear() {
			System.Console.Clear();
			return this;
		}
		#endregion
	
		#region Write a line to the output
		public ConsoleApp_output WriteLine(string txt) {
			System.Console.WriteLine(txt);
			return this;
		}
		#endregion
	}
}
