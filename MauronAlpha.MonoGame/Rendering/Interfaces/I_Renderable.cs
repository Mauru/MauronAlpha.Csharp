namespace MauronAlpha.MonoGame.Rendering.Interfaces {
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;

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

		void SetRenderResult(I_RenderResult result);

		ShapeBuffer ShapeBuffer { get; }
		bool IsPolygon { get; }

		GameManager Game { get; }


	}

}