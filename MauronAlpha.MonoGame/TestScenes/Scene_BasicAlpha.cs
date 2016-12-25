namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.UI.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Utility;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;

	public class Scene_BasicAlpha:GameScene {

		public Scene_BasicAlpha(GameManager game) : base(game) { }

		public override GameRenderer.DrawMethod DrawMethod {
			get { return CompositeRenderer.RenderMethod; }
		}

		public override void Initialize() {
			GameRenderer renderer = Game.Renderer;

			PreRenderChain chain = new PreRenderChain(Game) {
				new PreRenderProcess(
					Game, "base", Rectangle2d.CreateAlignCenter(200, 200)
				),
				new PreRenderProcess(
					Game, "mask", Rectangle2d.CreateAlignCenter(180,180), new Vector2d(10,10)
				)
			};
			chain.PrepareShapeBuffers();

			RenderComposite composite = new RenderComposite(Game, chain, BlendModes.Solid);
			CompositeBuffer buffer = new CompositeBuffer() {
				composite
			};
			SetCompositeBuffer(buffer);

			renderer.SetCurrentScene(this);
			renderer.SetDrawMethod(CompositeRenderer.RenderMethod);

			base.Initialize();
		}

	}
}
