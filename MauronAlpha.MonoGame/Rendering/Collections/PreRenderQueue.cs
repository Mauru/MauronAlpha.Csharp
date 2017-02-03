namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.MonoGame.Rendering.Interfaces;

	public class PreRenderQueue:List<I_PreRenderable> {

		GameManager _game;
		public GameManager Game { get { return _game; } }

		public PreRenderQueue(GameManager game) : base() {
			_game = game;
		}
	}
}
