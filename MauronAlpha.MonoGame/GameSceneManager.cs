namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.Interfaces;
	
	public class GameSceneManager:MonoGameComponent {

		public GameSceneManager(GameManager game) : base() {
			_game = game;
		}

		GameManager _game;

		I_GameScene _current;

		public void SetCurrent(I_GameScene scene) {
			_current = scene;
		}
	}
}
