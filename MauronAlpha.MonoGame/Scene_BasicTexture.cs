namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.UI.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Utility;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.MonoGame.Collections;

	public class Scene_BasicTexture:GameScene {

		public Scene_BasicTexture(GameManager game) : base(game) { }

		public override void Initialize() {
			
		}

		public override GameRenderer.DrawMethod DrawMethod {
			get { return TextureRenderer.DrawMethod; }
		}

		public static string DebugTextures(GameManager game) {

			AssetManager assets = game.Assets;
			List<string> assets.ListOfTextureNames(true);


		}
	}
}
