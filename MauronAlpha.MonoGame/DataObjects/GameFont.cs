namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.FontParser;

	using MauronAlpha.FileSystem.Units;

	using MauronAlpha.MonoGame.Collections;

	/// <summary> A SpriteFont </summary>
	public class GameFont :MonoGameComponent {

		string STR_name;
		public string Name {
			get {
				return STR_name;
			}
		}

		GameManager DATA_game;

		public bool IsNull {
			get { return STR_name == null; }
		}

		bool B_isBusy = false;
		public bool IsBusy { get { return B_isBusy; } }

		bool B_hasLoaded = false;
		public bool HasLoaded { get { return B_hasLoaded; } }

		List<MonoGameSprite> Textures = new List<MonoGameSprite>();

		//constructor
		public GameFont(GameManager game): base() {
			DATA_game = game;
		}
		public GameFont(GameManager game, string name)	: base() {
			DATA_game = game;
			STR_name = name;
		}

		public void Load() {
			B_isBusy = true;
			Directory m = DATA_game.Content.BaseDirectory;
			File def = new File(m,STR_name,"fnt");
			FontDefinition font = new FontDefinition(def);
			font.Parse();

			foreach(File f in font.Files) {
				if(!f.Exists)
					throw new GameError("Could not find texture for font {"+f.Path+"}!",this);
				MonoGameSprite res = DATA_game.Content.LoadTextureFromFile(f);
				Textures.Add(res);
			}

			B_hasLoaded = true;
			B_isBusy = false;
		}
	
	}

}
