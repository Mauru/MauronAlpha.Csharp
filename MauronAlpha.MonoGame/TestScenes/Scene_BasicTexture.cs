namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.UI.DataObjects;

	using MauronAlpha.MonoGame.Rendering.Utility;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.MonoGame.Collections;

	using Microsoft.Xna.Framework;

	public class Scene_BasicTexture:GameScene {

		public Scene_BasicTexture(GameManager game) : base(game) { }

		public override void Initialize() {

			MonoGameTexture t=null;
			if (!Game.Assets.TryTexture("Default", "TestImage", ref t))
				throw new AssetError("Texture does not exist!", this);

			SpriteBuffer b = new SpriteBuffer();
			SpriteData d = new SpriteData(
				t,
				t.SizeAsRectangle
			);
			b.Add(d);
			SetSpriteBuffer(b);

			Game.Renderer.SetCurrentScene(this);
			Game.Renderer.SetDrawMethod(TextureRenderer.DrawMethod);

			base.Initialize();

		}

		public override GameRenderer.DrawMethod DrawMethod {
			get { return TextureRenderer.DrawMethod; }
		}

		public static string DebugTextures(GameManager game) {

			AssetManager assets = game.Assets;
			List<string> t = assets.ListOfTextureNames(true);

			string result = "";
			foreach (string s in t)
				result += t + " - ";
			return result;

		}
		public static string Serialize(List<string> d) {
			string result = "";
			int count = d.Count;
			foreach (string s in d) { 
				result += s;
				count--;
				if (count > 0)
					result += ",";
			}
			return result+";";
		}
	}
}
