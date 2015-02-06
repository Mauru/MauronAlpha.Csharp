using MauronAlpha.Events;
using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Collections;

using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.ConsoleApp {
	
	//Class that controls Output to the system
	public class ConsoleApp_output:SystemInterface, 
	I_layoutRenderer {

		//constructor
		public ConsoleApp_output (MauronConsole console) {
			SetTarget ( console );
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


		public I_layoutRenderer Draw( I_layoutUnit source, I_layoutModel layout ) {
			Clear();

			Vector2d CaretPosition = new Vector2d();
			Vector2d CaretEnd = source.Context.Size.AsVector;

			string output = "";
			ConsoleLayout_header header = (ConsoleLayout_header) layout.Member("header");			

			return this;
		}
	}
}
