namespace MauronAlpha.MonoGame {
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Geometry;

	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Collections;
	using MauronAlpha.Events.Interfaces;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	/// <summary> Manages the rendering of content </summary>///
	public class GameRenderer : MonoGameComponent, I_CoreGameComponent, I_sender<ReadyEvent> {

		//The game manager (link to all other aspects of the game)
		GameManager DATA_Manager;
		public GameManager Game {
			get { return DATA_Manager; }
		}
		public void Set(GameManager o) {
			if (DATA_Manager == null)
				DATA_Manager = o;
			DATA_Manager.Set(this);
		}

		//used for multithreading
		System.Object LockStatus;

		//Constructor
		public GameRenderer(GameManager o): base() {
			LockStatus = new System.Object();
			Set(o);
			B_isBusy = false;
		}

		//Boolean statuses
		bool GameIsBusy {
			get {
				return Game.IsReady == false;
			}
		}
		public bool CanRender {
			get {
				if (B_isBusy)
					return false;
				if (!B_isInitialized)
					return false;
				return true;
			}
		}

		bool B_isBusy = true;
		public bool IsBusy {
			get { return B_isBusy; }
		}

		bool B_isInitialized = false;
		public bool IsInitialized {
			get {
				return B_isInitialized;
			}
		}

		bool B_isPreRendering = false;
		public bool IsPrerendering {
			get {
				return B_isPreRendering;
			}
		}

		//Methods
		/// <summary> Tells the renderer if he should render a "busy-animation" </summary>
		public void SetReadyState(bool state) { }

		/// <summary> Initialize, requires engine </summary>
		public void Initialize() {
			if (B_isInitialized)
				return;
			if (B_isBusy)
				return;
			else
				B_isBusy = true;

			GameEngine engine = DATA_Manager.Engine;
			
			GraphicsDevice device = engine.GraphicsDevice;

			_spriteDrawManager = new SpriteDrawManager(device);
			Texture2D pixel = new Texture2D(engine.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			System.Int32[] pixelData = { 0xFFFFFF };
			pixel.SetData<System.Int32>(pixelData, 0, 1);
			_pixelTexture = pixel;

			CreateRenderTargets();

			B_isInitialized = true;
			B_isBusy = false;
		}

		public GraphicsDevice GraphicsDevice {
			get {
				return Game.Engine.GraphicsDevice;
			}
		}



		I_GameScene _currentScene;
		public void SetCurrentScene(I_GameScene scene) {
			_currentScene = scene;
		}
		public I_GameScene CurrentScene { get { return _currentScene; } }

		//Properties
		Registry<I_Shader> _shaders = new Registry<I_Shader>();
		public I_Shader GetShader(string name) {
			I_Shader result = null;
			if (!_shaders.Try(name, ref result))
				throw new GameError("Unknown shader {" + name + "}!", this);
			return result;
		}
		
		I_Shader _currentShader;
		public I_Shader CurrentShader { get { return _currentShader; } }
		public void SetCurrentShader(I_Shader shader) {
			_currentShader = shader;
		}

		MonoGameTexture _blank;
		public MonoGameTexture UnrenderedTexture {
			get {
				if (_blank == null) {
					Texture2D t = new Texture2D(Game.Engine.GraphicsDevice, 1, 1, false, SurfaceFormat.Single);
					_blank = new MonoGameTexture(Game, t);
				}
				return _blank;
			}
		}

		SpriteDrawManager _spriteDrawManager;
		public SpriteDrawManager SpriteDrawManager { get { return _spriteDrawManager; } }

		Texture2D _pixelTexture;
		public Texture2D PixelTexture {
			get {
				return _pixelTexture;
			}
		}


		//Property Returns
		public Vector2d ScreenSize {
			get {
				if (DATA_Manager == null)
					return new Vector2d();
				if (Game.EngineIsNull)
					return new Vector2d();
				GameEngine engine = Game.Engine;
				ScreenSize size = new ScreenSize(engine.GraphicsDevice);
				return new Vector2d(size.Width, size.Height);
			}
		}
		public Vector2d WindowSize {
			get {
				if (DATA_Manager == null)
					return new Vector2d();
				if (Game.EngineIsNull)
					return new Vector2d();
				GameEngine engine = Game.Engine;
				Viewport fov = engine.GraphicsDevice.Viewport;
				return new Vector2d(fov.Width, fov.Height);
			}
		}

		DrawMethod _currentDrawMethod = GameRenderer.ShowEngineStateAsSolidColor;
		public delegate void DrawMethod(GameRenderer renderer, long time);
		public void SetDrawMethod(DrawMethod method) {
			_currentDrawMethod = method;
		}

		//Drawing
		public void Draw(long time) {
			DrawMethod method = _currentDrawMethod;
			method(this, DATA_renderTime);
			DATA_renderTime++;
		}

		//RenderTarget
		RenderStage _defaultRenderTarget;
		public RenderStage DefaultRenderTarget {
			get { return _defaultRenderTarget; }
		}
		RenderStage _activeStage;
		public void SetActiveStage(RenderStage obj) {
			_activeStage = obj;
			GraphicsDevice.SetRenderTarget(obj.RenderTarget);
		}
		void CreateRenderTargets() {
			GraphicsDevice device = Engine.GraphicsDevice;
			_defaultRenderTarget = new RenderStage(Game, ScreenSize);
			_queue = new PreRenderQueue(Game);
		}

		public delegate I_RenderResult RenderMethod(RenderStage stage, I_Renderable target, long time);
	
		//PreRendering
		PreRenderQueue _queue;
		public PreRenderQueue Queue {
			get {
				return _queue;
			}
		}

		public delegate void PreRenderEventHandler(GameRenderer renderer, SpriteBuffer result, long time);
		PreRenderEventHandler _preRenderEventHandler = NoPreRenderEvent;
		public PreRenderEventHandler HandlePreRenderEvent {
			get { return _preRenderEventHandler; }
		}
		public void SetPreRenderHandler(PreRenderEventHandler handler) {
			_preRenderEventHandler = handler;
		}
		public static void NoPreRenderEvent(GameRenderer renderer, SpriteBuffer buffer, long time) { }

		//Render-time
		long DATA_renderTime = 0;
		public long Time {
			get {
				return DATA_renderTime;
			}
		}


		public void ClearScreen(Color color) {
			GraphicsDevice.Clear(color);
		}

		//Accessors
		public GameEngine Engine {
			get {
				return Game.Engine;
			}
		}
		public GameLogic Logic {
			get {
				return Game.Logic;
			}
		}
		public AssetManager Assets {
			get {
				return Game.Assets;
			}
		}

		//Events
		Subscriptions<ReadyEvent> S_Ready = new Subscriptions<ReadyEvent>();
		public virtual void Subscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Add(s);
		}
		public virtual void UnSubscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Remove(s);
		}

