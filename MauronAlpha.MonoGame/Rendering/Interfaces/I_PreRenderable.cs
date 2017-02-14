﻿namespace MauronAlpha.MonoGame.Rendering.Interfaces {
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework.Graphics;

	/// <summary> Interface for an object that can be preRendered </summary>
	public interface I_PreRenderable {

		Polygon2dBounds Bounds { get; }

		PreRenderOrders PreRenderOrders { get; }

		void SetRenderResult(I_RenderResult result);
		I_RenderResult RenderResult { get; }
		bool TryRenderResult(ref I_RenderResult result);
	
	}
}