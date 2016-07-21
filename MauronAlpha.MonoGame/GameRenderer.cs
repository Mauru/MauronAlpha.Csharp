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

	/// <summary> Manages the rendering of content </summary>///
	public class GameRenderer :MonoGameComponent, I_sender<ReadyEvent> {
		
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
		bool CanRender {
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

		//MonoGame related properties
		RenderBuffer Buffer = new RenderBuffer();

		SpriteBatch DATA_engineOutput; //The class that collects all sprites
		Color DATA_defaultSolidColor = Color.LightCoral; //Default Background color

		//Methods
		/// <summary> Tells the renderer if he should render a "busy-animation" </summary>
		public void SetReadyState(bool state) { }
		
		//Initialize the renderer
		public void Initialize() {
			if(B_isBusy)
				return;
			if(B_isInitialized)
				return;
			if(!DATA_Manager.EngineIsNull)
				return;

			//Get properties from the engine
			GameEngine engine = DATA_Manager.Engine;
			//create the texture drawing thing
			DATA_engineOutput = new SpriteBatch(engine.GraphicsDevice);

			//Create the shader for drawing meshes
			GraphicsDevice device = engine.GraphicsDevice;
			DATA_defaultShader = new BasicEffect(device);

			//Set default texture
			CurrentScreenTexture = new Texture2D(engine.GraphicsDevice, 1, 1);

			//Set default rendertarget
			Screen = new RenderTarget2D(device, WindowSize.IntX, WindowSize.IntY,false,SurfaceFormat.Alpha8,DepthFormat.Depth24Stencil8);

			B_isInitialized = true;
		}

		//Properties
		long DATA_renderTime = 0;	
		BasicEffect DATA_defaultShader;

		Texture2D CurrentScreenTexture;
		public Registry<RenderTarget2D> Targets;
		public RenderTarget2D Screen;

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
		
			//Draw the final screen texture
			lock(LockStatus) {

				B_isBusy = true;
				DATA_renderTime = time;
				GraphicsDevice device = Game.Engine.GraphicsDevice;
				device.SetRenderTarget(null);

				device.Clear(DATA_defaultSolidColor);

				DATA_engineOutput.Begin();
				//DATA_engineOutput.Draw(CurrentScreenTexture,ScreenSizeAsRectangle,Color.White);	
				DATA_engineOutput.End();
				DATA_renderTime = time;
				B_isBusy = false;
			}

			return;
		}

		MonoGameSprite RenderToTexture(PolyShape drawable, GraphicsDevice device, RenderTarget2D target) {
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

			return new MonoGameSprite(Game,bounds,texture);
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
		public void AddRequest(Drawable target, RenderInstructions instructions, long time) {
			RenderRequest request = new RenderRequest(target,instructions,time);
			Requests.Add(request);
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

	}

}

namespace MauronAlpha.MonoGame.Interfaces {
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	/// <summary> Represents an item that can be rendered </summary>
	public interface I_Drawable {

		Polygon2dBounds Bounds { get; }

		List<Polygon2d> Shapes { get; }
		List<MonoGameSprite> Sprites { get; }
		MonoGameSprite Rendered { get; }

		bool NeedsRenderUpdate {	get; }

		void SetRenderResult(MonoGameSprite t, long renderTime);

		GameRenderer Renderer { get; }

	}
}



namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.HandlingErrors;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Interfaces;
	
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

	using Microsoft.Xna.Framework.Graphics;
	
		
	//constructor
	public class Drawable :MonoGameComponent, I_Drawable {

		RenderInstructions DATA_instructions;
		public RenderInstructions Instructions { 
			get {
				if(DATA_instructions == null)
					return RenderInstructions.Default;
				return DATA_instructions; 
			} 
		}

		public Drawable(GameManager game, Polygon2dBounds bounds, RenderInstructions instructions): base() {
			DATA_game = game;
			DATA_bounds = bounds;
			DATA_instructions = instructions;
		}

		GameManager DATA_game;
		public GameManager Game {
			get {
				return DATA_game;
			}
		}
		public GameRenderer Renderer {
			get { return Game.Renderer; }
		}

		long DATA_lastRendered = 0;
		public long LastRendered {
			get { return DATA_lastRendered; }
		}

		long DATA_frame;
		public long AnimationFrame {
			get {
				return DATA_frame;
			}
		}

		Matrix2d DATA_matrix = new Matrix2d();
		Matrix2d Matrix { get { return DATA_matrix; } }

		TriangleList DATA_triangles;
		public TriangleList Triangles {
			get {
				if(DATA_triangles == null)
					return new TriangleList(new Triangle2d[]{});
				return DATA_triangles;
			}
		}

		Polygon2dBounds DATA_bounds;
		public Polygon2dBounds Bounds {
			get { return DATA_bounds; }
		}

		List<Polygon2d> DATA_shapes = new List<Polygon2d>();
		public List<Polygon2d> Shapes {
			get { return DATA_shapes; }
		}
		List<Line2d> DATA_lines = new List<Line2d>();
		public List<Line2d> Lines {
			get {
				return DATA_lines;
			}
		}
		List<MonoGameSprite> DATA_sprites = new List<MonoGameSprite>();
		public List<MonoGameSprite> Sprites {
			get {
				return DATA_sprites;
			}
		}

		MonoGameSprite DATA_rendered;
		public MonoGameSprite Rendered {
			get {
				if(DATA_rendered == null)
					return new MonoGameSprite(DATA_game, DATA_bounds, ClearTexture);
				return DATA_rendered;			
			}
		}

		Texture2D TEX_default;
		Texture2D ClearTexture {
			get {
				if(TEX_default == null)
					TEX_default = new Texture2D(Game.Engine.GraphicsDevice, 1, 1,false,SurfaceFormat.Alpha8);
				return TEX_default;
			}
		}

		bool B_needsRenderUpdate = false;
		public bool NeedsRenderUpdate {
			get { return B_needsRenderUpdate; }
		}

		public void SetRenderResult(MonoGameSprite t, long renderTime) {
			DATA_rendered = t;
			DATA_lastRendered = renderTime;
			B_needsRenderUpdate = false;
		}

	}

	public class MonoGameText :Drawable {
		public MonoGameText(GameManager game, Polygon2dBounds bounds) : base(game, bounds, RenderInstructions.Text) { }
	}
	public class MonoGameLines :Drawable {
		public MonoGameLines(GameManager game, Polygon2dBounds bounds) : base(game, bounds, RenderInstructions.Lines) { }
	}
	public class MonoGameShape :Drawable {
		Polygon2d DATA_polygon;
		public MonoGameShape(GameManager game, Polygon2d poly): base(game, poly.Bounds, RenderInstructions.Shape) {
			DATA_polygon = poly;
		}		
	}
	public class MonoGameSprite :Drawable { 
		Texture2D DATA_texture;
		public Texture2D Texture {
			get {
				return DATA_texture;
			}
		}
		public MonoGameSprite(GameManager game, Polygon2dBounds bounds, Texture2D texture): base(game, bounds, RenderInstructions.Sprite) {
			DATA_texture = texture;
		}		
	}
	public abstract class RenderInstruction :MonoGameComponent {
		public abstract string Name { get; }
	}

	public class Render_Shape :RenderInstruction {
		public override string Name {
			get { return "Mesh"; }
		}
	}
	public class Render_Lines :RenderInstruction { 
		public override string Name {
			get { return "Lines"; }
		}
	}
	public class Render_Text :RenderInstruction { 
		public override string Name {
			get { return "Text"; }
		}
	}
	public class Render_Sprite :RenderInstruction { 
		public override string Name {
			get { return "Sprite"; }
		}
	}
	public class Render_Rectangle :RenderInstruction { 
		public override string Name {
			get { return "Rectangle"; }
		}
	}
}

namespace MauronAlpha.MonoGame.Collections {
	using MauronAlpha;
	using MauronAlpha.HandlingData;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Geometry;

	public class RenderInstructions :List<RenderInstruction> {
		public static RenderInstructions Sprite {
			get {
				return new RenderInstructions() {
					new Render_Sprite()
				};
			}
		}
		public static RenderInstructions Text {
			get {
				return new RenderInstructions() {
					new Render_Text()
				};
			}
		}
		public static RenderInstructions Shape {
			get {
				return new RenderInstructions() {
					new Render_Shape()
				};
			}
		}
		public static RenderInstructions Lines {
			get { return new RenderInstructions() { new Render_Lines() }; }
		}
		public static RenderInstructions Default {
			get { return new RenderInstructions() { new Render_Rectangle() }; }
		}
	}
	public class RenderBuffer :MonoGameComponent  {
		Stack<MonoGameSprite> Sprites;
		Stack<MonoGameShape> Shapes;
		Stack<MonoGameLines> Lines;
		Stack<MonoGameText> Text;
	}

}
