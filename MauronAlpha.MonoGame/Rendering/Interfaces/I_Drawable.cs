namespace MauronAlpha.MonoGame.Interfaces {
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.Rendering;

	/// <summary> Represents an item that can be rendered </summary>
	public interface I_Drawable {

		Polygon2dBounds Bounds { get; }

		List<Polygon2d> Shapes { get; }
		List<MonoGameTexture> Sprites { get; }
		MonoGameTexture Rendered { get; }

		bool NeedsRenderUpdate {	get; }

		I_RenderResult Result { get; }
		GameRenderer Renderer { get; }

	}
}