using MauronAlpha.MonoGame.Scripts;
using MauronAlpha.MonoGame.Collections;

namespace MauronAlpha.MonoGame.Utility {
	public class GameObjectManager:MonoGameComponent {
		GameManager Manager;

		//constructor
		public GameObjectManager(GameManager manager) : base() {
				Manager = manager;
		}
	}
}
