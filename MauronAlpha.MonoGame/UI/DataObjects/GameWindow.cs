namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.MonoGame.UI.Interfaces;
	using MauronAlpha.MonoGame.UI.Collections;
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame;
	using MauronAlpha.MonoGame.Rendering;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.TextProcessing.Units;

	public class GameWindow:UIElement {
		public override bool CanHaveChildren { get { return true; } }
		public override bool CanHaveParent { get { return false; } }

		public GameWindow(GameManager game) : base(game) {}

		public Vector2d WindowSize {
			get {
				return Game.Engine.GameWindow.SizeAsVector2d;
			}
		}
		public override Polygon2dBounds Bounds {
			get { return Game.Engine.GameWindow.Bounds; }
		}
	
		public override GameRenderer.RenderMethod RenderMethod
		{
			get { return Render; }
		}

		I_RenderResult Render(RenderStage stage, I_Renderable target, long time) {

			if(target.LastRendered == time)
				return target.RenderResult;

			TextureBatch batch = stage.Caller;
			batch.Begin();
			foreach(I_Renderable r in Children)
				batch.Draw(r.RenderResult,r);


			batch.End();

			RenderResult result = new RenderResult(time, this, stage.AsTexture2D);
			return result;

		}

		public override System.Type RenderPresetType
		{
			get { return typeof(GameWindow); }
		}
	}

}

