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

	}

}