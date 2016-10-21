namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Utility;

	using MauronAlpha.MonoGame.UI.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public class Scene_BasicShape:GameScene  {

		public Scene_BasicShape(GameManager game) : base(game) { }

		DefaultShader _shader;

		GraphicsDevice GraphicsDevice {
			get { return Game.Renderer.GraphicsDevice; }
		}
		public override void Initialize() {
			if (base.IsInitialized)
				return;
			Ngon2d shape = new Ngon2d(6,200);
			ShapeBuffer.Add(shape);
			ShapeBuffer.Triangulate(Game.Renderer, Color.White);

			Vector2d scale = new Vector2d(
				2 / (double)GraphicsDevice.Viewport.Width,
				2 / (double)GraphicsDevice.Viewport.Height
			);

			_shader = new DefaultShader(Game);
			_shader.World = Matrix.CreateScale(scale.FloatX, scale.FloatY, 1);
			_shader.View = Matrix.CreateTranslation(new Vector3(-1, -1, 0)); ;
			_shader.Projection = Matrix.Identity;
			_shader.VertexColorEnabled = true;

			Game.Renderer.SetCurrentShader(_shader);
			Game.Renderer.SetCurrentScene(this);
			Game.Renderer.SetDrawMethod(ShapeRenderer.RenderDirectlyToScreen);

			base.Initialize();
		}

		public override GameRenderer.DrawMethod DrawMethod {
			get { return ShapeRenderer.RenderDirectlyToScreen; }
		}
	
	}
}
