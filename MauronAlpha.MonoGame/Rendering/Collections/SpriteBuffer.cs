namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework;

	/// <summary> Holds render-information for sprites (textures).</summary>
	public class SpriteBuffer : List<SpriteDrawCall>, I_PreRenderableCollection {

		public SpriteBuffer() : base() { }
		public SpriteBuffer(MonoGameTexture texture, Polygon2dBounds bounds): base() {
			SpriteDrawCall data = new SpriteDrawCall(texture);
			Add(data);
			_bounds = bounds;
		}

		//Bounds
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
		public bool HasBounds {
			get {
				return _bounds == null;
			}
		}

		/// <summary> Moves each component in buffer by v (including its bounds). </summary>
		public static SpriteBuffer OffsetPosition(ref SpriteBuffer buffer, Vector2d v) {
			foreach (SpriteDrawCall data in buffer)
				data.SetPosition(data.Position.X + v.X, data.Position.Y + v.Y);
			if (buffer.HasBounds)
				buffer.Bounds.Offset(v);
			return buffer;
		}

		/// <summary> Returns the width of SpriteBuffer the last element (or 0 if empty). Assumes Object starts @ offset.X</summary>
		public static double WidthByLastMemberAndOffset(SpriteBuffer buffer, Vector2d offset) {
			SpriteDrawCall member = null;
			if (!buffer.TryLastElement(ref member))
				return 0;

			double wordEnd = member.Position.X + member.Width;

			return wordEnd-offset.X;
		}
		public static double WidthOfLastMember(SpriteBuffer buffer) {
			SpriteDrawCall member = null;
			if (!buffer.TryLastElement(ref member))
				return 0;
			return member.Width;
		}
		
		/// <summary> Generates the bounds of a SpriteBuffer. </summary>
		public static Polygon2dBounds GenerateBounds(SpriteBuffer buffer) {
			if (buffer.IsEmpty)
				return Polygon2dBounds.Empty;

			Vector2d min = null, max = null, t;

			Polygon2dBounds bounds;

			foreach (SpriteDrawCall data in buffer) {
				bounds = SpriteDrawCall.GenerateBounds(data);
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
			return result;
		}

		public static SpriteBuffer Empty { get { return new SpriteBuffer(); } }
	}
}
