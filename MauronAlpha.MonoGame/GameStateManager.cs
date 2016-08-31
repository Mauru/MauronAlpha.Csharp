
namespace MauronAlpha.MonoGame {


	public class GameStateManager :MonoGameComponent, I_CoreGameComponent {

		GameManager DATA_Manager;
		public GameManager Game {
			get {
				return DATA_Manager;
			}
		}
		public void Set(GameManager o) {
			if(DATA_Manager == null)
				DATA_Manager = o;
			DATA_Manager.Set(this);
		}

		//constructor
		public GameStateManager(GameManager game): base() {
			Set(game);
		}

		//Boolean states
		bool B_isBusy = false;
		public bool IsBusy {
			get { return B_isBusy; }
		}
		bool B_isInitialized = false;
		public bool IsInitialized {
			get { return B_isInitialized; }
		}
		
		//Methods
		public void Initialize() {
			B_isInitialized = true;
		}

	}

}

