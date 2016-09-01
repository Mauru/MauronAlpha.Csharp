
namespace MauronAlpha.MonoGame {
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	//The Game Engine
	public class GameEngine : Game, I_sender<ReadyEvent> {
		GraphicsDeviceManager GraphicsDeviceManager;

		MonoGameComponent IdObj = new MonoGameComponent();
		public string Id {
			get { return IdObj.Id; }
		}

		GameManager DATA_Manager;
		public GameManager Game {
			get { return DATA_Manager; }
		}

		public bool CanExit {
			get { return false; }
		}

		public void DoNothing() { }

		//Constructor
		public GameEngine(GameManager o):base() {
			GraphicsDeviceManager = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			DATA_Manager = o;
			o.Set(this);
		}

		//Events
		Subscriptions<ReadyEvent> S_Ready = new Subscriptions<ReadyEvent>();
		public virtual void Subscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Add(s);
		}
		public virtual void UnSubscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Remove(s);
		}

		//0 - Initialize
		protected override void Initialize() {
			GameRenderer renderer = new GameRenderer(DATA_Manager);
			GameContentManager content = new GameContentManager(DATA_Manager);

			Game.Renderer.Initialize();
			Game.Logic.Initialize();
			base.Initialize();
		}

		//1 - LoadContent
		protected override void LoadContent() {

			

		}

		//2 - UnloadContent
		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		//3 - UpdateLogic
		protected override void Update(GameTime gameTime) {
			
			DATA_Manager.Logic.Cycle(gameTime.ElapsedGameTime.Ticks);

			base.Update(gameTime);
		}

		//4 - Draw calls
		protected override void Draw(GameTime gameTime) {
			DATA_Manager.Renderer.Draw(gameTime.ElapsedGameTime.Ticks);

			base.Draw(gameTime);
		}




	}
}

namespace MauronAlpha.MonoGame.Actuals {
	public class SampleGameLogic : GameLogic { }
}

namespace MauronAlpha.MonoGame {
	using MauronAlpha.ExplainingCode;
	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Collections;
	using MauronAlpha.Events.Interfaces;

	/// <summary> Base class for all MauronAlpha.MonoGame Code (exception GameEngine) </summary>///
	public class MonoGameComponent :MauronCode_component { }

	/// <summary> Ties all GameComponents together </summary>///
	public class GameManager :MonoGameComponent, I_sender<ReadyEvent> {

		public GameManager() {}

		GameLogic DATA_Logic;
		public GameLogic Logic { get { return DATA_Logic; } }

		GameContentManager DATA_Content;
		public GameContentManager Content { get { return DATA_Content; } }

		GameEngine DATA_Engine;
		public GameEngine Engine { get { return DATA_Engine; } }

		GameRenderer DATA_Renderer;
		public GameRenderer Renderer { get {return DATA_Renderer; }	}

		public bool IsReady {
			get {
				if (DATA_Logic == null)
					return false;
				if (DATA_Engine == null)
					return false;
				if (DATA_Content == null)
					return false;

				return true;
			}
		}

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

		//Events
		Subscriptions<ReadyEvent> S_Ready = new Subscriptions<ReadyEvent>();
		public virtual void Subscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Add(s);
		}
		public virtual void UnSubscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Remove(s);
		}
	}

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

		//Initialize
		public virtual void Initialize() {}

		//Events
		Subscriptions<ReadyEvent> S_Ready = new Subscriptions<ReadyEvent>();
		public virtual void Subscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Add(s);
		}
		public virtual void UnSubscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Remove(s);
		}
	
	}

	/// <summary> Manages external content for the game </summary>///
	public class GameContentManager :MonoGameComponent, I_sender<ReadyEvent> {
	
		GameManager DATA_Manager;
		public GameManager Game {
			get { return DATA_Manager; }
		}
		public void Set(GameManager o) {
			if(DATA_Manager == null)
				DATA_Manager = o;
			DATA_Manager.Set(this);
		}

		public GameContentManager(GameManager o) :base() {
			Set(o);
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

	/// <summary> Event fired when a component is "ready" </summary>///
	public class ReadyEvent :EventUnit_event {

		GameLogic DATA_Logic;
		GameEngine DATA_Engine;
		GameManager DATA_Manager;
		GameContentManager DATA_Content;
		GameRenderer DATA_Renderer;

		public ReadyEvent() : base("Ready") { }
		public ReadyEvent(GameLogic o) : this() {
			DATA_Logic = o;
		}
		public ReadyEvent(GameEngine o): this() {
			DATA_Engine = o;
		}
		public ReadyEvent(GameManager o): this() {
			DATA_Manager = o;
		}
		public ReadyEvent(GameContentManager o): this() {
			DATA_Content = o;
		}
		public ReadyEvent(GameRenderer o): this() {
			DATA_Renderer = o;
		}

	}

}

