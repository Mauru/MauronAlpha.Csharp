using System;
using System.Collections.Generic;
using MauronAlpha.ExplainingCode;
using MauronAlpha.Settings;

namespace MauronAlpha.Events {

	// A Timing based execution check for an event
	public class MauronCode_eventShedule:MauronCode,I_hasDefaultSettings {
		
		//constructor
		public MauronCode_eventShedule(MauronCode_eventClock clock):base(CodeType_eventShedule.Instance) {
			SetClock(clock);
			FromDefaults(new string[3] {"Interval","LastChecked","LastExecuted"});
		}

		#region I_HasDefaultSettings

		#region create From Defaults
		public MauronCode_eventShedule FromDefaults(string[] query){
			List<string> q_defaults = new List<string>(query);
			if (q_defaults.Contains("Clock")) SetClock((MauronCode_eventClock) Defaults.GetDefault("Clock",this));
			if (q_defaults.Contains("Interval")) SetInterval((MauronCode_timeUnit) Defaults.GetDefault("Interval",this));
			if (q_defaults.Contains("LastChecked")) SetLastChecked((MauronCode_timeUnit) Defaults.GetDefault("LastChecked",this));
			if (q_defaults.Contains("LastExecuted")) SetLastExecuted((MauronCode_timeUnit) Defaults.GetDefault("LastExecuted",this));
			return this;
		}
		I_hasDefaultSettings I_hasDefaultSettings.FromDefaults (string[] query) {
			return FromDefaults(query);
		}
		public MauronCode_eventShedule FromDefaults(){
			SetClock((MauronCode_eventClock) Defaults.GetDefault("Clock", this));
			SetInterval((MauronCode_timeUnit) Defaults.GetDefault("Interval", this));
			SetLastChecked((MauronCode_timeUnit) Defaults.GetDefault("LastChecked", this));
			SetLastExecuted((MauronCode_timeUnit) Defaults.GetDefault("LastExecuted", this));
			return this;
		}
		I_hasDefaultSettings I_hasDefaultSettings.FromDefaults ( ) {
			return FromDefaults();
		}
		#endregion
		
		#region get the default settings
		public MauronCode_defaultSettingsObject Defaults {
			get {
				return MauronCode_eventShedule_defaults.Instance;
			}
		}
		MauronCode_defaultSettingsObject I_hasDefaultSettings.Defaults {
			get {	return Defaults; }
		}
		#endregion
		
		#endregion

		#region original feature-set
		#region The clock that determins the time
		private MauronCode_eventClock EC_clock;
		public MauronCode_eventClock Clock {
			get {
				if(EC_clock==null){
					Error("Clock can not be null!", this);
				}
				return EC_clock;
			}
		}
		public MauronCode_eventShedule SetClock(MauronCode_eventClock clock){
			EC_clock=clock;
			return this;
		}
		#endregion

		#region the interval in ticks when the clock is checked
		public MauronCode_timeUnit TU_interval;
		public MauronCode_timeUnit Interval {
			get { 
				if(TU_interval==null) {
					Error("Interval can not be null!", this);
				}
				return TU_interval;
			}
		}
		public MauronCode_eventShedule SetInterval(MauronCode_timeUnit time){
			TU_interval=time;
			return this;
		}
		#endregion

		#region Last Checked
		public MauronCode_timeUnit TU_lastChecked;
		public MauronCode_timeUnit LastChecked {
			get {
				return TU_lastChecked;
			}
		}
		public MauronCode_eventShedule SetLastChecked(MauronCode_timeUnit time) {
			TU_lastChecked=time;
			return this;
		}
		#endregion

		#region Last Executed
		public MauronCode_timeUnit TU_lastExecuted;
		public MauronCode_timeUnit LastExecuted {
			get {
				return TU_lastExecuted;
			}
		}
		public MauronCode_eventShedule SetLastExecuted (MauronCode_timeUnit time) {
			TU_lastExecuted=time;
			return this;
		}
		#endregion

		#region Linked Event
		public MauronCode_event E_event;
		public MauronCode_event Event {
			get{
				if(E_event==null) { Error("Event can not be null!", this); }
				return E_event;
			}
		}
		public MauronCode_eventShedule SetEvent(MauronCode_event e) {
			E_event=e;
			return this;
		}
		#endregion

		#region How often has this event been executed?
		public int INT_executions=0;
		public int Executions { get {
			return INT_executions;
		} }
		public MauronCode_eventShedule SetExecutions(int n){
			INT_executions=n;
			return this;
		}
		#endregion

		#region How often CAN this event be executed
		public int INT_maxExecutions=1;
		public int MaxExecutions {
			get {
				return INT_maxExecutions;
			}
		}
		public MauronCode_eventShedule SetMaxExecutions (int n) {
			INT_maxExecutions=n;
			return this;
		}
		#endregion
		
		#region How often has this event been checked?
		public int INT_checks=0;
		public int Checks { get { return INT_checks; } }
		public MauronCode_eventShedule SetChecks(int n) {
			INT_checks=n;
			return this;
		}
		#endregion

		#region How CAN this event be checked?
		public int INT_MaxChecks=0;
		public int MaxChecks { get { return INT_MaxChecks; } }
		public MauronCode_eventShedule SetMaxChecks (int n) {
			INT_MaxChecks=n;
			return this;
		}
		#endregion

		//Force Event execution regardless of anything
		public MauronCode_eventShedule ForceExecute() {
			SetLastExecuted(Clock.Time);
			SetExecutions(Executions+1);
			Event.Trigger(Event.Receiver, Event);
			return this;
		}

		//Cycle the event shedule
		public MauronCode_eventShedule Cycle() {
			
			//Only one check per cycle
			if(LastChecked==Clock.Time) {
				return this;
			}

			//Cycle interval
			if(LastChecked.Add(Interval).SmallerOrEqual(Clock.Time)){
				return this;
			}
						
			//Set Check Timestamp
			SetLastChecked(Clock.Time);

			//check count
			if(MaxChecks>0&&MaxChecks<Checks) {
				//check limit reached, shedule done
				SheduleDone();
				return this;
			}
			SetChecks(Checks+1);

			//check condition
			if(!Event.IsCondition){
				return this;
			}

			//Execution count
			if(MaxExecutions>0&&MaxExecutions<Executions){
				return SheduleDone(true);
			}
			
			return ForceExecute();
		}

		#region The Shedule is done, either because Checked or Executions has reached a limit, assume Checked Limit as default
		public MauronCode_eventShedule SheduleDone(bool dueToExecutions) {
			return this;
		}
		public MauronCode_eventShedule SheduleDone() {
			//check limit reached, deRegister
			return SheduleDone(false);
		}
		#endregion
		#endregion

	}

	//Code Description
	public sealed class CodeType_eventShedule: CodeType {
		#region singleton
		private static volatile CodeType_eventShedule instance=new CodeType_eventShedule();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_eventShedule ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_eventShedule();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "eventShedule"; } }
	}

}
