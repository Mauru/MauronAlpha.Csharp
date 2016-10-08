namespace MauronAlpha.MonoGame.Rendering {
	using Microsoft.Xna.Framework.Graphics;
	
	public class CustomShader:Effect, I_Shader {

		string _name;

		GameManager _game;
		public GameManager Game { get { return _game; } }

		public CustomShader(GameManager game, string name, byte[] code):base(game.Engine.GraphicsDevice, code) {
			_name = name;
		}

		public void SetUpCamera(GameRenderer device, MonoGame.Interfaces.I_GameScene scene) {


			return;

		}

		public System.Collections.Generic.IEnumerable<EffectPass> ShaderPasses {
			get {
				return base.CurrentTechnique.Passes;
			}
		}

		public void Apply() { }
	}

}

namespace MauronAlpha.MonoGame.Rendering {
	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.MonoGame.Interfaces;

	public class DefaultShader :BasicEffect, I_Shader {

		GameManager _game;
		public GameManager Game { get { return _game; } }
		public new string Name { get { return "Default"; } }

		public DefaultShader(GameManager game):base(game.Engine.GraphicsDevice) {
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
