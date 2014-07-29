using System;

namespace MauronAlpha.GameEngine.Events {

	//A shedule for an event
	public abstract class GameEventShedule : GameComponent {
		private GameTick T_created; //when created
		public GameTick Created { get{ return T_created; } }

		private GameTick T_sheduled; //when sheduled
		public GameTick Sheduled { get { return T_sheduled; } }

		private GameTick T_executed; //when last executed
		public GameTick Executed { get { return T_executed;} }

		//The Class calling cycles
		private GameEventClock GEC_clock; 
		public virtual GameEventClock Clock { get { return GEC_clock; }}

		//The Class That created this event
		private I_GameEventSender GES_source;//The item sending the event
		public virtual I_GameEventSender Source { get { return GES_source; } }

		//The Class to receive the callback
		private I_GameEventListener GEL_target;//The item sending the event
		public virtual I_GameEventListener Target { get { return GEL_target; } }

		//A condition to check
		private Delegate D_condition;
		
		//A function to call on the target
		private Delegate D_callback;

		//Information about the shedule-capabilities, an interface of sorts
		private GameEventSheduleType ST_sheduleType;
		public GameEventSheduleType SheduleType { get { return ST_sheduleType; } }

		//The super-scary constructor
		public GameEventShedule (
			GameEventSheduleType sheduletype,
			GameEventClock clock,
			I_GameEventSender source,
			I_GameEventListener target,
			Delegate condition,
			Delegate callback
		) {
			ST_sheduleType=sheduletype;
			GEC_clock=clock;
			GES_source=source;
			GEL_target=target;
			D_condition=condition;
			D_callback=callback;
		}

	}

}
