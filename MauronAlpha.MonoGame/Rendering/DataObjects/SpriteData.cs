namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Assets.Interfaces;
	
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.FontParser.DataObjects;

	using Microsoft.Xna.Framework;

	/// <summary> Drawcall for a sprite </summary>
	public class SpriteData:MonoGameComponent {

		//constructor
		public SpriteData(I_MonoGameTexture texture, Rectangle2d mask) : base() {
			_texture = texture;
			_mask = Rectangle2dToRectangle(mask);
		}
		public SpriteData(I_MonoGameTexture texture, Rectangle mask): base() {
			_mask = mask;
			_texture = texture;
		}
		public SpriteData(I_MonoGameTexture texture):base() {
			_texture = texture;
			_mask = texture.SizeAsRectangle;
		}

		I_MonoGameTexture _texture;
		public I_MonoGameTexture Texture {
			get { return _texture; }
		}

		public double Width {
			get {
				if (_texture == null)
					return 0;
				if (_mask != null)
					return _mask.Value.Width;

				return _texture.Width;
			}
		}
		public double Height {
			get {
				if (_texture == null)
					return 0;
				if (_mask != null)
					return _mask.Value.Height;

				return _texture.Height;
			}
		}

		Vector2d _position = new Vector2d();
		public Vector2d Position { get { return _position; } }
		public SpriteData SetPosition(Vector2d v) {
			_position = v;
			return this;
		}
		public SpriteData SetPosition(double x, double y) {
			_position = new Vector2d(x, y);
			return this;
		}
		
		System.Nullable<Rectangle> _posAsRect = null;
		public Rectangle PositionAsRectangle {
			get {
				if (_posAsRect == null) {
					Rectangle result = GeneratePositionRectangle(this);
					_posAsRect = result;
				}
				return _posAsRect.Value;
			}
		}
		public Rectangle GeneratePositionRectangle(SpriteData data) {
			Rectangle result = new Rectangle(data.Position.IntX, data.Position.IntY, (int)data.Width, (int)data.Height);
			return result;
		}

		System.Nullable<Rectangle> _mask;
		public Rectangle Mask {
			get {
				return _mask.Value;
			}
		}

		Color _color;
		public Color Color {
			get {
				if (_color == null)
					return Color.White;
				return _color;
			}
		}

		public static Rectangle Rectangle2dToRectangle(Rectangle2d r) {
			return new Rectangle(
				(int)r.X,
				(int)r.Y,
				(int)r.Width,
				(int)r.Height
			);
		}

	}

}