using System;
using System.Collections.Generic;

namespace MauronAlpha.GameEngine.Events {

	//An Event in the Game
	public abstract class GameEvent : MauronCode_dataset {

		//Source of the Event
		private object O_source;

		//Client of the Event
		private object O_client;

		private GameTick T_created;
		private GameTick T_sheduled;
		private GameTick T_executed;

		private bool B_hasExecuted=false;

		private GameEventShedule TS_shedule;
		private GameEventType ET_eventType;

		//Constructor : A gamevent without
		public GameEvent (GameTick time, object source) {
		}

	}

}

