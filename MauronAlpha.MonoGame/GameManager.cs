namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;


	/// <summary> Ties all GameComponents together </summary>///
	public class GameManager :MonoGameComponent, I_sender<ReadyEvent>, I_subscriber<ReadyEvent> {

		public GameManager() { }


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

		//ContentManager
		GameContentManager DATA_Content;
		public GameContentManager Content { get { return DATA_Content; } }
		public bool ContentIsNull {
			get {
				return DATA_Content == null;
			}
		}
		public bool ContentIsInitialized {
			get {
				if( DATA_Content == null)
					return false;
				return DATA_Content.IsInitialized;
			}
		}
		public string ContentState {
			get {
				if(ContentIsNull)
					return "NULL";
				if(ContentIsInitialized)
					return "INITIALIZED";
				if(Content.IsBusy)
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
		/// <summary>
		/// Requires that the renderer is not null, the base config file needs to have loaded and 
		/// </summary>
		public bool EngineIsInitialized {
			get {
				if(DATA_Engine == null)
					return false;
				if(DATA_Content == null)
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
			if(DATA_Logic == null)
				return false;
			if(DATA_Engine == null)
				return false;
			if(DATA_Content == null)
				return false;

			return true;
			}
		}

		//Register core components
		public void Set(GameLogic o) {
		if(DATA_Logic == null)
			DATA_Logic = o;
		return;
		}
		public void Set(GameContentManager o) {
		if(DATA_Content == null)
			DATA_Content = o;
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

		public static bool CONDITION_AllInitialized(GameManager source) {
			return source.IsReady;
		}
		public static bool PROBE_IsBusy(GameManager source) {
			bool result = source.IsReady;
			if(source.RendererIsInitialized)
				source.Renderer.SetReadyState(result);
			return result;
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


}

namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.MonoGame.Interfaces;


	public class StatusInfo :MonoGameComponent {

		GameManager DATA_manager;
		public StatusInfo(GameManager manager) {
			DATA_manager = manager;
		}

	}

}

namespace MauronAlpha.MonoGame.Interfaces {

	public interface I_VisualGameStatus {

		void Message(string message);


	}

}