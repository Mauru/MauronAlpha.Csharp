namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.DataObjects;

	public class PreRenderError:GameError {
		public PreRenderError(string message, object source) : base(message, source) { }
	}
}
