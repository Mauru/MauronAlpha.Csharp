namespace MauronAlpha.GameEngine.Events {

	public interface I_GameEventListener {
		//Receive a single event
		void ReceiveEvent(GameEvent ge);
		
		//Receive an array of events
		void ReceiveEvents(GameEvent[] a_ge);
		
		//no idea what this was supposed to do
		void IsEventCondition(GameEvent ge);
	}

}