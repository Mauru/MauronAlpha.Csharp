using System.Collections.Generic;

namespace MauronAlpha.GameEngine.Events {

	//A Gameeventwatcher is a class that controls a classes events
	public class GameEventWatcher : GameEventShedule {
		public delegate bool DELEGATE_condition(I_GameEventSender d);
		public delegate void DELEGATE_callback();

		//constructor
		public GameEventWatcher (I_GameEventSender source, I_GameEventListener target)
			: base(
				SheduleType_conditional.Instance,
				GameMasterClock.Instance.EventClock,
				GameMasterClock.Instance.EventClock,
				source,
				source.CheckEvent(source),
				target.ReceiveEvents(source.GameEventWatcher.Events)
			) {
			//Everything done in baseclass
		}
		public GameEventWatcher (I_GameEventSender source):this(source,source){}

		public void CheckEvent() {}

		private Stack<GameEvent> AGE_events = new Stack<GameEvent>();
		public GameEvent[] Events {
			get { return AGE_events.ToArray(); }
		}

	}



}
