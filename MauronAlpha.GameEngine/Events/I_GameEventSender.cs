namespace MauronAlpha.GameEngine.Events {

	public interface I_GameEventSender {
		
		// Send an event
		void SendEvent(GameEvent ge);
		
		// Return the Shedule on WHEN this item shoudl be checked for events
		GameEventShedule EventShedule { get; }

		//no idea what this was supposed to do
		bool CheckEvent (I_GameEventSender d);
		
		//helper object for game events?
		GameEventWatcher GameEventWatcher { get; }
	}



}
