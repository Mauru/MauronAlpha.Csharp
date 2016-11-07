namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Assets;
	
	/// <summary> Serves as a basehub for the minimum of required static information (How a game runs) </summary>
	public class GameSetup :MonoGameComponent {

		GameManager _game;
		public GameManager Game { get { return _game; } }

		public GameSetup(GameManager game) : base() {
			_game = game;
		}
		
		public Stack<LoadRequest> PrepareRequiredAssets(AssetManager assets) {
			Stack<LoadRequest> result = new Stack<LoadRequest>();
			
			result.Add(new LoadRequest("Default", "Default.fnt", AssetTypes.Font));
			//result.Add(new LoadRequest("PixelShader", AssetTypes.Shader));
			result.Add(new LoadRequest("TestImage", "TestImage.png", AssetTypes.Texture));
			result.Add(new LoadRequest("4px", "4px.png", AssetTypes.Texture));
			return result;		
		}

	}

}