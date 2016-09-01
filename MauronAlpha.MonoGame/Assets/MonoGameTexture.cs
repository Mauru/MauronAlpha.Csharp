namespace MauronAlpha.MonoGame.DataObjects {
	using Microsoft.Xna.Framework.Graphics;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Assets;
	using MauronAlpha.MonoGame.Collections;

	public class MonoGameTexture :MonoGameAsset { 
		Texture2D _texture;
		public Texture2D Texture {
			get {
				return _texture;
			}
		}
		public MonoGameTexture(GameManager game, Texture2D texture): base() {
			_texture = texture;
		}

		public static Polygon2dBounds BoundsOfTexture(Texture2D t) {
			return new Polygon2dBounds(t.Width, t.Height);
		}

		public string Name {
			get {
				return _texture.Name;
			}
		}
	}
}