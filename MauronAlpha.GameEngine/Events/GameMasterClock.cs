using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.GameEngine.Events {
	public sealed class GameMasterClock : GameEventClock {
		#region Singleton
		private static volatile GameMasterClock instance=new GameMasterClock();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static GameMasterClock ( ) { }
		public static GameMasterClock Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new GameMasterClock();
					}
				}
				return instance;
			}
		}
		#endregion

		public override GameTick Sync (object syncobject) {
			throw new NotImplementedException();
		}
		public override void RegisterEvent (object client, GameEventShedule shedule) {
			throw new NotImplementedException();
		}

		public GameEventClock RenderClock;

		public GameEventClock EventClock;
	
		public GameEventClock SyncObject;
	}
}
