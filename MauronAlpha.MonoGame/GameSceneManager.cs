namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;
	
	public class GameSceneManager:MonoGameComponent {

		public GameSceneManager(GameManager game) : base() {
			_game = game;
		}

		GameManager _game;

		I_GameScene _current;
		Registry<I_GameScene> _scenes = new Registry<I_GameScene>();

		public bool Try(string name, ref I_GameScene scene) {
			return _scenes.TryGet(name, ref scene);
		}

		public void RegisterAndInitialize(string name, I_GameScene scene) {
			_scenes.Add(name, scene);
			scene.Initialize();
		}

		public void SetCurrent(I_GameScene scene) {
			_current = scene;
		}
	}
}
