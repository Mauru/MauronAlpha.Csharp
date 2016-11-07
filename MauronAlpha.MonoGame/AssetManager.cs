namespace MauronAlpha.MonoGame {
	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.FileSystem.Units;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.MonoGame.Assets;
	using MauronAlpha.MonoGame.Assets.DataObjects;

	/// <summary> Manages external content for the game </summary>///
	public class AssetManager :MonoGameComponent, I_CoreGameComponent, I_sender<ReadyEvent> {

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

		//constructor
		public AssetManager(GameManager o, FileStructure appDir): base() {
			DATA_Root = appDir;
			Set(o);
		}

		public void Initialize() {
			B_isInitialized = true;
		}

		//Boolean states
		bool B_isBusy = false;
		public bool IsBusy { get { return B_isBusy; } }

		bool B_isInitialized = false;
		public bool IsInitialized {
			get {
				return B_isInitialized;
			}
		}

		//Handling AssetGroups
		Registry<AssetGroup> DATA_AssetGroups = new Registry<AssetGroup>();
		AssetGroup FetchAssetGroup(string str) {
			AssetGroup g = null;
			if (DATA_AssetGroups.TryGet(str, ref g))
				return g;
			g = new AssetGroup(DATA_Manager, str);
			DATA_AssetGroups.SetValue(str, g);
			return g;
		}
		public AssetGroup InitializeAssetGroup(string name, Stack<LoadRequest> requests) {
			AssetGroup g = FetchAssetGroup(name);
			g.Add(requests);
			return g;
		}

		//AssetGroup-Events
		Subscriptions<ReadyEvent> S_Ready = new Subscriptions<ReadyEvent>();
		public virtual void Subscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Add(s);
		}
		public virtual void UnSubscribe(I_subscriber<ReadyEvent> s) {
			S_Ready.Remove(s);
		}

		//Generic paths
		Directory DIR_saveGames;
		public Directory SaveDirectory {
			get {
				if (DIR_saveGames == null)
					DIR_saveGames = DATA_Root.CreateDirectoryAndReturn("GameState");
				return DIR_saveGames;
			}
		}
		public Directory ContentDirectory {
			get {
				return DATA_Root.CreateDirectoryAndReturn("Content");
			}
		}
		public Directory LogDirectory {
			get {
				return ContentDirectory;
			}
		}

		//Fonts
		public bool HasDefaultFont {
			get {
				if(!IsInitialized)
					return false;
				AssetGroup g = FetchAssetGroup("Default");
				GameFont result = null;

				return g.TryFont("Default", ref result);
			}
		}
		public List<string> ListOfFontNames(bool includeGroupName) {
			List<string> result = new List<string>();
			foreach(AssetGroup g in DATA_AssetGroups)
				result.AddValuesFrom(g.ListOfFontNames(includeGroupName));

			return result;
		}
		public GameFont DefaultFont {
			get {
				AssetGroup g = FetchAssetGroup("Default");
				return g.Font("Default");
			}
		}
		public Directory FontDirectory {
			get {
				return ContentDirectory;
			}
		}

		//Textures
		public List<string> ListOfTextureNames(bool includeGroupName) {
			List<string> result = new List<string>();
			foreach (AssetGroup g in DATA_AssetGroups)
				result.AddValuesFrom(g.ListOfTextureNames(includeGroupName));

			return result;
		}
		public Directory TextureDirectory {
			get {
				return ContentDirectory;
			}
		}
		public bool TryTexture(string assetGroup, string name, ref MonoGameTexture result) {
			AssetGroup g = null;
			if (!DATA_AssetGroups.TryGet(assetGroup, ref g))
				return false;
			return g.TryTexture(name, ref result);
		}


		//Shaders
		public List<string> ListOfShaderNames(bool includeGroupName) {
			List<string> result = new List<string>();
			foreach (AssetGroup g in DATA_AssetGroups)
				result.AddValuesFrom(g.ListOfShaderNames(includeGroupName));

			return result;
		}
		public Directory ShaderDirectory {
			get {
				return ContentDirectory;
			}
		}
	
	}
}