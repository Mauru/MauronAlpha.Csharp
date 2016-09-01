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

	using MauronAlpha.MonoGame.Rendering;

	/// <summary> Manages the rendering of content </summary>///
	public class GameRenderer :MonoGameComponent, I_CoreGameComponent, I_sender<ReadyEvent> {
		
		//The game manager (link to all other aspects of the game)
		GameManager DATA_Manager;
		public GameManager Game {
			get { return DATA_Manager; }
		}
		public void Set(GameManager o) {
			if(DATA_Manager == null)
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
				if(B_isBusy)
					return false;
				if(!B_isInitialized)
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

		SpriteBatch DATA_engineOutput; //The class that collects all sprites
		Color DATA_defaultSolidColor = Color.LightCoral; //Default Background color

		//Methods
		/// <summary> Tells the renderer if he should render a "busy-animation" </summary>
		public void SetReadyState(bool state) { }
		
		/// <summary> Initialize, requires engine </summary>
		public void Initialize() {
			if(B_isInitialized)
				return;
			if(B_isBusy)
				return;
			else
				B_isBusy = true;

			//Get properties from the engine
			GameEngine engine = DATA_Manager.Engine;

			//create the texture drawing thing
			DATA_engineOutput = new SpriteBatch(engine.GraphicsDevice);

			//Create the shader for drawing meshes
			GraphicsDevice device = engine.GraphicsDevice;
			DATA_defaultShader = new BasicEffect(device);

			//Set default texture
			_currentScreen = new Texture2D(engine.GraphicsDevice, 1, 1);

			//Set default rendertarget
			Screen = new RenderTarget2D(device, WindowSize.IntX, WindowSize.IntY,false, SurfaceFormat.Alpha8, DepthFormat.Depth24Stencil8);

			B_isInitialized = true;
			B_isBusy = false;
		}

		//Properties

		BasicEffect DATA_defaultShader;

		/// <summary> Gets rendered in initialize </summary>
		Texture2D _currentScreen;
		public Registry<RenderTarget2D> Targets;
		public RenderTarget2D Screen;

		public Texture2D LastRenderedScene {
			get {
				return _currentScreen;
			}
		}

		MonoGameTexture _blank;
		public MonoGameTexture UnrenderedTexture {
			get {
				if(_blank == null) {
				Texture2D t = new Texture2D(Game.Engine.GraphicsDevice, 1, 1, false, SurfaceFormat.Single);
				_blank = new MonoGameTexture(Game, t);
				}
				return _blank;
			}
		}

		//Property Returns
		public Vector2d ScreenSize {
			get {
				if(DATA_Manager == null)
					return new Vector2d();
				if(Game.EngineIsNull)
					return new Vector2d();
				GameEngine engine = Game.Engine;
				ScreenSize size = new ScreenSize(engine.GraphicsDevice);
				return new Vector2d(size.Width,size.Height);
			}
		}
		public Vector2d WindowSize {
			get {
				if(DATA_Manager == null)
					return new Vector2d();
				if(Game.EngineIsNull)
					return new Vector2d();
				GameEngine engine = Game.Engine;
				Viewport fov = engine.GraphicsDevice.Viewport;
				return new Vector2d(fov.Width, fov.Height);
			}
		}

		//MonoGame specific returns
		Matrix WorldMatrix() {
			return Matrix.CreateTranslation(0, 0, 0);
		}
		Matrix ViewMatrix() {
			return Matrix.CreateLookAt(
				new Vector3(0, 0, 3),
				new Vector3(0, 0, 0),
				new Vector3(0, 1, 0)
			);
		}
		Matrix ProjectionMatrix() {
			return Matrix.CreatePerspectiveFieldOfView(
				MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f
			);
		}

		//Draw event
		public void Draw(long time) {
			if(B_isBusy)
				return;
			if(!B_isInitialized)
				return;

			SolveRenderRequests(time);

			//Draw the final screen texture
			lock(LockStatus) {

				B_isBusy = true;
				DATA_renderTime = time;
				GraphicsDevice device = Game.Engine.GraphicsDevice;
				device.SetRenderTarget(null);

				device.Clear(Game.Engine.StateColor);

				DATA_engineOutput.Begin();
				DATA_engineOutput.Draw(_currentScreen,ScreenSizeAsRectangle,Color.White);	
				DATA_engineOutput.End();
				DATA_renderTime = time;
				B_isBusy = false;

			}

			return;
		}

		public void SolveRenderRequests(long time) {


			if(B_isPreRendering)
				return;

			CycleRenderRequests(time);			
			
		}

		Assign<Vector2d, RenderStage> _renderStages = new Assign<Vector2d, RenderStage>();

		public delegate I_RenderResult RenderMethod(RenderStage stage, I_Renderable target, long time);

		void CreateRenderTargets() {
			_renderStages.SetValue(ScreenSize, new RenderStage(Engine.GraphicsDevice, ScreenSize));
			_renderStages.SetValue(new Vector2d(1024,1024),new RenderStage(Engine.GraphicsDevice, 1024, 1024));
			_renderStages.SetValue(new Vector2d(512,512),new RenderStage(Engine.GraphicsDevice, 512, 512));
			_renderStages.SetValue(new Vector2d(256,256),new RenderStage(Engine.GraphicsDevice, 256, 256));
			_renderStages.SetValue(new Vector2d(128,128),new RenderStage(Engine.GraphicsDevice, 128, 128));
			_renderStages.SetValue(new Vector2d(64,64),new RenderStage(Engine.GraphicsDevice, 64, 64));
			_renderStages.SetValue(new Vector2d(32,32),new RenderStage(Engine.GraphicsDevice, 32, 32));
			_renderStages.SetValue(new Vector2d(16,16),new RenderStage(Engine.GraphicsDevice, 16, 16));
		}
		void CycleRenderRequests(long time) {
			if(Requests.IsEmpty)
				B_isPreRendering = false;
			B_isPreRendering = true;
			RenderRequest next = Requests.Pop;

			Vector2d size = next.Target.Bounds.Size;

			RenderStage stage = _renderStages.Value(next.RenderTargetSize);
			RenderMethod method = next.Target.RenderMethod;

			I_RenderResult result = method(stage, next.Target, time);
			next.SetResult(result);

			B_isPreRendering = false;

			CycleRenderRequests(time);
		}

		MonoGameTexture RenderToTexture(PolyShape drawable, GraphicsDevice device, RenderTarget2D target) {
			MauronAlpha.Geometry.Geometry2d.Units.Polygon2dBounds bounds = drawable.Bounds;

			TriangulationData data = drawable.RenderData;
			TriangleList tt = data.Triangles;

			// This step should be skippable / or placable elsewhere... maybe in the update loop?
			VertexPositionColor[] vv = tt.AsPositionColor;
			VertexBuffer buffer = new VertexBuffer(device, typeof(VertexPositionColor), vv.Length+1, BufferUsage.WriteOnly);
			buffer.SetData<VertexPositionColor>(vv);

			BasicEffect fx = DATA_defaultShader;
			fx.World = WorldMatrix();
			fx.View = ViewMatrix();
			fx.Projection = ProjectionMatrix();
			fx.VertexColorEnabled = true;

			RasterizerState rr = new RasterizerState();
			rr.CullMode = CullMode.None;
			device.RasterizerState = rr;
			// ...until here

			device.SetVertexBuffer(buffer);
			foreach (EffectPass pass in fx.CurrentTechnique.Passes){
				pass.Apply();
				device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
			}
			
			// Get data from the rendertarget and set it to a texture
			Vector2d size = bounds.Size;
			Texture2D texture = new Texture2D(device, size.IntX, size.IntY);
			Color[] colorData = new Color[size.IntX*size.IntY];
			target.GetData<Color>(colorData);
			texture.SetData<Color>(colorData);

			return new MonoGameTexture(Game,texture);
		}

		//Accessors
		GameEngine Engine {
			get {
				return Game.Engine;
			}
		}

		List<Vector2d> TotalSize(List<I_Drawable> ss) {

			Vector2d min = new Vector2d();
			Vector2d max = new Vector2d();
			foreach(I_Drawable s in ss) {
				Polygon2dBounds b = s.Bounds;
				Vector2d mi = b.Min;
				Vector2d mx = b.Max;
				if(mi.X < min.X)
					min.SetX(mi.X);
				if(mi.Y < min.Y)
					min.SetY(mi.Y);
				if(mx.X > max.X)
					max.SetX(mx.X);
				if(mx.Y > max.Y)
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
	
		//Handling Requests
		Stack<RenderRequest> Requests = new Stack<RenderRequest>();
		public void AddRequest(I_Renderable target, long time) {
			RenderRequest request = new RenderRequest(target,time);
			Requests.Add(request);
			return;
		}
		public void AddRequest(RenderRequest request) {
			Requests.Add(request);
			return;
		}

		Rectangle _screenSizeAsRectangle;
		Rectangle ScreenSizeAsRectangle {
			get {
				if(_screenSizeAsRectangle==null) {
					Vector2d v = Engine.GameWindow.SizeAsVector2d;
					_screenSizeAsRectangle = new Rectangle(0, 0, v.IntX, v.IntY);
				}
				return _screenSizeAsRectangle;
			}
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

	}

}