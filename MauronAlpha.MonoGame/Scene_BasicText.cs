namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.UI.DataObjects;
	
	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Utility;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.MonoGame.Collections;

	public class Scene_BasicText:GameScene {
		public Scene_BasicText(GameManager game) : base(game) { }

		public override void Initialize() {

			AssetManager assets = Game.Assets;
			GameFont font = assets.DefaultFont;

			TextDisplay text = new TextDisplay(Game,"This is a test.",font);

			SpriteBuffer _sprites = text.SpriteBuffer;
			base.SetSpriteBuffer(_sprites);

			Game.Renderer.SetCurrentScene(this);

			base.Initialize();
		}

		public override GameRenderer.DrawMethod DrawMethod {
			get { return TextRenderer.DrawMethod; }
		}
	}
}
