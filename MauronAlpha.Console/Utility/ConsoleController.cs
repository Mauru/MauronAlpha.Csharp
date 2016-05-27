using MauronAlpha.Layout.Layout2d.Interfaces;

namespace MauronAlpha.Console.Utility {
	public class ConsoleController:I_layoutController {
		public string Name {
			get { throw new System.NotImplementedException(); }
		}

		public Events.EventHandler EventHandler {
			get { throw new System.NotImplementedException(); }
		}

		public I_layoutRenderer Output {
			get { throw new System.NotImplementedException(); }
		}

		public Layout.Layout2d.Context.Layout2d_context Context {
			get { throw new System.NotImplementedException(); }
		}

		public I_layoutUnit MainScreen {
			get { throw new System.NotImplementedException(); }
		}
	}
}
