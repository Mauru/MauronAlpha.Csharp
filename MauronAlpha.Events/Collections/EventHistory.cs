using MauronAlpha.HandlingData;

using MauronAlpha.Events.Units;
using MauronAlpha.Events.Utility;

using System;


namespace MauronAlpha.Events.Collections {
	
	//A class keeping track of event firings etc
	public class EventHistory : MauronCode_eventComponent, IEquatable<EventUnit_timeStamp>	{

		//constructors
		private EventHistory () : base () {}
		public EventHistory (EventUnit_clock clock) : this () {
			CLOCK_events=clock;
			TREE_timeUnits.SetValue("created",clock.TimeStamp);
		}
		
		//The Clock
		private EventUnit_clock CLOCK_events;
		public EventUnit_clock Clock {
			get { 
				if(CLOCK_events==null){
					throw NullError("Clock can not be null!",this,typeof(EventUnit_clock));
				}
				return CLOCK_events; 
			}
		}

		//DataTrees
		private static string[] KEYS_default = new string[]{"created","updated","checked","executed"};
		private MauronCode_dataTree<string, EventUnit_timeStamp> TREE_timeUnits=new MauronCode_dataTree<string, EventUnit_timeStamp>(KEYS_default);

		//Created
		public EventUnit_timeStamp LastCreated {
			get {
				return TREE_timeUnits.Value("created");
			}
		}
		public EventHistory SetCreated (EventUnit_timeStamp time) {
			TREE_timeUnits.SetValue("created", time);
			return this;
		}

		//Checked
		public EventUnit_timeStamp LastChecked {
			get {
				//never:lastCreated
				if(!TREE_timeUnits.IsSet("checked")){ 
					return LastCreated;
				}

				return TREE_timeUnits.Value("checked");
			}
		}
		public EventHistory SetLastChecked (EventUnit_timeStamp time) {
			TREE_timeUnits.SetValue("checked", time);
			return this;
		}
		public bool WasChecked {
			get { return TREE_timeUnits.IsSet("checked"); }
		}

		//Updated
		public EventUnit_timeStamp TimeUpdated {
			get {
				//never: created
				if(!TREE_timeUnits.IsSet("updated")){
					return LastCreated;
				}
				return TREE_timeUnits.Value("updated");
			}
		}
		public EventHistory SetTimeUpdated (EventUnit_timeStamp time) {
			TREE_timeUnits.SetValue("updated",time);
			return this;
		}
		public bool WasUpdated {
			get { return TREE_timeUnits.IsSet("updated"); }
		}

		//Executed
		public EventUnit_timeStamp LastExecuted {
			get {
				//return never
				if( !TREE_timeUnits.IsSet("executed")) {
					return new EventUnit_timeStamp(Clock,new EventUnit_time(-1,Clock));
				}
				return TREE_timeUnits.Value("excuted");
			}
		}
		public EventHistory SetLastExecuted (EventUnit_timeStamp time) {
			TREE_timeUnits.SetValue("executed", time);
			return this;
		}
		public bool WasExecuted {
			get { return TREE_timeUnits.IsSet("executed"); }
		}

		//Execution count
		private int INT_executions=0;
		public int ExecutionCount {
			get {
				return INT_executions;
			}
		}
		public EventHistory SetExecutionCount (int n) {
			INT_executions=n;
			return this;
		}

		public bool Equals(EventUnit_timeStamp other, EventUtility_precision precisionHandler) {
			if( !EventUnit_clock.EQUALS(Clock, other.Clock, precisionHandler) )
				return false;
			return true;
		}
		public bool Equals(EventUnit_timeStamp other){
			EventUtility_precision handler = Clock.PrecisionHandler;
			return EventUnit_clock.EQUALS(Clock, other.Clock, Clock.PrecisionHandler);
		}

		#region IEquatable
		bool IEquatable<EventUnit_timeStamp>.Equals (EventUnit_timeStamp other) {
			return Equals(other, Clock.PrecisionHandler);
		}
		#endregion

	}

}