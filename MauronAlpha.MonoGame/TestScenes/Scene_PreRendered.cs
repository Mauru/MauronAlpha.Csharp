namespace MauronAlpha.MonoGame {
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.UI.DataObjects;
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Utility;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	
	public class Scene_PreRendered:GameScene {

		DefaultShader _shader;

		public Scene_PreRendered(GameManager game) : base(game) { }

		public override void Initialize() {
			Initialize_TestTexture();

			base.Initialize();
		}

		void Initialize_TestTexture() {
			PreRenderable_Texture test = new PreRenderable_Texture(Game, "Default", "TestImage");
			GameRenderer renderer = Game.Renderer;
			renderer.SetCurrentScene(this);
			renderer.SetDrawMethod(PreRenderer.DrawMethod);
			renderer.SetPreRenderHandler(PreRenderHandler);
			renderer.Queue.Add(test);
		}

		void Initialize_TestText() {
			PreRenderable_TextFragment test = new PreRenderable_TextFragment(Game, Game.Assets.DefaultFont, "Some Text.", Color.White);

			GameRenderer renderer = Game.Renderer;
			renderer.SetCurrentScene(this);
			renderer.SetDrawMethod(PreRenderer.DrawMethod);
			renderer.SetPreRenderHandler(PreRenderHandler);
			renderer.Queue.Add(test);
		}

		void Initialize_TestShape() {
			GameRenderer renderer = Game.Renderer;
			Ngon2d hex = new Ngon2d(6, 100);


			Vector2d size = renderer.WindowSize;
			Vector2d scale = size.Divide(2);

			_shader = new DefaultShader(Game);
			_shader.World = Matrix.CreateScale(scale.FloatX, scale.FloatY, 1);
			_shader.View = Matrix.CreateTranslation(new Vector3(-1, -1, 0)); ;
			_shader.Projection = Matrix.Identity;
			_shader.VertexColorEnabled = true;

			renderer.SetCurrentScene(this);
			renderer.SetDrawMethod(PreRenderer.DrawMethod);
			renderer.SetCurrentShader(_shader);
			renderer.SetPreRenderHandler(PreRenderHandler);

			PreRenderable_Shape shape = new PreRenderable_Shape(Game, hex, _shader, Color.Green);
			renderer.Queue.Add(shape);
		}

		public GameRenderer.PreRenderEventHandler PreRenderHandler {
			get {
				return OnPreRenderEvent;
			}
		}
		public static void OnPreRenderEvent(GameRenderer renderer, SpriteBuffer buffer, long time) {
			I_GameScene scene = renderer.CurrentScene;
			scene.SetSpriteBuffer(buffer);
			renderer.SetDrawMethod(TextureRenderer.DrawMethod);
		}

		public override GameRenderer.DrawMethod DrawMethod {
			get {
				return PreRenderer.DrawMethod;
			}
		}
	}
}

namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;

	using MauronAlpha.MonoGame.Rendering.Collections;


	using Microsoft.Xna.Framework;

	public class PreRenderable_Shape : PreRenderable {

		I_polygonShape2d _shape;

		public PreRenderable_Shape(GameManager game, I_polygonShape2d shape, I_Shader shader, Color color) : base(game) {
			_shape = shape;
			ShapeBuffer buffer = new ShapeBuffer() {
				TriangulationData.CreateFromShape(shape,TriangulationData.WhiteVertexColors)
			};
			_orders = new RenderOrders() {
				new PreRenderOrder(buffer,shader,color),
			};
		}

		RenderOrders _orders;
		public override RenderOrders RenderOrders {
			get {
				return _orders;
			}
		}

		public override Polygon2dBounds Bounds {
			get { return _shape.Bounds; }
		}

	}
}

namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.MonoGame.UI.DataObjects;

	using Microsoft.Xna.Framework;

	public class PreRenderable_TextFragment : PreRenderable {

		TextFragment _text;

		//constructor
		public PreRenderable_TextFragment(GameManager game, GameFont font, string text, Color color) : base(game) {
			_text = new TextFragment(game, font, text);

			SpriteBuffer buffer = TextFragment.GenerateSpriteBuffer(_text);
			_bounds = buffer.GenerateBounds();

			_orders = new RenderOrders() {
				new PreRenderOrder(buffer,color)
			};
		}


		RenderOrders _orders;
		public override RenderOrders RenderOrders {
			get {
				return _orders;
			}
		}

		Polygon2dBounds _bounds;
		public override Polygon2dBounds Bounds {
			get { return _bounds; }
		}
	
	}
}

namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public class PreRenderable_Texture : PreRenderable {

		MonoGameTexture _texture;

		public PreRenderable_Texture(GameManager game, string assetGroup, string name) : base(game) {
			AssetManager assets = game.Assets;

			MonoGameTexture _texture = null;

			if (!assets.TryTexture(assetGroup, name, ref _texture))
				throw new AssetError("Unnown texture {" + assetGroup + "," + name + "}!",this);

			_bounds = MonoGameTexture.GenerateBounds(_texture);

			_orders = new RenderOrders() {
				new PreRenderOrder(_texture,_bounds)
			};
		}

		RenderOrders _orders;
		public override RenderOrders RenderOrders {
			get {
				return _orders;
			}
		}

		Polygon2dBounds _bounds;
		public override Polygon2dBounds Bounds {
			get { return _bounds; }
		}
	}
}