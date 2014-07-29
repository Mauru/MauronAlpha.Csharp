using System;
using System.Collections.Generic;

using MauronAlpha.GameEngine.Events;

namespace MauronAlpha.GameEngine {

	//Stores Assets in a singleton
	public sealed class GameAssetManager : GameComponentManager, I_GameComponentManager {

#region Singleton

		private static volatile GameAssetManager instance=new GameAssetManager();
		private static object syncRoot=new Object();
		
		//constructor singleton multithread safe
		static GameAssetManager ( ) { }
		public static GameAssetManager Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new GameAssetManager();
					}
				}
				return instance;
			}
		}

#endregion

		//the GameEngine
		public MauronAlphaGame GameEngine;
		public GameAssetLoader Loader;

		//Initialize
		public void Initialize (MauronAlphaGame gameengine, GameAssetLoader loader) {
			GameEngine=gameengine;
			Loader=loader;
		}

		//Loaded Assets are stored here		
		public Dictionary<string,GameAsset>Assets=new Dictionary<string,GameAsset>();

		//Queue related
		private Stack<GameAsset>LoadQueue=new Stack<GameAsset>();
		//the currently loading gameAsset
		private GameAsset LQ_active;
		//queue length
		public int QueueLength {get{ return LoadQueue.Count; }}
		//status
		private bool busy=false;
		public bool Busy { get { return busy; } }

		//start loading
		public void Load(GameAssetType assettype, string name) {
			GameAsset asset = new GameAsset(assettype);
			asset.Name=name;
			LoadQueue.Push(asset);
			if(!busy) {	cycle(); }
		}
		public void Load(Stack<GameAsset> assetlist) {
			Debug("Adding "+assetlist.Count+" Objects to Queue", this);
			foreach(GameAsset asset in assetlist.ToArray()){
				LoadQueue.Push(asset);
			}
			cycle();
		}
		
		//cycle the queue
		public void cycle(){
			
			//queue allready busy
			if(busy){ return; }
			
			//next item in queue
			if(LoadQueue.Count>0){
				busy=true;
				LQ_active=LoadQueue.Pop();
				Debug("Loading "+LQ_active.Name+":"+LQ_active.AssetType.Name,this);
				Loader.Load(LQ_active,this);
				return;
			}
			
			//queue complete
			GameEngine.Callback("LoadQueueComplete",this);
			busy=false;
		}
		
		//load callback
		public void Callback(GameAssetLoader loader){			
			//add the asset to the index
			Assets[loader.Asset.Name]=loader.Asset;
			
			//callback to the game
			GameEngine.Callback("LoadedAsset",loader.Asset);
			
			//prepare for next cycle
			Loader=Loader.Instance;
			busy=false;
			cycle();
		}

		#region I_GameEventSender
		public override void SendEvent (GameEvent ge) {
			throw new NotImplementedException();
		}
		public override GameEventShedule EventShedule {
			get { throw new NotImplementedException(); }
		}
		public override bool CheckEvent (I_GameEventSender d) {
			throw new NotImplementedException();
		}	
		#endregion

		#region I_GameEventListener
		public override void ReceiveEvent (GameEvent ge) {
			throw new NotImplementedException();
		}
		public override void IsEventCondition (GameEvent ge) {
			throw new NotImplementedException();
		}
		#endregion

	}

}