using MauronAlpha.HandlingData;

using MauronAlpha.Events.Units;
using MauronAlpha.Events.Utility;

using System;


namespace MauronAlpha.Events.Collections {
	
	//A class keeping track of event firings etc
	public class EventHistory : MauronCode_eventComponent, IEquatable<MauronCode_timeStamp>	{

		//constructors
		private EventHistory () : base () {}
		public EventHistory (MauronCode_eventClock clock) : this () {
			CLOCK_events=clock;
			TREE_timeUnits.SetValue("created",clock.TimeStamp);
		}
		
		//The Clock
		private MauronCode_eventClock CLOCK_events;
		public MauronCode_eventClock Clock {
			get { 
				if(CLOCK_events==null){
					throw NullError("Clock can not be null!",this,typeof(MauronCode_eventClock));
				}
				return CLOCK_events; 
			}
		}

		//DataTrees
		private static string[] KEYS_default = new string[]{"created","updated","checked","executed"};
		private MauronCode_dataTree<string, MauronCode_timeStamp> TREE_timeUnits=new MauronCode_dataTree<string, MauronCode_timeStamp>(KEYS_default);

		//Created
		public MauronCode_timeStamp LastCreated {
			get {
				return TREE_timeUnits.Value("created");
			}
		}
		public EventHistory SetCreated (MauronCode_timeStamp time) {
			TREE_timeUnits.SetValue("created", time);
			return this;
		}

		//Checked
		public MauronCode_timeStamp LastChecked {
			get {
				//never:lastCreated
				if(!TREE_timeUnits.IsSet("checked")){ 
					return LastCreated;
				}

				return TREE_timeUnits.Value("checked");
			}
		}
		public EventHistory SetLastChecked (MauronCode_timeStamp time) {
			TREE_timeUnits.SetValue("checked", time);
			return this;
		}
		public bool WasChecked {
			get { return TREE_timeUnits.IsSet("checked"); }
		}

		//Updated
		public MauronCode_timeStamp TimeUpdated {
			get {
				//never: created
				if(!TREE_timeUnits.IsSet("updated")){
					return LastCreated;
				}
				return TREE_timeUnits.Value("updated");
			}
		}
		public EventHistory SetTimeUpdated (MauronCode_timeStamp time) {
			TREE_timeUnits.SetValue("updated",time);
			return this;
		}
		public bool WasUpdated {
			get { return TREE_timeUnits.IsSet("updated"); }
		}

		//Executed
		public MauronCode_timeStamp LastExecuted {
			get {
				//return never
				if( !TREE_timeUnits.IsSet("executed")) {
					return new MauronCode_timeStamp(Clock,new MauronCode_timeUnit(-1,Clock));
				}
				return TREE_timeUnits.Value("excuted");
			}
		}
		public EventHistory SetLastExecuted (MauronCode_timeStamp time) {
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

		public bool Equals(MauronCode_timeStamp other, EventUtility_precision precisionHandler) {
			if( !MauronCode_eventClock.EQUALS(Clock, other.Clock, precisionHandler) )
				return false;
			return true;
		}
		public bool Equals(MauronCode_timeStamp other){
			EventUtility_precision handler = Clock.PrecisionHandler;
			return MauronCode_eventClock.EQUALS(Clock, other.Clock, Clock.PrecisionHandler);
		}

		#region IEquatable
		bool IEquatable<MauronCode_timeStamp>.Equals (MauronCode_timeStamp other) {
			return Equals(other, Clock.PrecisionHandler);
		}
		#endregion


	}

}