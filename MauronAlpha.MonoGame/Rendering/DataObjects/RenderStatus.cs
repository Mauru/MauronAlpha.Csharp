namespace MauronAlpha.MonoGame.Rendering.DataObjects {

	using MauronAlpha.MonoGame.Rendering.Interfaces;

	/// <summary> An object holding status information on a render-process </summary>
	public class RenderStatus:MonoGameComponent, I_RenderStatus {

		string _message;
		public string Message { get { return _message; } }

		public RenderStatus(string message)	: base() {
			_message = message;
		}

		public static RenderStatus Create(string message) {
			return new RenderStatus(message);
		}
	}

}
