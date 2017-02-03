﻿namespace MauronAlpha.MonoGame.Assets.DataObjects {

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

		string _name;
		public string Name {
			get {
				return _name;
			}
		}
		public void SetName(string name) {
			_name = name;
		}

		public bool IsEmpty {
			get {
				return _texture == null;
			}
		}

		//constructor
		public MonoGameTexture(GameManager game, Texture2D texture): base() {
			_texture = texture;
		}

		public double Width {
			get {
				if (_texture == null)
					return 0;
				return _texture.Width;
			}
		}
		public double Height {
			get {
				if (_texture == null)
					return 0;
				return _texture.Height;
			}
		}

		public Vector2d SizeAsVector2d {
			get {
				return new Vector2d(Width, Height);
			}
		}
		public Rectangle SizeAsRectangle { get {
			if (_texture == null)
				return new Rectangle();
			return _texture.Bounds;
		} }

		//static utility function
		public static Polygon2dBounds GenerateBounds(Texture2D t) {
			return new Polygon2dBounds(t.Width, t.Height);
		}
		public static Polygon2dBounds GenerateBounds(MonoGameTexture t) {
			if (t.IsEmpty)
				return Polygon2dBounds.Empty;
			Texture2D tt = t.AsTexture2d;
			return new Polygon2dBounds(0, 0, t.Width, t.Height);
		}
		public static Color[] GetColorData(MonoGameTexture t) {
			Color[] result = new Color[(int) (t.Width * t.Height) ];
			t.AsTexture2d.GetData<Color>(result);
			return result;
		}
	}

}