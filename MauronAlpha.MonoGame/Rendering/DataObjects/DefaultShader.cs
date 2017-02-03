namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework;

	public class DefaultShader : BasicEffect, I_Shader {

		GameManager _game;
		public GameManager Game { get { return _game; } }
		public new string Name { get { return "Default"; } }

		public DefaultShader(GameManager game): base(game.Engine.GraphicsDevice) {
			_game = game;
		}
		public System.Collections.Generic.IEnumerable<EffectPass> ShaderPasses {
			get {
				return base.CurrentTechnique.Passes;
			}
		}

		public void Apply() {
			base.CurrentTechnique.Passes[0].Apply();
		}

		public static DefaultShader Create2DTopLeft(GameManager game) {
			GraphicsDevice device = game.Renderer.GraphicsDevice;

			Vector2d scale = new Vector2d(
				2 / (double)device.Viewport.Width,
				2 / (double)device.Viewport.Height
			);

			Matrix rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(180));

			DefaultShader result = new DefaultShader(game);
			result.World = Matrix.CreateScale(scale.FloatX, -scale.FloatY, 1);
			result.View = Matrix.CreateTranslation(new Vector3(-1, 1, 0));
			result.VertexColorEnabled = true;

			RasterizerState state = new RasterizerState();
			state.CullMode = CullMode.None;
			device.RasterizerState = state;

			return result;
		}

	}

}