using MauronAlpha.ProjectTypes;
using MauronAlpha.Geometry;
using MauronAlpha.GameEngine.Events;
using MauronAlpha.GameEngine.SaveGames;
using MauronAlpha.GameEngine.Rendering.Textures;

namespace MauronAlpha.GameEngine {

	//Base class for a game
	public abstract class MauronAlphaGame : MauronAlpha.ProjectTypes.Program, I_GameEventListener, I_GameEventSender {
		//The GameType 
		public GameType GameType;
		//The Game Logic
		public GameLogic GameLogic;

		//constructor
		public MauronAlphaGame(GameType gametype,GameLogic GameLogic):base(GameLogic.Name){
			GameType=gametype;
		}

		//Shortcuts
		public virtual GameAssetManager AssetManager { 
			get{
				return GameAssetManager.Instance;
			}
		}
		public virtual GameTextureManager TextureManager {
			get {
				return GameTextureManager.Instance;
			}
		}
        
		//Start the Game
		public void Start(GameInstance game,Device device,OS os) {
            this.Self = game;
            base.Start(this.Self, device, os);
			GameLogic=(GameLogic) os.backend;
        }

		public abstract void Save(SaveGameData savegamedata);
		public abstract void DeleteSaveGame (SaveGame savegame);
		public abstract void Load(SaveGame savegame);
		public abstract void End ( );


		public virtual void LoadAssets(GameAssetList assets) {
			AssetManager.Load(assets.List);
		}
		public abstract void Callback(string message, object source);

		public abstract GameComponent Sound {get;set;}
		public abstract GameComponent Graphics {get;set;}

		public abstract object WindowSize {get;set;}

#region I_GameEventListener
		public void ReceiveEvent (GameEvent ge) {
			throw new System.NotImplementedException();
		}
		public void ReceiveEvents (GameEvent[] a_ge){
			foreach(GameEvent ge in a_ge){
				ReceiveEvent(ge);
			}
		}

		public void IsEventCondition (GameEvent ge) {
			throw new System.NotImplementedException();
		}
#endregion

#region I_GameEventSender 
		public GameEventWatcher GameEventWatcher {
			get { throw new System.NotImplementedException(); }
		}
		public void SendEvent (GameEvent ge) {
			throw new System.NotImplementedException();
		}
		public GameEventShedule EventShedule {
			get { throw new System.NotImplementedException(); }
		}
		public bool CheckEvent (I_GameEventSender d) {
			throw new System.NotImplementedException();
		}
#endregion







	}

}