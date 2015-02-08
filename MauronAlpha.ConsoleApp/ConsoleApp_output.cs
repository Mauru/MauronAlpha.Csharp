using MauronAlpha.Events;

using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.ConsoleApp.Interfaces;


namespace MauronAlpha.ConsoleApp {
	
	//Class that controls Output to the system
	public class ConsoleApp_output:SystemInterface, 
	I_layoutRenderer {

		//constructor
		public ConsoleApp_output (MauronConsole console) {
			SetConsole ( console );
		}

		//The console
		private MauronConsole UNIT_console;
		public MauronConsole Console {
			get {
				if( UNIT_console==null )
					NullError("Console can not be null!,(Target)", this, typeof(MauronConsole));
				return UNIT_console;
			}
		}

		//Methods
		public ConsoleApp_output SetConsole (MauronConsole console) {
			UNIT_console = console;
			return this;
		}
		public ConsoleApp_output Clear() {
			System.Console.Clear();
			return this;
		}
		public ConsoleApp_output WriteLine(string txt) {
			System.Console.WriteLine(txt);
			return this;
		}

		public I_layoutRenderer Draw (I_consoleUnit source) {

			Vector2d CaretPosition=new Vector2d();
			Vector2d CaretEnd=source.Context.Size.AsVector;

			return this;
		}
	}
}
