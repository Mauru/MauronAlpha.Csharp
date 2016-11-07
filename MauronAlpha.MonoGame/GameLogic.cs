namespace MauronAlpha.MonoGame {
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.DataObjects;

	/// <summary> Logic of the Game </summary>///
	public abstract class GameLogic :MonoGameComponent, I_CoreGameComponent, I_sender<ReadyEvent> {
		GameManager DATA_Manager;
		public GameManager Game {
			get { return DATA_Manager; }
		}
		public void Set(GameManager o) {
		if(DATA_Manager == null)
			DATA_Manager = o;
			DATA_Manager.Set(this);
		}

		//Constructor
		public GameLogic(GameManager manager) : base() {
			Set(manager);
		}

		///<summary>Performs a cycle of the gamelogic</summary>
		public virtual void Cycle(long time) { }

		bool B_initialized = false;
		public bool IsInitialized {
			get { return B_initialized; }
		}

		bool B_isBusy = false;
		public bool IsBusy {
			get {
				return B_isBusy;
			}
		}

		GameSetup DATA_setup;
		public virtual GameSetup Setup {
			get {
				if(DATA_setup == null)
					DATA_setup = new GameSetup(DATA_Manager);
				return DATA_setup;
			}
		}

		GameSceneManager _scenes;
		public GameSceneManager SceneManager {
			get {
				return _scenes;
			}
		}
		public I_GameScene _currentScene;
		public I_GameScene CurrentScene {
			get {
				return _currentScene;
			}
		}
		public void SetCurrentScene(I_GameScene scene) {
			_currentScene = scene;
		}

		//Initialize
		public virtual void Initialize() {
			_scenes = new GameSceneManager(Game);
			B_initialized = true;
		}

		public abstract void SetStartUpScene();

		//Events
		Subscriptions<ReadyEvent> S_Ready = new Subscriptions<ReadyEvent>();
		public virtual void Subscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Add(s);
		}
		public virtual void UnSubscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Remove(s);
		}
	}


}