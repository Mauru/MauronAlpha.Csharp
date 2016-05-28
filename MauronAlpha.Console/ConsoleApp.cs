using MauronAlpha.Events.Interfaces;
using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Events;

using MauronAlpha.Forms.Units;

using MauronAlpha.Layout.Layout2d.Interfaces;


using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.TextProcessing.Units;

using MauronAlpha.FileSystem.Units;

namespace MauronAlpha.Console {

	public class ConsoleApp:ConsoleComponent,I_subscriber<Event_keyUp> {

		public ConsoleApp() : base() { }

		public ConsoleApp(string text): base() {
			Text = new FormUnit_textField(text);
		}
		public ConsoleApp(string text, File file) : base() {
			file.Append(text, true);
			Text = new FormUnit_textField(text);
		}

		public void Start() {
			Input = new ConsoleInput();
			Input.Subscribe(this);

			if (!Text.IsEmpty)
				Layout.RenderContent(Text);

			Input.Listen();
		}

		ConsoleAppLayout LayoutController;
		public ConsoleAppLayout Layout {
			get {
				if (LayoutController == null)
					LayoutController = new Layout_singleText(this);
				return LayoutController;
			}
		}
		public ConsoleInput Input;

		public bool CanExit = false;

		public FormUnit_textField Text = new FormUnit_textField();

		public string Name {
			get {
				string response = "ConsoleApp(" + Id + ")";
				return response;
			}
		}

		public bool ReceiveEvent(Event_keyUp e) {
			KeyPress key = e.KeyPress;

			//if(!key.IsFunction)
			return true;
		}

		public bool Equals(I_subscriber<Event_keyUp> other) {
			return Id.Equals(other.Id);
		}

	}

	public abstract class ConsoleAppLayout : ConsoleComponent, I_layoutController {

		ConsoleApp Application;

		public ConsoleAppLayout(ConsoleApp application) : base() {
			Application = application;
		}

		public string Name {
			get { return "Generic MauronAlpha.ConsoleApp"; }
		}

		public Events.EventHandler EventHandler {
			get { throw new System.NotImplementedException("EventHandlerUse is obsolete!"); }
		}

		public I_layoutRenderer Output {
			get { throw new System.NotImplementedException("Let's see how we solve this."); }
		}

		public Layout.Layout2d.Context.Layout2d_context Context {
			get { throw new System.NotImplementedException("Do we use a custom context or the default layout context?"); }
		}

		public I_layoutUnit MainScreen {
			get { throw new System.NotImplementedException("The main screen would be the consoleWindow I guess."); }
		}

		public abstract void RenderContent(FormUnit_textField text);
	
	}



	public class Layout_singleText : ConsoleAppLayout {
		public Layout_singleText(ConsoleApp app) : base(app) { }

		public override void RenderContent(FormUnit_textField text) {
			System.Console.Clear();
			Lines ll = text.Lines;
			foreach (Line l in ll)
				System.Console.WriteLine(l.AsVisualString);
		}


	}
}
