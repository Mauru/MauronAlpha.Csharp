namespace MauronAlpha.MonoGame {
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Shapes;

	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Collections;
	using MauronAlpha.Events.Interfaces;

	/// <summary> Manages the rendering of content </summary>///
	public class GameRenderer :MonoGameComponent, I_sender<ReadyEvent> {
		GameManager DATA_Manager;
		public GameManager Game {
			get { return DATA_Manager; }
		}
		public void Set(GameManager o) {
			if(DATA_Manager == null)
				DATA_Manager = o;
			DATA_Manager.Set(this);
		}

		public GameRenderer(GameManager o): base() {
			Set(o);
		}

		public void Initialize() {
			
		}

		RenderBuffer Buffer = new RenderBuffer();

		bool B_isBusy = false;
		public bool IsBusy {
			get { return B_isBusy; }
		}

		//Draw event
		public void Draw(long time) {
			B_isBusy = true;
			foreach(I_Drawable d in Buffer) {
				if(d.NeedsRenderUpdate)
					d.SetRenderResult(RenderToTexture(d.Shapes),time);
			}
		}

		public MonoGameSprite RenderToTexture(List<MonoGameShape> shapes) {
			List<MonoGameSprite> result = new List<MonoGameSprite>();
			foreach(MonoGameShape shape in shapes)
				result.Add(RenderToTexture(shape));

			GraphicsDevice device = Game.Engine.GraphicsDevice;
			List<Vector2> minMax = TotalSize(shapes);
			Vector2 max = minMax[1];
			RenderTarget2D scene = new RenderTarget2D(device, (int) max.X, (int) max.Y);
			
			SpriteBatch batch = new SpriteBatch(Game.Engine.GraphicsDevice);

			batch.Begin();
			foreach(MonoGameSprite t in result)
				batch.Draw(t.MonoGame, t.Bounds.MonoGame, Color.White);
			batch.End();

			return new MonoGameSprite(scene, new Shapes.Rectangle(minMax));
		}
		public MonoGameSprite RenderToTexture(MonoGameShape shape) {
			GraphicsDevice device = Game.Engine.GraphicsDevice;
			RenderTarget2D scene = new RenderTarget2D(device, (int) shape.Size.X, (int) shape.Size.Y);
			device.SetRenderTarget(scene);
			device.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

			Triangles tt = shape.Triangles;
			int vcount = tt.Count*3;
			VertexPositionColor[] vv = new VertexPositionColor[vcount];
			int offset = 0;
			foreach(Triangle t in tt) {
				Points pp = t.Points;
				foreach(Pt p in pp) {
					vv[offset] = new VertexPositionColor(p.AsVector3, Microsoft.Xna.Framework.Color.White);
					offset++;
				}
			}

			VertexBuffer buffer = new VertexBuffer(device, typeof(VertexPositionColor), vv.Length+1, BufferUsage.WriteOnly);
			buffer.SetData<VertexPositionColor>(vv);

			BasicEffect fx = new BasicEffect(device);
			fx.World = WorldMatrix();
			fx.View = ViewMatrix();
			fx.Projection = ProjectionMatrix();
			fx.VertexColorEnabled = true;

			device.SetVertexBuffer(buffer);

			RasterizerState rr = new RasterizerState();
			rr.CullMode = CullMode.None;
			device.RasterizerState = rr;

			foreach (EffectPass pass in fx.CurrentTechnique.Passes){
				pass.Apply();
				device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
			}

			return new MonoGameSprite(scene,shape.Bounds);			
		}

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

		List<Vector2> TotalSize(List<MonoGameShape> ss) {

			Vector2 min = new Vector2();
			Vector2 max = new Vector2();
			foreach(MonoGameShape s in ss) {
				MauronAlpha.MonoGame.Shapes.Rectangle b = s.Bounds;
				Vector2 mi = b.Min;
				Vector2 mx = b.Max;
				if(mi.X < min.X)
					min.X = mi.X;
				if(mi.Y < min.Y)
					min.Y = mi.Y;
				if(mx.X > max.X)
					max.X = mx.X;
				if(mx.Y > max.Y)
					max.Y = mx.Y;
			}
			return new List<Vector2>() { min, max };

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

namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.HandlingErrors;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Shapes;
	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.MonoGame.Interfaces;

	public class GameError:MauronCode_error {
		public GameError(string code, MonoGameComponent source):base(code,source,ErrorType_fatal.Instance) { }
	}
	
	public class ShapeSize :MonoGameComponent {

		public int Width;
		public int Height;

	}

	public class RenderTime :MonoGameComponent,System.IEquatable<RenderTime> {
		long DATA_ticks = 0;
		public long Ticks { get { return DATA_ticks; } }

		public RenderTime(long ticks): base() {
			DATA_ticks = ticks;
		}
		public bool Equals(RenderTime other) {
			return DATA_ticks.Equals(other.Ticks);
		}
	}
	public class RenderLevel :MonoGameComponent {
		public int Z;
		public RenderTime LastRendered;
		public RenderTime LastSheduled;
	}

	public abstract class MonoGameShape :MonoGameComponent, I_Drawable {
		public abstract Pt Size { get; }
		public abstract Triangles Triangles { get; }
		public abstract Rectangle Bounds { get; }
		public abstract List<Pt> Points { get; }

		GameManager DATA_manager;
		public GameManager Game {
			get {
				return DATA_manager;
			}
		}

		public MonoGameShape(GameManager manager): base() {
			DATA_manager = manager;
		}

		public List<MonoGameShape> Shapes {
			get { return new List<MonoGameShape>(){this}; }
		}
		public List<MonoGameSprite> Sprites {
			get { return new List<MonoGameSprite>(); }
		}

		MonoGameSprite DATA_rendered;
		public MonoGameSprite Rendered {
			get {
				if(DATA_rendered != null)
					return DATA_rendered;
				throw new GameError("No renderData!",this);
			}
		}

		public bool NeedsRenderUpdate {
			get { throw new System.NotImplementedException(); }
		}

		RenderTime DATA_lastRendered;
		public RenderTime LastRendered {
			get {
				if(DATA_lastRendered == null)
					return new RenderTime(0);
				return DATA_lastRendered;
			}
		}

		public void SetRenderResult(MonoGameSprite t, long renderTime) {
			DATA_rendered = t;
			DATA_lastRendered = new RenderTime(renderTime);
		}

		public GameRenderer Renderer {
			get { return Game.Renderer; }
		}
	}
	public class MonoGameTexture :MonoGameComponent {

		Texture2D DATA_texture;
		public MonoGameTexture() : base() { }
		public MonoGameTexture(Texture2D texture): base() {
			DATA_texture = texture;
		}
	}
	public class MonoGameSprite :MonoGameComponent { 
		Texture2D DATA_texture;
		Rectangle DATA_rectangle;
		public MonoGameSprite() : base() { }
		public MonoGameSprite(Texture2D texture, Rectangle bounds): base() {
			DATA_texture = texture;
			DATA_rectangle = bounds;
		}

		public Rectangle Bounds {
			get { return DATA_rectangle; }
		}
		public Texture2D MonoGame {
			get {
				return DATA_texture;
			}
		}

	}

}

namespace MauronAlpha.MonoGame.Interfaces {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	/// <summary> Represents an item that can be rendered </summary>
	public interface I_Drawable {

		public List<MonoGameShape> Shapes { get; }
		public List<MonoGameSprite> Sprites { get; }
		public MonoGameSprite Rendered { get; }

		public bool NeedsRenderUpdate {	get; }

		public void SetRenderResult(MonoGameSprite t, long renderTime);

		public GameRenderer Renderer { get; }

	}

}

namespace MauronAlpha.MonoGame.Collections {
	using MauronAlpha.HandlingData;
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Shapes;

	public class Points :List<Pt> {}
	public class Triangles :List<Triangle> {}

	public class List<T>:MauronCode_dataList<T> {}
	public class RenderBuffer :List<I_Drawable> { }
}

namespace MauronAlpha.MonoGame.Shapes {
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.MonoGame.Interfaces;

	public class TriangleShape :MonoGameShape {

		public TriangleShape(GameManager game, Vector2d position, float width, float height): base(game) {
			
		}
	}

	public class Triangle :MonoGameComponent {
		Points DATA_pt;
		public Points Points {
			get {
				return DATA_pt;
			}
		}
		public Triangle(Pt a, Pt b, Pt c) : base() {
			DATA_pt = new Points() { a, b, c };
		}
	}

	public class Rectangle :MonoGameComponent {
		List<Pt> DATA_pts;
		public Rectangle(Pt a, Pt b, Pt c, Pt d) : base() {
			DATA_pts = new List<Pt>() { a, b, c, d };
		}
		public Rectangle(float x, float y, float width, float height): base() {
			DATA_pts = new List<Pt>() {
				new Pt(x,y),
				new Pt(x+width,y),
				new Pt(x+width,y+height),
				new Pt(x,y+height)
			};
		}
		public Rectangle(List<Microsoft.Xna.Framework.Vector2> pts) {
			Pt a = new Pt(pts[0]);
			Pt d = new Pt(pts.LastElement);
			Pt b = new Pt(d.X, a.Y);
			Pt c = new Pt(a.X, d.Y);
			DATA_pts = new List<Pt>() { a, b, c, d };
		}
		public Rectangle(float width, float height)	: base() {
			DATA_pts = new List<Pt>() {
				new Pt(0,0),
				new Pt(width,0),
				new Pt(width,height),
				new Pt(0,height)
			};
		}
		public Microsoft.Xna.Framework.Rectangle MonoGame {
			get {
				return new Microsoft.Xna.Framework.Rectangle(
					(int) DATA_pts.Value(0).X,
					(int) DATA_pts.Value(0).Y,
					(int) DATA_pts.Value(3).X,
					(int) DATA_pts.Value(3).Y
				);
			}
		}
		public Microsoft.Xna.Framework.Vector2 Min {
			get {
				Pt min = DATA_pts[0];
				return new Microsoft.Xna.Framework.Vector2((float)min.X,(float)min.Y);
			}
		}
		public Microsoft.Xna.Framework.Vector2 Max {
			get {
				Pt min = DATA_pts[3];
				return new Microsoft.Xna.Framework.Vector2((float)min.X,(float)min.Y);
			}
		}
	}

	public class Pt :Vector2d {
		public Pt() : base() { }
		public Pt(int x, int y) : base(x, y) { }
		public Pt(float x, float y) : base(x, y) { }
		public Pt(double x, double y) : base(x, y) { }
		public Pt(Microsoft.Xna.Framework.Vector2 v) : base(v.X, v.Y) { }

		public Microsoft.Xna.Framework.Vector3 AsVector3 {
			get {
				return new Microsoft.Xna.Framework.Vector3((float) X, (float) Y, 0);
			}
		}
	}
}