using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Forms.Units;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.Forms.DataObjects;
using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.TestCases.Console {
		
	//Used to render the console
	public class ConsoleRenderer:CodeComponent,I_layoutRenderer {

		ConsoleInputManager Manager;
		public ConsoleRenderer(ConsoleInputManager manager) : base() {
			Manager = manager;
		}

		public void Update() {
			FormUnit_textField text = Manager.Text;
			Vector2d pos = text.Position.AsVector;
			foreach (Line line in text.Lines) {
				int x = (int) pos.X;
				int y = (int) pos.Y + line.Index;
				System.Console.SetCursorPosition(x, y);
				string output = line.AsVisualString.PadRight(System.Console.BufferWidth);
				System.Console.Write(output);
			}
			System.Console.SetCursorPosition(0, (int) Manager.DebugTxt.Position.Y);
			System.Console.Write(Manager.DebugTxt.LastLine.AsString.PadRight(System.Console.BufferWidth));
			SetCaretPosition(Manager.Text.CaretPosition);
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
