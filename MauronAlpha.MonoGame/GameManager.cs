namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	/// <summary> Ties all GameComponents together </summary>///
	public class GameManager :MonoGameComponent, I_sender<ReadyEvent>, I_subscriber<ReadyEvent> {

		//constructor
		public GameManager() {}

		//GameState
		GameStateManager DATA_GameState;
		public GameStateManager GameState {
			get {
				return DATA_GameState;
			}
		}
		public bool GameStateIsNull {
			get {
				return DATA_GameState == null;
			}
		}
		public bool GameStateIsInitialized {
			get {
				if(DATA_GameState == null)
					return false;
				return DATA_GameState.IsInitialized;
			}
		}
		public string GameStateState {
			get {
				if(GameStateIsNull)
					return "NULL";
				if(GameStateIsInitialized)
					return "INITIALIZED";
				if(GameState.IsBusy)
					return "BUSY";
				return "READY";
			}
		}

		//GameLogic
		GameLogic DATA_Logic;
		public GameLogic Logic { get { return DATA_Logic; } }
		public bool LogicIsNull {
			get {
				return DATA_Logic == null;
			}
		}
		public bool LogicIsInitialized {
			get {
			if(DATA_Logic == null)
				return false;
			return DATA_Logic.IsInitialized;
			}
		}
		public string LogicState {
			get {
				if(LogicIsNull)
					return "NULL";
				if(LogicIsInitialized)
					return "INITIALIZED";
				if(Logic.IsBusy)
					return "BUSY";
				return "READY";
			}
		}

		//AssetManager
		AssetManager DATA_Assets;
		public AssetManager Assets { get { return DATA_Assets; } }
		public bool AssetsIsNull {
			get {
				return DATA_Assets == null;
			}
		}
		public bool AssetsIsInitialized {
			get {
				if( DATA_Assets == null)
					return false;
				return DATA_Assets.IsInitialized;
			}
		}
		public string AssetsState {
			get {
				if(AssetsIsNull)
					return "NULL";
				if(AssetsIsInitialized)
					return "INITIALIZED";
				if(DATA_Assets.IsBusy)
					return "BUSY";
				return "READY";
			}
		}

		//GameEngine
		GameEngine DATA_Engine;
		public GameEngine Engine { get { return DATA_Engine; } }
		public bool EngineIsNull {
			get {
				return DATA_Engine == null;
			}
		}
		/// <summary> Requires that the renderer is not null, the base config file needs to have loaded </summary>
		public bool EngineIsInitialized {
			get {
				if(DATA_Engine == null)
					return false;
				if(DATA_Assets == null)
					return false;
				if(DATA_Renderer == null)
					return false;
				return DATA_Engine.IsInitialized;
			}
		}
		public string EngineState {
			get {
				if(EngineIsNull)
					return "NULL";
				if(EngineIsInitialized)
					return "INITIALIZED";
				if(Engine.IsBusy)
					return "BUSY";
				return "READY";
			}
		}

		//Renderer
		GameRenderer DATA_Renderer;
		public GameRenderer Renderer { get { return DATA_Renderer; } }
		public bool RendererIsNull {
			get {
				return DATA_Engine == null;
			}
		}
		public bool RendererIsInitialized {
			get {
				if(DATA_Renderer == null)
					return false;
				return DATA_Renderer.IsInitialized;
			}
		}
		public string RendererState {
			get {
				if(RendererIsNull)
					return "NULL";
				if(RendererIsInitialized)
					return "INITIALIZED";
				if(Renderer.IsBusy)
					return "BUSY";
				return "READY";
			}
		}

		//Status booleans
		public bool IsReady {
			get {
				if(DATA_GameState == null)
					return false;
				if(DATA_Logic == null)
					return false;
				if(DATA_Assets == null)
					return false;
				if(DATA_Engine == null)
					return false;
				if(DATA_Renderer == null)
					return false;
				return true;
			}
		}

		//Register core components
		public void Set(GameStateManager o) {
			if(DATA_GameState == null)
				DATA_GameState = o;
			return;
		}
		public void Set(GameLogic o) {
		if(DATA_Logic == null)
			DATA_Logic = o;
		return;
		}
		public void Set(AssetManager o) {
		if(DATA_Assets == null)
			DATA_Assets = o;
		return;
		}
		public void Set(GameEngine o) {
		if(DATA_Engine == null)
			DATA_Engine = o;
		return;
		}
		public void Set(GameRenderer o) {
			if(DATA_Renderer == null)
				DATA_Renderer = o;
			return;
		}

		//Event Handler ReadyEvent
		Subscriptions<ReadyEvent> S_Ready = new Subscriptions<ReadyEvent>();
		public virtual void Subscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Add(s);
		}
		public virtual void UnSubscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Remove(s);
		}
		public bool ReceiveEvent(ReadyEvent e) {
			return true;
		}
		public bool Equals(I_subscriber<ReadyEvent> other) {
			return Id.Equals(other.Id);
		}
	
	}

	public interface I_CoreGameComponent {

		void Set(GameManager o);
		GameManager Game { get; }

		bool IsBusy { get; }
		bool IsInitialized { get; }
	}

}