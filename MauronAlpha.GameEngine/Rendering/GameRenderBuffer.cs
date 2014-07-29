using System;
using System.Collections.Generic;

using MauronAlpha.GameEngine.Events;

namespace MauronAlpha.GameEngine.Rendering {

	//DrawBuffer, a singleton
	public sealed class GameRenderBuffer : GameComponentManager, I_GameComponentManager {

#region singleton

		private static volatile GameRenderBuffer instance=new GameRenderBuffer();
		private static object syncRoot=new Object();

		//constructor singleton multithread safe
		static GameRenderBuffer ( ) { }
		public static GameRenderBuffer Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new GameRenderBuffer();
					}
				}
				return instance;
			}
		}

		#endregion
	
/* Actual Functions */

		public bool Busy=false;
		public Stack<GameRenderData> Buffer=new Stack<GameRenderData>();
		
		//Add an item to the buffer
		public void Add(I_Drawable d) {
			if(d.NeedsRendering && !d.RenderSheduled) {
				d.RenderData.RenderShedule.SetLastSheduled( GameMasterClock.Instance.RenderClock.Time );
				Buffer.Push(d.RenderData);
			}
		}

#region I_GameEventSender
		public override void SendEvent (GameEvent ge) {
			throw new NotImplementedException();
		}
		public override GameEventShedule EventShedule {
			get { throw new NotImplementedException(); }
		}
		public override bool CheckEvent (I_GameEventSender d) {
			throw new System.NotImplementedException();
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