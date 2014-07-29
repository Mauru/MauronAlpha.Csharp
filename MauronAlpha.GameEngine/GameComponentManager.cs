using MauronAlpha.GameEngine.Events;

namespace MauronAlpha.GameEngine {

	//Basically a singleton that keeps a reference to other classes
	public abstract class GameComponentManager:GameComponent,I_GameEventSender,I_GameEventListener {


		internal virtual void InitializeEventWatchers ( ) {
			//we are setting a Default trigger-shedule 
			// (might want to get a default from a centralized class in the future)
			GES_eventShedule=new GameEventWatcher(this);
		}

		#region I_GameEventListener
		public abstract void ReceiveEvent (GameEvent ge);
		public virtual void ReceiveEvents (GameEvent[] a_ge) {
			foreach( GameEvent ge in a_ge ) {
				ReceiveEvent(ge);
			}
		}
		public abstract void IsEventCondition (GameEvent ge);
		#endregion
	
		#region I_GameEventSender
		public abstract void SendEvent (GameEvent ge);
		public abstract GameEventShedule EventShedule { get; }
		public abstract bool CheckEvent (I_GameEventSender d);
		internal GameEventWatcher GES_eventShedule;
		public virtual GameEventWatcher GameEventWatcher { get { return GES_eventShedule; } }
		#endregion
	}

}