using MauronAlpha.HandlingData;
using MauronAlpha.MonoGame.Utility;

using Microsoft.Xna.Framework.Graphics;

namespace MauronAlpha.MonoGame.Resources {



	public class ResourceManager:MonoGameComponent {
		GameManager Manager;

		MauronCode_dataMap<string> Resources = new MauronCode_dataMap<string>();
		MauronCode_dataMap<GameFont> Fonts = new MauronCode_dataMap<GameFont>();
		MauronCode_dataMap<GameTexture> Textures = new MauronCode_dataMap<GameTexture>();
		MauronCode_dataStack<string> LoadQueue = new MauronCode_dataStack<string>();



		public ResourceManager(GameManager manager) : base() {
			Manager = manager;
		}

		public void SetDefaultFont(GameFont font) {
			Fonts.SetValue("default", font);
		}

		public GameFont Font(string name) {
			if(Fonts.ContainsKey(name))
				return Fonts.Value(name);
			return Fonts.Value("default");
		}

		public void RegisterFont(GameFont font) {
			if(Fonts.ContainsKey(font.ResourceCode))
				return;
			font.SetLoaded(false);
			Fonts.SetValue(font.ResourceCode,font);
			LoadQueue.Add(font.ResourceCode);
		}

		public void SetDefaultFont(string name) {
			Fonts.SetValue("default", Fonts.Value(name));
		}
	}
}
