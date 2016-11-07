namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using Microsoft.Xna.Framework;

	public class PreRenderOrder:MonoGameComponent {

		public PreRenderOrder(ShapeBuffer buffer, I_Shader shader, Color color): base() {
			_shapes = buffer;
			_color = color;
			_type = RenderTypes.Shape;
			_shader = shader;
		}
		public PreRenderOrder(SpriteBuffer buffer, Color color):base() {
			_sprites = buffer;
			_color = color;
			_type = RenderTypes.Sprite;
		}
		public PreRenderOrder(MonoGameTexture t, Polygon2dBounds mask):base() {
			_sprites = new SpriteBuffer() {
				new SpriteData(t,mask)
			};
			_type = RenderTypes.Sprite;
		}

		string _type;
		public string RenderType { get { return _type; } }

		ShapeBuffer _shapes;
		public ShapeBuffer Shapes {
			get {
				return _shapes;
			}
		}

		LineBuffer _lines;
		public LineBuffer Lines { get { return _lines; } }

		SpriteBuffer _sprites;
		public SpriteBuffer Sprites { get { return _sprites; } }

		Color _color;
		public Color Color {
			get {
				return _color;
			}
		}

		BlendMode _blendMode;
		BlendMode BlendMode {
			get {
				return _blendMode;
			}
		}

		I_Shader _shader;
		public I_Shader Shader { get { return _shader; } }

	}

}