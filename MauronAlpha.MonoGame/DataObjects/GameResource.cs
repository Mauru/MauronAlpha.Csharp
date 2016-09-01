namespace MauronAlpha.MonoGame.DataObjects {
	using Microsoft.Xna.Framework.Graphics;
	public class GameResource:MonoGameComponent {

		
		public GameResource(string name) : base() {
			STR_name = name;
		}
		public GameResource(string name, SpriteFont f): this(name) {
			font = f;
			B_isEmpty = false;
		}
		public GameResource(string name, Texture2D t): this(name) {
			texture = t;
		}

		string STR_name;
		public string Name { get { return STR_name; } }

		SpriteFont font = null;
		public SpriteFont Font { get { return font; } }

		Texture2D texture = null;
		public Texture2D Texture { get { return texture; } }
		public Microsoft.Xna.Framework.Rectangle SizeRectangle {
			get {
				if(!IsTexture)
					return new Microsoft.Xna.Framework.Rectangle();
				return new Microsoft.Xna.Framework.Rectangle(0, 0, texture.Width, texture.Height);
			}
		}


		public bool IsFont {
			get { return font != null; }
		}
		public bool IsTexture {
			get {
				return texture != null;
			}
		}

		bool B_isEmpty = true;
		public bool IsEmpty {
			get { return B_isEmpty; }
		}

		public void CleanUp() {
			texture = null;
			font = null;
		}
	}
}
