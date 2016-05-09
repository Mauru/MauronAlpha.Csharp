using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Forms.Units;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.Forms.DataObjects;
using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Console.Units;

namespace MauronAlpha.Console.Utility {
		
	//Used to render the console
	public class ConsoleRenderer:ConsoleComponent,I_layoutRenderer {

		ConsoleWindow Window;

		//Constructor
		public ConsoleRenderer(ConsoleWindow window) : base() {
			Window = window;
		}

		public void Update() {
			FormUnit_textField text = Window.Text;
			Vector2d pos = text.Position.AsVector;
			foreach (Line line in text.Lines) {
				int x = (int) pos.X;
				int y = (int) pos.Y + line.Index;
				System.Console.SetCursorPosition(x, y);
				string output = line.AsVisualString.PadRight(System.Console.BufferWidth);
				System.Console.Write(output);
			}
			System.Console.SetCursorPosition((int)pos.X,(int)pos.Y);
		}

		public I_layoutRenderer Clear() {
			System.Console.Clear();
			return this;
		}

		public I_layoutRenderer HandleRenderRequest(Layout.Layout2d.Collections.Layout2d_renderChain chain) {
			return this;
		}

		private bool B_isRendering = false;
		public bool IsRendering {
			get { return B_isRendering; }
		}
		private bool B_needsUpdate = false;
		public bool NeedsRenderUpdate {
			get {
				return B_needsUpdate;
			}
		}

		public void SetCaretPosition(CaretPosition data) {
			Vector2d pos = data.AsVector;
			System.Console.SetCursorPosition((int) pos.X, (int) pos.Y);
		}
		public Vector2d WindowSize { get {
			return new Vector2d(System.Console.WindowWidth, System.Console.WindowHeight);
		} }
	}

}