		int _requestCount = 0;

		//Handling Requests
		Stack<RenderRequest> Requests = new Stack<RenderRequest>();
		public void AddRequest(I_Renderable target, long time) {
			RenderRequest request = new RenderRequest(target, time);
			Requests.Add(request);
			_requestCount++;
			return;
		}
		public void AddRequest(RenderRequest request) {
			Requests.Add(request);
			_requestCount++;
			return;
		}

		/* some circle drawing logic...
		 public void Circle(Texture2D tex, int cx, int cy, int r, Color col) {
			int x, y, px, nx, py, ny, d;
             
			for (x = 0; x <= r; x++){
				d = (int) Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
				for (y = 0; y <= d; y++) {
                     px = cx + x;
                     nx = cx - x;
                     py = cy + y;
                     ny = cy - y;
     
                     tex.SetPixel(px, py, col);
                     tex.SetPixel(nx, py, col);
      
                     tex.SetPixel(px, ny, col);
                     tex.SetPixel(nx, ny, col);
     
                 }
             }    
         }*/


		//Get the current renderStage as Color representation
		public Color StatusColor {
			get {
				return RenderStatusColors.Undefined;
			}
		}

		//Static utility functions
		public static Vector2 AsVector2(Vector2d v) {
			return new Vector2(v.FloatX, v.FloatY);
		}

		//Static RenderCalls
		static void ShowEngineStateAsSolidColor(GameRenderer renderer, long time) {
			renderer.ClearScreen(renderer.Engine.StateColor);
		}

		//Useful proxies
		public MonoGameWindow GameWindow {
			get { return Game.Engine.GameWindow; }
		}
		public Vector2d CenterOfScreen {
			get {
				return GameWindow.Center;
			}
		}

	}
}
