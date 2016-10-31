namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	using MauronAlpha.MonoGame.Collections;

	public class RenderQueue : Stack<I_PreRenderable> {

		GameManager _game;
		public GameManager Game { get { return _game; } }

		public RenderQueue(GameManager game): base() {
			_game = game;
		}

	}
}
