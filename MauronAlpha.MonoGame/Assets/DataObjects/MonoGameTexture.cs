namespace MauronAlpha.MonoGame.Assets.DataObjects {

	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Assets;
	using MauronAlpha.MonoGame.Assets.Interfaces;
	using MauronAlpha.MonoGame.Collections;

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	public class MonoGameTexture :MonoGameAsset, I_MonoGameTexture { 
		Texture2D _texture;
		public Texture2D AsTexture2d {
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

		public Rectangle SizeAsRectangle { get {
			if (_texture == null)
				return new Rectangle();
			return _texture.Bounds;
		} }

		string _name;
		public string Name {
			get {
				return _name;
			}
		}
		public void SetName(string name) {
			_name = name;
		}
	}

}