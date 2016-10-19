namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.UI.DataObjects;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Utility;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;
	using MauronAlpha.TextProcessing.DataObjects;

	using MauronAlpha.MonoGame.Geometry.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Collections;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public class Scene_Mixed : GameScene {
		public Scene_Mixed(GameManager game) : base(game) { }

		RenderLayer Layer;

		GraphicsDevice GraphicsDevice {
			get {
				return Game.Engine.GraphicsDevice;
			}
		}
		public override void Initialize() {

			//Sample shape
//			Polygon2d hex = new Rectangle2d(50, Game.Engine.GraphicsDevice.Viewport.Height*2-50, 200, -200);
			Polygon2d hex = new Rectangle2d(0, 0, 200, -200);
			Segment2dList segments = hex.Segments;
			SetLineBuffer(new LineBuffer() { new MonoGameLine(1, 1, 301, -299, 1) });

			//Segment2dList segments = hex.Segments;
			ShapeBuffer.Add(hex);
			ShapeBuffer.Triangulate(Game.Renderer, Color.Red);



			//shader
			Vector2d size = new Vector2d(
				1 / (double) GraphicsDevice.Viewport.Width,
				1 / (double) GraphicsDevice.Viewport.Height
			);

			DefaultShader shader = new DefaultShader(Game);
			shader.World = Matrix.CreateScale(size.FloatX, size.FloatY, 1);
			shader.View = Matrix.CreateTranslation(new Vector3(-1,1, 0)); ;
			shader.Projection = Matrix.Identity;
			shader.VertexColorEnabled = true;
			Game.Renderer.SetCurrentShader(shader);


			//SpriteBuffer
			AssetManager assets = Game.Assets;
			GameFont font = assets.DefaultFont;
			Text txt = new Text("Some Sample-Text");

			TextFragment text = new TextFragment(Game, txt, font);

			SpriteBuffer sprites = text.SpriteBuffer;
			base.SetSpriteBuffer(sprites);


			Game.Renderer.SetCurrentScene(this);
			Game.Renderer.SetDrawMethod(MixedRenderer.DrawMethod);


			 


			/*
			AssetManager assets = Game.Assets;
			GameFont font = assets.DefaultFont;
			Text txt = new Text("GameCycle : 0");
			
			TextDisplay text = new TextDisplay(Game,txt,font);

			SpriteBuffer _sprites = text.SpriteBuffer;
			base.SetSpriteBuffer(_sprites);
			_text = text;
			Game.Renderer.SetCurrentScene(this);
			Game.Renderer.SetDrawMethod(TextRenderer.DrawMethod);
			*/

			base.Initialize();
		}

		bool _isBusy = false;
		long _time = 0;
		public bool IsBusy { get { return _isBusy; } }

		long _lastCycle = -10;
		long _sleepTime = 100000;
		public override void RunLogicCycle(long time) {
			return;
			long current = _time;

			if (_lastCycle + _sleepTime < current) {
				_time++;
				return;
			}

			if (IsBusy) {
				_time++;
				return;
			}

		}

		public override GameRenderer.DrawMethod DrawMethod {
			get { return MixedRenderer.DrawMethod; }
		}
	}

	public class RenderLayer : MonoGameComponent {

		public RenderLayer() : base() { }
		public RenderLayer(ShapeBuffer shapes): this() {
			_shapes = shapes;
		}
		public RenderLayer(SpriteBuffer sprites): this() {
			_sprites = sprites;
		}
		public RenderLayer(ShapeBuffer shapes, SpriteBuffer sprites): this() {
			_shapes = shapes;
			_sprites = sprites;
		}
		public RenderLayer(ShapeBuffer shapes, SpriteBuffer sprites, I_RenderResult result): this() {
			_shapes = shapes;
			_sprites = sprites;
			_result = result;
		}

		SpriteBuffer _sprites = new SpriteBuffer();
		public SpriteBuffer Sprites { get { return _sprites; } }
		public RenderLayer Add(SpriteBuffer buffer) {
			_sprites.AddValuesFrom(buffer);
			return this;
		}

		ShapeBuffer _shapes = new ShapeBuffer();
		public ShapeBuffer Shapes { get { return _shapes; } }
		public RenderLayer Add(ShapeBuffer buffer) {
			_shapes.AddValuesFrom(buffer);
			return this;
		}

		I_RenderResult _result = RenderResult.Empty;
		public void SetRenderResult(I_RenderResult result) {
			_result = result;
		}
		
	}
}
