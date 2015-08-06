using MauronAlpha.Events;

using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Collections;

using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.ConsoleApp.Interfaces;

using MauronAlpha.Text.Interfaces;

using MauronAlpha.Forms.DataObjects;


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

		//Booleans
		private bool B_isRendering = false;
		public bool IsRendering { get { return B_isRendering; } }
		private bool B_needsRenderUpdate = false;
		public bool NeedsRenderUpdate { get { return B_needsRenderUpdate; } }

		//Methods
		public ConsoleApp_output SetConsole (MauronConsole console) {
			UNIT_console = console;
			return this;
		}
		public I_consoleOutput Write(I_textUnit unit) {
			Write(unit.AsString);
			return this;
		}
		public I_consoleOutput Write(string unit) {
			System.Console.Write(unit);
			return this;
		}
		public I_consoleOutput WriteLine(I_textUnit unit) {
			WriteLine(unit.AsString);
			return this;
		}
		public I_consoleOutput WriteLine(string txt) {
			System.Console.WriteLine(txt);
			return this;
		}

		public I_layoutRenderer Clear ( ) {
			System.Console.Clear();
			return this;
		}
		public I_layoutRenderer HandleRenderRequest(Layout2d_renderChain chain) {
			return this;
		}

		public I_consoleOutput SetCaretPosition (I_consoleUnit focus, CaretPosition position) {
			Vector2d pos = position.AsVector;
			pos.Add(focus.Context.Position.AsVector);

			System.Console.SetCursorPosition((int) pos.X, (int) pos.Y);
			return this;
		}
		public I_consoleOutput SetCaretPosition(Vector2d pos) {
			System.Console.SetCursorPosition((int)pos.X, (int)pos.Y);
			return this;
		}
		public I_consoleOutput SetScreenBufferSize(Vector2d size) {
			System.Console.SetBufferSize((int) size.X, (int) size.Y);
			return this;
		}	
	}
}
