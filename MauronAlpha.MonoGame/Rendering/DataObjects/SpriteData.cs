namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Assets.Interfaces;

	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;
	
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.FontParser.DataObjects;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	/// <summary> Drawcall for a sprite </summary>
	public class SpriteData:MonoGameComponent, I_SpriteDrawInfo {

		//constructor
		public SpriteData(I_MonoGameTexture texture, Rectangle2d mask) : base() {
			_texture = texture;
			_mask = Rectangle2dToRectangle(mask);
		}
		public SpriteData(I_MonoGameTexture texture, Polygon2dBounds mask): base() {
			_texture = texture;
			_mask = Bounds2Rectangle(mask);
		}
		public SpriteData(I_MonoGameTexture texture, Rectangle mask): base() {
			_mask = mask;
			_texture = texture;
		}
		public SpriteData(I_MonoGameTexture texture):base() {
			_texture = texture;
			_mask = texture.SizeAsRectangle;
		}
		public SpriteData(I_MonoGameTexture texture, Vector2d position):base() {
			_texture = texture;
			_position = position;
			_mask = texture.SizeAsRectangle;
		}
		public SpriteData(I_MonoGameTexture texture, Vector2d position, Color color): base() {
			_texture = texture;
			_position = position;
			_color = color;
			_mask = texture.SizeAsRectangle;
		}

		public BlendMode BlendMode {
			get {
				return BlendModes.Alpha;
			}
		}

		I_MonoGameTexture _texture;
		public I_MonoGameTexture Texture {
			get { return _texture; }
		}
		public bool TryTexture(ref I_MonoGameTexture result) {
			if (_texture == null)
				return false;
			result = _texture;
			return true;
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

		Vector2d _position = Vector2d.Zero;
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

		System.Nullable<Rectangle> _mask;
		public Rectangle Mask {
			get {
				return _mask.Value;
			}
		}
		public bool HasMask {
			get {
				return _mask.HasValue;
			}
		}
		public bool TryMask(ref Rectangle result) {
			if (_mask == null)
				return false;
			result = _mask.Value;
			return true;
		}

		Color _color = Color.White;
		public Color Color {
			get {
				return _color;
			}
		}
		public void SetColor(Color color) {
			_color = color;
		}

		public static Rectangle Rectangle2dToRectangle(Rectangle2d r) {
			return new Rectangle(
				(int)r.X,
				(int)r.Y,
				(int)r.Width,
				(int)r.Height
			);
		}
		public static Rectangle Bounds2Rectangle(Polygon2dBounds b) {
			return new Rectangle((int) b.X, (int) b.Y, (int) b.Width, (int) b.Height);
		}
		public static Rectangle GeneratePositionRectangle(SpriteData data) {
			Rectangle result = new Rectangle(data.Position.IntX, data.Position.IntY, (int)data.Width, (int)data.Height);
			System.Diagnostics.Debug.Print("SpriteData.GeneratePositionRectangle: " + result.ToString());
			return result;
		}

		/// <summary> Generate Poly2dBounds from SpriteData </summary>
		public static Polygon2dBounds GenerateBounds(SpriteData data) {
			Rectangle tmp = new Rectangle();
			Vector2d p = data.Position;
			if (data.TryMask(ref tmp))
				return new Polygon2dBounds(p.X, p.Y, tmp.Width, tmp.Height);
			I_MonoGameTexture t = null; 
			if(data.TryTexture(ref t))
				return new Polygon2dBounds(p.X, p.Y, t.Width, t.Height);
			return Polygon2dBounds.Empty;
		}
		public static Polygon2dBounds Rectangle2Bounds(Rectangle r) {
			return new Polygon2dBounds(r.X, r.Y, r.Width, r.Height);
		}

	}

}