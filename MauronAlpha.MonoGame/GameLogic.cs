namespace MauronAlpha.MonoGame {
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

	/// <summary> Logic of the Game </summary>///
	public abstract class GameLogic :MonoGameComponent, I_sender<ReadyEvent> {
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
		public GameLogic() : base() { }

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

		//Initialize
		public virtual void Initialize() {
			B_isBusy = true;
			Lines result = GenerateInitializeReport(DATA_Manager);

			foreach(Line l in result)
				System.Console.WriteLine(l.AsString);
			

			B_initialized = true;
			B_isBusy = false;
		}

		public static Lines GenerateInitializeReport(GameManager m) {

			string spacer = " : ";
			Lines ll = new Lines();
			return ll;

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