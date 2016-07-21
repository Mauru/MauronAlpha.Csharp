namespace MauronAlpha.MonoGame {
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.CustomInjected;

	/// <summary>	GameEngineWrapper for MonoGame	</summary>
	public class GameEngine : Game, I_sender<ReadyEvent> {
		GraphicsDeviceManager GraphicsDeviceManager;

		/// <summary>	To make this mauronCode compatible	</summary>
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

			//Hook up the content with the debug Interface
			Content.DebugReceiver = new MauronAlpha.MonoGame.CustomInjected.DebugInterface();

			DATA_Manager = o;
			o.Set(this);
		}

		public void Start()  {
			base.Run(GameRunBehavior.Synchronous);
		}

		protected override void EndRun() {

			base.EndRun();

		}

		//Events
		Subscriptions<ReadyEvent> S_Ready = new Subscriptions<ReadyEvent>();
		public virtual void Subscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Add(s);
		}
		public virtual void UnSubscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Remove(s);
		}

		bool B_isInitialized = false;
		public bool IsInitialized {
			get {
				return B_isInitialized;
			}
		}
		bool B_isReady = false;
		public bool IsReady {
			get {
				return B_isReady;
			}
		}

		public bool IsBusy {
			get { return false; }
		}

		//0 - Initialize
		/// <summary> Dependencies </summary>
		protected override void Initialize() {

			GameRenderer renderer = new GameRenderer(DATA_Manager);
			SpriteBatch batch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);

			Game.Logic.Initialize();

			SB_default = new SpriteBatch(GraphicsDevice);

			base.Initialize();

		}

		//1 - LoadContent
		protected override void LoadContent() {

			GameFont font = Game.Content.LoadGameFont("robotoLight.unaliased.16");
			Game.Content.SetDefaultFont(font);
			base.LoadContent();
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

		SpriteBatch SB_default;

		Vector2 DATA_centerOfScreen;
		Vector2 CentreOfScreen {
			get {
			if(DATA_centerOfScreen == null)
				DATA_centerOfScreen = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
			return DATA_centerOfScreen;
			}
		}

		//4 - Draw calls
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Red);
			
			//DATA_Manager.Renderer.Draw(gameTime.ElapsedGameTime.Ticks);
			base.Draw(gameTime);
		}
	}

}