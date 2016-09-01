namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.HandlingErrors;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Interfaces;
	
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	using MauronAlpha.MonoGame.Rendering;
	
		
	/// <summary> A Drawable element </summary>
	public class Drawable :MonoGameComponent, I_Drawable, I_Renderable {

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

		long DATA_frame = 0;
		public long AnimationFrame {
			get {
				return DATA_frame;
			}
		}

		Matrix2d DATA_matrix = new Matrix2d();
		Matrix2d Matrix { get { return DATA_matrix; } }

		public Vector2 PositionAsVector2 {
			get {
				Vector2d v = Matrix.Translation;
				return new Vector2(v.FloatX, v.FloatY);
			}
		}

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
		List<MonoGameTexture> DATA_sprites = new List<MonoGameTexture>();
		public List<MonoGameTexture> Sprites {
			get {
				return DATA_sprites;
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

		public Vector2d Position {
			get {
				return Matrix.Translation;
			}
		}

		// I_Renderable stuff
		I_RenderResult _outline;
		public I_RenderResult Outline {
			get {
				if(_outline == null)
					return RenderResult.Empty;
				return _outline;
			}
		}

		public RenderOrders Orders {
			get { return RenderOrders.Empty; }
		}

		I_RenderResult _result;
		public virtual void SetRenderResult(I_RenderResult result) {
			_result = result;
		}


		public virtual T TargetAs<T>() where T :I_Renderable {
			return default(T);
		}
	}

}