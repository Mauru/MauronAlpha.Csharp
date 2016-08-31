namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Assets;
	
	/// <summary> Serves as a basehub for the minimum of required static information (How a game runs) </summary>
	public class GameSetup :MonoGameComponent {

		public GameSetup() : base() {}
		
		public Stack<LoadRequest> PrepareRequiredAssets(AssetManager assets) {
			Stack<LoadRequest> result = new Stack<LoadRequest>();
			
			LoadRequest request = new LoadRequest("Default",AssetTypes.Font);
			result.Add(request);
			return result;		
		}

	}

}