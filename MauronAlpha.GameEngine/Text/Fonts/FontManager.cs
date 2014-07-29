using System;

namespace MauronAlpha.GameEngine.Text.Fonts {
	public sealed class FontManager : GameComponentManager {
		#region singleton

		private static volatile FontManager instance=new FontManager();
		private static object syncRoot=new Object();

		//constructor singleton multithread safe
		static FontManager ( ) { }
		public static FontManager Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new FontManager();
					}
				}
				return instance;
			}
		}

	#endregion

		public override void ReceiveEvent (Events.GameEvent ge) {
			throw new NotImplementedException();
		}

		public override void IsEventCondition (Events.GameEvent ge) {
			throw new NotImplementedException();
		}

		public override void SendEvent (Events.GameEvent ge) {
			throw new NotImplementedException();
		}

		public override Events.GameEventShedule EventShedule {
			get { throw new NotImplementedException(); }
		}

		public override bool CheckEvent (Events.I_GameEventSender d) {
			throw new NotImplementedException();
		}
	}
}
