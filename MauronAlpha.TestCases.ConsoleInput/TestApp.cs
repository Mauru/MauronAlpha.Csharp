using MauronAlpha.Console;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Input.Keyboard.Events;

namespace MauronAlpha.TestCases.Console {
	
	public class TestApp:ConsoleComponent,I_subscriber<Event_keyUp> {

		public ConsoleInputManager Input = new ConsoleInputManager();

		public ConsoleRenderer Renderer;

		public ConsoleWindow Window = new ConsoleWindow();

		public TestApp(): base() {
			Renderer = new ConsoleRenderer(Window);
			Window.Text.SetRenderManager(Renderer);
			Input.Subscribe(this);
		}

		public void Start() {
			Input.Start();
		}
		public void DoNothing() { }

		public bool ReceiveEvent(Event_keyUp e) {
			Window.Text.InsertAndUpdateContext(Input.CharacterOf(e.KeyPress));
			System.Console.WriteLine("HI"+Window.Text.AsString);

			//Renderer.Update();
			return true;
		}

		public bool CanExit {
			get {
				return false;
			}
		}

		public bool Equals(I_subscriber<Event_keyUp> other) {
			return Id.Equals(other.Id);
		}
	}
}
