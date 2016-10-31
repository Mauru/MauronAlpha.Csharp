namespace MauronAlpha.MonoGame.Rendering.Interfaces {
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework.Graphics;

	/// <summary> Interface for an object that can be preRendered </summary>
	public interface I_PreRenderable {

		RenderOrders RenderOrders { get; }

		Polygon2dBounds Bounds { get; }

		void SetRenderResult(Texture2D texture, long time);

		GameManager Game { get; }

		I_RenderResult RenderResult { get; }
	
	}
}