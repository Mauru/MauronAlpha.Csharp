namespace MauronAlpha.MonoGame.DataObjects {
	using Microsoft.Xna.Framework.Graphics;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Collections;

	public class MonoGameTexture :Drawable { 
		Texture2D _texture;
		public Texture2D Texture {
			get {
				return _texture;
			}
		}
		public MonoGameTexture(GameManager game, Texture2D texture): base(game, BoundsOfTexture(texture), RenderInstructions.Texture) {
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
	public sealed class Render_Texture :RenderInstruction { 
		public override string Name {
			get { return "Texture"; }
		}

		static object _sync= new System.Object();
		static volatile Render_Texture _instance;

		public static Render_Texture Instance {
			get {
				if(_instance == null) {
					lock(_sync) {
						_instance = new Render_Texture();
					}
				}

				return _instance;
			}
		}

		Render_Texture() : base() { }
	}

}