namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Utility;

	using MauronAlpha.MonoGame.UI.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

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
			Matrix2d matrix = Matrix2d.CreateTranslation(120, 120);
			ShapeBuffer buffer = new ShapeBuffer(matrix) {
				Rectangle2d.CreateAlignCenter(200,200)
			};
			SetShapeBuffer(buffer);

			Vector2d scale = new Vector2d(
				2 / (double)GraphicsDevice.Viewport.Width,
				2 / (double)GraphicsDevice.Viewport.Height
			);

			Matrix rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(180));

			_shader = new DefaultShader(Game);
			_shader.World = Matrix.CreateScale(scale.FloatX, -scale.FloatY, 1);
			_shader.View = Matrix.CreateTranslation(new Vector3(-1, 1, 0));
			_shader.VertexColorEnabled = true;

			RasterizerState state = new RasterizerState();
			state.CullMode = CullMode.None;
			Game.Renderer.GraphicsDevice.RasterizerState = state;

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
