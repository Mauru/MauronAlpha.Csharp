namespace MauronAlpha.MonoGame.TestScenes {

	using MauronAlpha.MonoGame.UI.DataObjects;

	using MauronAlpha.MonoGame.Rendering.Utility;

	public class Scene_2dMatrixTests : GameScene {

		public Scene_2dMatrixTests(GameManager game) : base(game) { }

		public override void Initialize() {

			base.Initialize();
		}

		public override GameRenderer.DrawMethod DrawMethod {
			get { return ShapeRenderer.RenderDirectlyToScreen; }
		}
	}
}