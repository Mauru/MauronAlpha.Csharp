namespace MauronAlpha.MonoGame {
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.FileSystem.Units;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.Collections;

	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.Geometry.Geometry2d.Shapes;

	/// <summary> Manages external content for the game </summary>///
	public class GameContentManager :MonoGameComponent, I_sender<ReadyEvent> {

		GameManager DATA_Manager;
		public GameManager Game {
			get { return DATA_Manager; }
		}
		public void Set(GameManager o) {
			if(DATA_Manager == null)
				DATA_Manager = o;
			DATA_Manager.Set(this);
		}

		FileStructure DATA_Root;

		public GameContentManager(GameManager o, FileStructure appDir): base() {
			DATA_Root = appDir;
			Set(o);
			B_isInitialized = true;
		}

		bool B_isBusy = false;
		public bool IsBusy { get { return B_isBusy; } }

		bool B_isInitialized = false;
		public bool IsInitialized {
			get {
				return B_isInitialized;
			}
		}

		//paths
		Directory DIR_saveGames;
		public Directory SaveDirectory {
			get {
				if(DIR_saveGames == null)
					DIR_saveGames = DATA_Root.CreateDirectoryAndReturn("GameState");
				return DIR_saveGames;
			}
		}
		public Directory BaseDirectory {
			get {
				return DATA_Root.CreateDirectoryAndReturn("Content");
			}
		}

		GameFont DATA_defaultFont;
		public GameFont DefaultFont {
			get {
				return DATA_defaultFont;
			}
		}
		public void SetDefaultFont(GameFont font) {
			DATA_defaultFont = font;
		}

		//Specific resources
		public Registry<MonoGameSprite> DATA_textures = new Registry<MonoGameSprite>();
		public Registry<GameFont> DATA_fonts = new Registry<GameFont>();

		public GameFont LoadGameFont(string str) {
			GameFont result = null;
			if(DATA_fonts.TryGet(str, ref result))
				return result;

			result = new GameFont(DATA_Manager, str);
			DATA_fonts.SetValue(str, result);
			result.Load();
			return result;
		}
		public MonoGameSprite LoadTextureFromFile(File  f) {
			MonoGameSprite result = null;
			if(DATA_textures.TryGet(f.Name, ref result))
				return result;
			Texture2D t = Game.Engine.Content.Load<Texture2D>(f.Name);
			Rectangle2d bounds = new Rectangle2d(0,0,t.Width, t.Height);
			result = new MonoGameSprite(DATA_Manager, bounds.Bounds, t);
			DATA_textures.SetValue(f.Name, result);
			return result;			
		}

		//Events
		Subscriptions<ReadyEvent> S_Ready = new Subscriptions<ReadyEvent>();
		public virtual void Subscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Add(s);
		}
		public virtual void UnSubscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Remove(s);
		}
	
	}

}