namespace MauronAlpha.MonoGame.Interfaces {
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	/// <summary> Represents an item that can be rendered </summary>
	public interface I_Drawable {

		Polygon2dBounds Bounds { get; }

		List<Polygon2d> Shapes { get; }
		List<MonoGameTexture> Sprites { get; }
		MonoGameTexture Rendered { get; }

		bool NeedsRenderUpdate {	get; }

		void SetRenderResult(MonoGameTexture t, long renderTime);

		GameRenderer Renderer { get; }

	}
}