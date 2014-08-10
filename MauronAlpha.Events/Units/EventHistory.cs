using System;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.Units
{
	
	//A class keeping track of event firings etc
	public class EventHistory:MauronCode_dataObject	{

		//constructor
		public EventHistory (MauronCode_eventClock clock):base(DataType_maintaining.Instance){
			SetClock(clock);
			SetLastCreated(new MauronCode_timeUnit(clock.Time));
		}
		
		//The Clock
		private MauronCode_eventClock CLOCK_events;
		public MauronCode_eventClock Clock {
			get { 
				if(CLOCK_events==null){
					Error("Clock can not be null!",this);
				}
				return CLOCK_events; 
			}
		}
		public EventHistory SetClock(MauronCode_eventClock clock){
			CLOCK_events=clock;
			return this;
		}

		//Created
		private MauronCode_timeUnit TIME_created;
		public MauronCode_timeUnit LastCreated {
			get {
				return TIME_created;
			}
		}
		public EventHistory SetLastCreated(MauronCode_timeUnit time) {
			TIME_created=time;
			return this;
		}

		//Checked
		private MauronCode_timeUnit TIME_checked;
		public MauronCode_timeUnit LastChecked {
			get {
				if(TIME_checked==null){
					SetLastChecked(new MauronCode_timeUnit(0,Clock));
				}
				return TIME_created;
			}
		}
		public EventHistory SetLastChecked (MauronCode_timeUnit time) {
			TIME_checked=time;
			return this;
		}

		//Updated
		private MauronCode_timeUnit TIME_updated;
		public MauronCode_timeUnit TimeUpdated {
			get {
				if(TIME_updated==null){
					SetTimeUpdated(LastCreated.Instance);
				}
				return TIME_updated;
			}
		}
		public EventHistory SetTimeUpdated(MauronCode_timeUnit time){
			TIME_updated=time;
			return this;
		}

		//Execution count
		private int INT_executions=0;
		public int ExecutionCount {
			get {
				return INT_executions;
			}
		}
		public EventHistory SetExecutionCount(int n) {
			INT_executions=n;
			return this;
		}

		//Executed
		private MauronCode_timeUnit TIME_executed;
		public MauronCode_timeUnit LastExecuted {
			get {
				if( LastExecuted==null ) {
					SetLastExecuted(new MauronCode_timeUnit(0, Clock));
				}
				return LastExecuted;
			}
		}
		public EventHistory SetLastExecuted (MauronCode_timeUnit time) {
			TIME_executed=time;
			return this;
		}

	}

}