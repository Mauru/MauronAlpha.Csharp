namespace MauronAlpha.MonoGame {
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Assets;

	/// <summary>	GameEngineWrapper for MonoGame	</summary>
	public class GameEngine : Game, I_CoreGameComponent, I_sender<ReadyEvent>, I_subscriber<AssetLoadEvent> {


		//Constructor
		public GameEngine(ref GameManager o):base() {
			GraphicsDeviceManager = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			Set(o);
		}

		//XNA related components
		GraphicsDeviceManager GraphicsDeviceManager;
		SpriteBatch SB_default;

		/// <summary>	To make this mauronCode compatible	</summary>
		MonoGameComponent IdObj = new MonoGameComponent();
		public string Id {
			get { return IdObj.Id; }
		}

		GameManager DATA_Manager;
		public GameManager Game {
			get { return DATA_Manager; }
		}
		public GameLogic Logic {
			get {
				return DATA_Manager.Logic;
			}
		}
		public GameRenderer Renderer {
			get {
				return DATA_Manager.Renderer;
			}
		}
		public AssetManager Assets {
			get {
				return DATA_Manager.Assets;
			}
		}
		public GameStateManager GameState {
			get { return DATA_Manager.GameState; }
		}
		
		//Start the game
		public void Start()  {
			base.Run(GameRunBehavior.Synchronous);
		}

		//Boolean properties
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
		public bool CanExit {
			get { return false; }
		}

		public void Set(GameManager o) {
			if(DATA_Manager == null)
				DATA_Manager = o;
			DATA_Manager.Set(this);
		}

		//0 - Initialize
		/// <summary> Dependencies </summary>
		protected override void Initialize() {
			
			SpriteBatch batch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
			SB_default = new SpriteBatch(GraphicsDevice);

			//Verify components and initialize them
			if(DATA_Manager.RendererIsNull)
				throw new GameError("Initialization chain is incorrect, {Renderer}.",DATA_Manager);
			Renderer.Initialize();
			if(DATA_Manager.AssetsIsNull)
				throw new GameError("Initialization chain is incorrect, {Assets}.",DATA_Manager);
			Assets.Initialize();
			if(DATA_Manager.GameStateIsNull)
				throw new GameError("Initialization chain is incorrect,  {GameState}.",DATA_Manager);
			GameState.Initialize();
			if(DATA_Manager.LogicIsNull)
				throw new GameError("Initialization chain is incorrect,  {Logic}.",DATA_Manager);
			Logic.Initialize();

			StateColor = Color.Yellow;
			B_isInitialized = true;
			base.Initialize();
		}

		//1 - LoadContent
		protected override void LoadContent() {
			StateColor = Color.White;
			Stack<LoadRequest> startUpAssets = Logic.Setup.PrepareRequiredAssets(Assets);
			AssetGroup loadThis = Assets.InitializeAssetGroup("Core",startUpAssets);
			loadThis.Subscribe(this);
			loadThis.Load();
			base.LoadContent();
		}

		//2 - UnloadContent
		protected override void UnloadContent() {
			base.UnloadContent();
		}

		//3 - UpdateLogic
		protected override void Update(GameTime gameTime) {
			if(!Logic.IsBusy)
				Logic.Cycle(gameTime.ElapsedGameTime.Ticks);
			base.Update(gameTime);
		}

		Vector2 DATA_centerOfScreen;
		Vector2 CentreOfScreen {
			get {
				if(DATA_centerOfScreen == null)
					DATA_centerOfScreen = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
				return DATA_centerOfScreen;
			}
		}

		public MonoGameWindow GameWindow {
			get {
				return new MonoGameWindow(Window);
			}
		}

		public Color StateColor = Color.Blue;

		//4 - Draw calls
		protected override void Draw(GameTime gameTime) {
			if(B_isInitialized) {
				if(Renderer.IsInitialized)
					Renderer.Draw(gameTime.ElapsedGameTime.Ticks);
				else
					GraphicsDevice.Clear(StateColor);
			} else {
				StateColor = Color.Red;
			}
			base.Draw(gameTime);
		}
	
		//x - Called on Exit
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

		//this is the preload event chain
		public bool ReceiveEvent(AssetLoadEvent e) {
			AssetGroup group = e.Target;
			group.UnSubscribe(this);
			StateColor = Color.Green;
			return true;
		}

		public bool Equals(I_subscriber<AssetLoadEvent> other) {
			return Id.Equals(other.Id);
		}
	}

}