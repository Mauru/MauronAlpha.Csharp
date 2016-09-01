namespace MauronAlpha.MonoGame.Rendering {
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;
	
	public interface I_Renderable {

		Polygon2dBounds Bounds { get; }
		Vector2d Position { get; }

		Vector2d RenderTargetSize { get; }

		long LastRendered { get; }

		I_RenderResult RenderResult { get; }
		I_RenderResult Outline { get; }
		bool HasResult { get; }

		RenderOrders Orders { get; }
		void SetRenderResult(I_RenderResult result);

		GameRenderer.RenderMethod RenderMethod { get; }

		System.Type RenderPresetType { get; }

	}

}