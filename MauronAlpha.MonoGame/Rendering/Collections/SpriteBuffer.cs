namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.Geometry.Geometry2d.Units;

	/// <summary> Holds render-information for sprites (textures)</summary>
	public class SpriteBuffer:List<SpriteData> {

		public static SpriteBuffer OffsetPosition(ref SpriteBuffer buffer, Vector2d v) {
			foreach (SpriteData data in buffer)
				data.SetPosition(data.Position.X + v.X, data.Position.Y + v.Y);

			return buffer;
		}

		/// <summary> Returns the width of SpriteBuffer the last element (or 0 if empty). Assumes Object starts @ offset.X</summary>
		public static double WidthByLastMemberAndOffset(SpriteBuffer buffer, Vector2d offset) {
			SpriteData member = null;
			if (!buffer.TryLastElement(ref member))
				return 0;

			double wordEnd = member.Position.X + member.Width;

			return wordEnd-offset.X;
		}
		public static double WidthOfLastMember(SpriteBuffer buffer) {
			SpriteData member = null;
			if (!buffer.TryLastElement(ref member))
				return 0;
			return member.Width;
		}
		public static SpriteBuffer Empty { get { return new SpriteBuffer(); } }

		public Polygon2dBounds GenerateBounds() {
			if (IsEmpty)
				return Polygon2dBounds.Empty;
		}
	}
}
