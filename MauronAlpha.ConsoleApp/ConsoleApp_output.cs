using MauronAlpha.Events;

using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.ConsoleApp.Interfaces;

using MauronAlpha.Text.Units;


namespace MauronAlpha.ConsoleApp {
	
	//Class that controls Output to the system
	public class ConsoleApp_output:SystemInterface, 
	I_consoleOutput {

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
		public ConsoleApp_output WriteLine(string txt) {
			System.Console.WriteLine(txt);
			return this;
		}

		public I_consoleOutput WriteLine(TextUnit_line unit) {
			WriteLine(	unit.AsString	);
			return this;
		}
		public I_consoleOutput WriteLine( TextUnit_line unit , int maxWidth) {
			WriteLine( unit.AsString );
			return this;
		}

		public I_layoutRenderer Clear ( ) {
			System.Console.Clear();
			return this;
		}
		public I_layoutRenderer Draw (I_consoleUnit source) {

			Vector2d CaretPosition=new Vector2d();
			Vector2d CaretEnd=source.Context.Size.AsVector;

			return this;
		}

		public I_consoleOutput SetCaretPosition (I_consoleUnit focus, CaretPosition position) {
			Vector2d pos=position.AsVector;
			pos.Add(focus.Context.Position.AsVector);

			System.Console.SetCursorPosition(position.X, position.Y);
			return this;
		}
	}
}
