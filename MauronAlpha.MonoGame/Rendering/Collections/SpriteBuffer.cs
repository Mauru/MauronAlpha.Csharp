namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.Geometry.Geometry2d.Units;

	/// <summary> Holds render-information for sprites (textures)</summary>
	public class SpriteBuffer:List<SpriteData> {

		public SpriteBuffer OffsetPosition(Vector2d v) {
			foreach (SpriteData data in this)
				data.Position.Add(v);

			return this;
		}

	}
}
