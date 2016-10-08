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
		public GameRenderer(GameManager o)
			: base() {
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

			//Get properties from the engine
			GameEngine engine = DATA_Manager.Engine;



			//Create the shader for drawing meshes
			GraphicsDevice device = engine.GraphicsDevice;

			CreateRenderTargets();

			B_isInitialized = true;
			B_isBusy = false;
		}

		public GraphicsDevice GraphicsDevice {
			get {
				return Game.Engine.GraphicsDevice;
			}
		}


		void CreateRenderTargets() {
			GraphicsDevice device = Engine.GraphicsDevice;
			_renderStages.SetValue(ScreenSize, new RenderStage(device, ScreenSize));
			_renderStages.SetValue(new Vector2d(1024, 1024), new RenderStage(device, 1024, 1024));
			_renderStages.SetValue(new Vector2d(512, 512), new RenderStage(device, 512, 512));
			_renderStages.SetValue(new Vector2d(256, 256), new RenderStage(device, 256, 256));
			_renderStages.SetValue(new Vector2d(128, 128), new RenderStage(device, 128, 128));
			_renderStages.SetValue(new Vector2d(64, 64), new RenderStage(device, 64, 64));
			_renderStages.SetValue(new Vector2d(32, 32), new RenderStage(device, 32, 32));
			_renderStages.SetValue(new Vector2d(16, 16), new RenderStage(device, 16, 16));
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
			if (!_shaders.TryGet(name, ref result))
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

		//Camera
		Registry<Camera> _cameras = new Registry<Camera>();
		public Camera GetCamera(string name) {
			Camera result = null;
			if (!_cameras.TryGet(name, ref result))
				throw new GameError("Unknown Camera {" + name + "}!", this);
			return result;
		}
		Camera _currentCamera;
		public void SetCurrentCamera(Camera camera) {
			_currentCamera = camera;
		}
		public Camera CurrentCamera { get { return _currentCamera; } }

		DrawMethod _currentDrawMethod = GameRenderer.ShowEngineStateAsSolidColor;
		public delegate void DrawMethod(GameRenderer renderer, long time);
		public void SetDrawMethod(DrawMethod method) {
			_currentDrawMethod = method;
		}

		//Drawing
		public void Draw(long time) {

			DrawMethod method = _currentDrawMethod;
			method(this, time);

		}
		
		//RenderRequests
		void CycleRenderRequests(long time) {
			if (B_isPreRendering) {
				return;
			}
			if (_requestCount < 1) {
				return;
			}
			B_isPreRendering = true;
			RenderRequest next = Requests.Pop;
			I_Renderable target = next.Target;


			RenderStage stage = GetRenderStage(next.RenderTargetSize);
			/*RenderMethod method = next.Target.RenderMethod;

			I_RenderResult result = method(stage, target, time);
			target.SetRenderResult(result);
			*/
			_requestCount--;
			if (_requestCount > 0 && !B_isPreRendering)
				CycleRenderRequests(time);
			else if (_requestCount <= 0) {
				B_isPreRendering = false;
				return;
			}
		}
		public void SolveRenderRequests(long time) {
			if (_requestCount < 1)
				return;
			if (B_isPreRendering)
				return;

			CycleRenderRequests(time);

		}

		public delegate I_RenderResult RenderMethod(RenderStage stage, I_Renderable target, long time);

		Assign<Vector2d, RenderStage> _renderStages = new Assign<Vector2d, RenderStage>();
		public RenderStage GetRenderStage(Vector2d size) {
			foreach (RenderStage stage in _renderStages.Values)
				if (stage.Size.CompareTo(size) >= 0)
					return stage;
			throw new GameError("No valid RenderStage found for object (too large!)!", this);
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

		List<Vector2d> TotalSize(List<I_Drawable> ss) {

			Vector2d min = new Vector2d();
			Vector2d max = new Vector2d();
			foreach (I_Drawable s in ss) {
				Polygon2dBounds b = s.Bounds;
				Vector2d mi = b.Min;
				Vector2d mx = b.Max;
				if (mi.X < min.X)
					min.SetX(mi.X);
				if (mi.Y < min.Y)
					min.SetY(mi.Y);
				if (mx.X > max.X)
					max.SetX(mx.X);
				if (mx.Y > max.Y)
					max.SetY(mx.Y);
			}
			return new List<Vector2d>() { min, max };

		}

		long DATA_renderTime = 0;
		public long Time {
			get {
				return DATA_renderTime;
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

		public VertexPositionColor[] TestTriangle = new VertexPositionColor[3] {
			new VertexPositionColor(new Vector3(0,0,0),Color.Red),
			new VertexPositionColor(new Vector3(0,100,0),Color.Blue),
			new VertexPositionColor(new Vector3(100,100,0),Color.Green),
		};

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

		//Static utility functions
		public static Vector2 AsVector2(Vector2d v) {
			return new Vector2(v.FloatX, v.FloatY);
		}

		//Static RenderCalls
		static void ShowEngineStateAsSolidColor(GameRenderer renderer, long time) {
			renderer.ClearScreen(renderer.Engine.StateColor);
		}

	}
}
