namespace MauronAlpha.MonoGame.Rendering {
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Interfaces;

	using Microsoft.Xna.Framework;
	
	public interface I_Renderable {

		Polygon2dBounds Bounds { get; }
		Vector2d Position { get; }
		Vector2 PositionAsVector2 { get; }
		Vector2d SizeAsVector2d { get; }

		Vector2d RenderTargetSize { get; }

		long LastRendered { get; }

		I_RenderResult RenderResult { get; }
		I_RenderResult Outline { get; }
		bool HasRenderResult { get; }

		RenderOrders Orders { get; }
		void SetRenderResult(I_RenderResult result);

		GameManager Game { get; }

		GameRenderer.RenderMethod RenderMethod { get; }

		I_MonoShape AsMonoShape();

	}

}