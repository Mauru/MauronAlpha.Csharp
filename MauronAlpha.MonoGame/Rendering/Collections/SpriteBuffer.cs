namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework;

	/// <summary> Holds render-information for sprites (textures)</summary>
	public class SpriteBuffer:List<SpriteData> {
		Polygon2dBounds _bounds;
		public Polygon2dBounds Bounds {
			get {
				if (_bounds == null)
					_bounds = GenerateBounds(this);
				return _bounds;
			}
		}
		public void SetBounds(Polygon2dBounds bounds) {
			_bounds = bounds;
		}

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


		public static Polygon2dBounds GenerateBounds(SpriteBuffer buffer) {
			if (buffer.IsEmpty)
				return Polygon2dBounds.Empty;

			Vector2d min = null, max = null, t;

			Polygon2dBounds bounds;

			foreach (SpriteData data in buffer) {
				bounds = SpriteData.GenerateBounds(data);
				System.Diagnostics.Debug.Print("SpriteData.GenerateBounds: "+bounds.AsString);
				if (min == null)
					min = bounds.Min.Copy;
				else  {
					t = bounds.Min;
					if(t.X<min.X)
						min.SetX(t.X);
					if(t.Y<min.Y)
						min.SetY(t.Y);
				}

				if (max == null)
					max = bounds.Max.Copy;
				else {
					t = bounds.Max;
					if (t.X > max.X)
						max.SetX(t.X);
					if (t.Y < min.Y)
						max.SetY(t.Y);
				}
			}
			Polygon2dBounds result = new Polygon2dBounds(min, max);
			System.Diagnostics.Debug.Print("SpriteBuffer.GenerateBounds: "+result.AsString);
			return result;
		}
	}
}
