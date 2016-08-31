namespace MauronAlpha.MonoGame {
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

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
					DATA_setup = new GameSetup();
				return DATA_setup;
			}
		}

		//Initialize
		public virtual void Initialize() {
			B_initialized = true;
		}

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