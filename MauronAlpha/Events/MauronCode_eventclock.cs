using System;
using MauronAlpha.Settings;
using MauronAlpha.ExplainingCode;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events {

	//A class keeping Time
	public class MauronCode_eventClock:MauronCode {

		//Is this clock the System Time
		public MauronCode_timeUnit SystemTime {
			get { return MauronAlpha.Events.SystemTime.Instance.Time; }
		}
		public virtual bool IsSytemTime { get { return false; } }
		
		//constructor
		public MauronCode_eventClock():base(CodeType_eventClock.Instance) {}
		public MauronCode_eventClock (MauronCode_eventClock clock) : this() { 
			SetMasterClock(clock);
		}

		//MasterClock
		private MauronCode_eventClock EC_masterClock;
		public MauronCode_eventClock MasterClock {
			get {
				if(EC_masterClock==null) {
					return MauronAlpha.Events.SystemTime.Instance;
				}
				return EC_masterClock;
			}
		}
		public MauronCode_eventClock SetMasterClock(MauronCode_eventClock clock) {
			EC_masterClock=clock;
			return this;
		}

		//Get the time
		private MauronCode_timeUnit TU_time; 
		public virtual MauronCode_timeUnit Time {
			get { 
				if(TU_time==null) {
					TU_time=new MauronCode_timeUnit(0, this);
				}
				return TU_time; 
			}
		}
		public MauronCode_eventClock SetTime(MauronCode_timeUnit time) {
			if(IsSytemTime) {
				Error("Can not set System Time",this);
			}
			TU_time=time;
			return this;
		}
		public MauronCode_eventClock SetTime (long n) {
			return SetTime(new MauronCode_timeUnit(n,this));
		}
	
		//Advance the internal Time by one
		public MauronCode_eventClock Tick() {
			SetTime(Time.Ticks+1);
			return this;
		}
	
		public MauronCode_eventClock SubmitEvent(MauronCode_event e){

			//TODO: handle event submission
			Error("Event Submission has not been implemented yet!");

			return this;
		}
	}

	//Code Description
	public sealed class CodeType_eventClock:CodeType {
		#region singleton
		private static volatile CodeType_eventClock instance=new CodeType_eventClock();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_eventClock ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_eventClock();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "eventClock"; } }
	}
}
