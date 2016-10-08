namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.UI.DataObjects;
	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Utility;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public class Scene_BasicText:GameScene {
		public override void RequestRender() {
			return;
		}

		public Scene_BasicText(GameManager game) : base(game) { }

		Camera _camera;
		public override Rendering.Camera Camera {
			get { return _camera; }
		}

		public override void Initialize() {

			AssetManager assets = Game.Assets;

			GameFont font = assets.DefaultFont;

		}

		public override GameRenderer.DrawMethod DrawMethod {
			get { return TextRenderer.DrawMethod; }
		}
	}
}
