using MauronAlpha.MonoGame.Utility;

namespace MauronAlpha.MonoGame.Scripts {
	public class GameLogic:MonoGameComponent {

		GameManager Manager;
		internal bool B_initialized = false;
		public bool Initilized { get { return B_initialized; } }

		GameEngine Engine { get { return Manager.Engine; } }

		public GameLogic()	: base() {}

		public virtual void Initialize(GameManager manager) {
			Manager = manager;
			B_initialized = true;
		}

	}
}
