using System;
using MauronAlpha.Settings;
using MauronAlpha.ExplainingCode;

namespace MauronAlpha.Events {

	//A class keeping Time
	public class MauronCode_eventClock:MauronCode {
		
		//constructor
		public MauronCode_eventClock():base(CodeType_eventClock.Instance) {}

		//MasterClock
		private MauronCode_eventClock EC_masterClock;
		public MauronCode_eventClock MasterClock {
			get {
				if(EC_masterClock==null) {
					return this;
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
		public MauronCode_timeUnit Time {
			get { 
				if(TU_time==null) {
					TU_time=new MauronCode_timeUnit(0, this);
				}
				return TU_time; 
			}
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
