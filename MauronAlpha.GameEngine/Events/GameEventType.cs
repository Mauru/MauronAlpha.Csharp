namespace MauronAlpha.GameEngine.Events {

	public abstract class GameEventType : MauronCode_subtype {
		public abstract string Name { get; }
		public abstract GameEventShedule ApplyShedule (GameEvent e);
	}

}