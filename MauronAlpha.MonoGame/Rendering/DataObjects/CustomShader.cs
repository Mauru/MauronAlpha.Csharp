namespace MauronAlpha.MonoGame.Rendering {
	using Microsoft.Xna.Framework.Graphics;
	
	public class CustomShader:Effect, I_Shader {

		string _name;
		public string Name { get {
			return _name;
		} }

		GameManager _game;
		public GameManager Game { get { return _game; } }

		public CustomShader(GameManager game, string name, byte[] code):base(game.Engine.GraphicsDevice, code) {
			_name = name;
		}

	}

}

namespace MauronAlpha.MonoGame.Rendering {
	using Microsoft.Xna.Framework.Graphics;

	public class DefaultShader :BasicEffect, I_Shader {

		GameManager _game;
		public GameManager Game { get { return _game; } }
		public new string Name { get { return "Default"; } }

		public DefaultShader(GameManager game):base(game.Engine.GraphicsDevice) {
			_game = game;
		}

	}

}
