namespace MauronAlpha.MonoGame {
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;

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
			Initialize_TestShapes();

			base.Initialize();
		}

		void Initialize_TestLines() {
			Ngon2d hex = new Ngon2d(6, 100);
			PreRenderable_Outline test = new PreRenderable_Outline(Game, hex, Color.White);
			GameRenderer renderer = Game.Renderer;
			renderer.SetCurrentScene(this);
			renderer.SetDrawMethod(PreRenderer.DrawMethod);
			renderer.SetPreRenderHandler(PreRenderHandler);
			renderer.Queue.Add(test);
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
			PreRenderable_TextFragment test = new PreRenderable_TextFragment(Game, Game.Assets.DefaultFont, "A Test of sorts", Color.White);

			GameRenderer renderer = Game.Renderer;
			renderer.SetCurrentScene(this);
			renderer.SetDrawMethod(PreRenderer.DrawMethod);
			renderer.SetPreRenderHandler(PreRenderHandler);
			renderer.Queue.Add(test);
		}

		void Initialize_TestShapes() {
			GameRenderer renderer = Game.Renderer;
			I_polygonShape2d hex = new Triangle2d(100);

			Vector2d size = renderer.WindowSize;
			GraphicsDevice device = renderer.GraphicsDevice;
			Vector2d scale = new Vector2d(
				2 / (double) device.Viewport.Width,
				2 / (double) device.Viewport.Height
			);

			_shader = new DefaultShader(Game);
			_shader.World = Matrix.CreateScale(scale.FloatX, -scale.FloatY, 1);
			_shader.View = Matrix.CreateTranslation(new Vector3(-1, 1, 0));
			_shader.VertexColorEnabled = true;

			RasterizerState state = new RasterizerState();
			state.CullMode = CullMode.None;
			Game.Renderer.GraphicsDevice.RasterizerState = state;

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