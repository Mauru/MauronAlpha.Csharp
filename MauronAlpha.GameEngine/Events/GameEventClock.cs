using System;
using System.Collections.Generic;

namespace MauronAlpha.GameEngine.Events {

	//A EventTimer
	public abstract class GameEventClock : GameComponent {

		//The current gametime
		internal GameTick T_turn=new GameTick(GameMasterClock.Instance.EventClock,GameMasterClock.Instance.SyncObject.Time);
		//Get the current GameTime
		public virtual GameTick Time { get { return T_turn; } }

		//Synchronize the time with an external Source
		public abstract GameTick Sync (object syncobject);

		//Advance the Clock by an amount of time
		public virtual GameTick Step ( ) {
			Time.Forward(StepSize);
			return Time;
		}
		public virtual GameTick Step (int steps) {
			Time.Forward(steps*StepSize);
			return Time;
		}

		//Set the size of a step
		private double D_stepsize=1;
		public virtual double StepSize { get { return D_stepsize; } }

		//Set the Timing to be asynchronus (i.e. set from an outsize source)
		private bool B_asynchronous=false;
		public virtual bool IsAsynch { get { return B_asynchronous; } }

		//Set a MasterClock
		private object O_masterclock;
		public virtual object MasterClock { get { return O_masterclock; } }

		//Register an Event to occur
		private Stack<GameEventShedule> GE_listeners=new Stack<GameEventShedule>();
		public abstract void RegisterEvent (object client, GameEventShedule shedule);

	}

}
