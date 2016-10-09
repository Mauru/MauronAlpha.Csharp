namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Assets.Interfaces;
	
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

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

		I_MonoGameTexture _texture;
		public I_MonoGameTexture Texture {
			get { return _texture; }
		}

		Vector2d _position = Vector2d.Zero;
		public Vector2d Position { get { return _position; } }
		public SpriteData SetPosition(Vector2d v) {
			_position = v;
			return this;
		}

		Rectangle _mask;
		public Rectangle Mask {
			get {
				return _mask;
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

		Color _color;
		public Color Color { get {
			if (_color == null)
				return Color.White;
			return _color;		
		} }
	}

}