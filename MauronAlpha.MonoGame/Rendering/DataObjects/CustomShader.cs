namespace MauronAlpha.MonoGame.Rendering.DataObjects {
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


