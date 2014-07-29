namespace MauronAlpha.GameEngine {

	//Loads Assets
	public abstract class GameAssetLoader : GameComponent {
		protected GameAssetManager AssetManager;
		protected GameAsset Target;

		public abstract GameAssetLoader Instance { get; }
		public GameAsset Load (GameAsset asset, GameAssetManager assetmanager) {
			Debug("Loading ...",this);
			AssetManager=assetmanager;
			Target=asset;
			Target=performLoad();
			return Target;
		}
		public bool Loaded {
			get {
				return Target.Loaded;
			}
		}
		public virtual void Callback ( ) {
			Debug("Load Result",this);
			AssetManager.Callback(this);
		}
		public GameAsset Asset {
			get { return Target; }
		}
		protected abstract GameAsset performLoad ( );
	}

}