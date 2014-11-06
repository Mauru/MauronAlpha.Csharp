using System;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.Units {
	
	//A Class that keeps track of a timespan
	public class MauronCode_timeSpan:MauronCode_dataObject {

		//constructor
		public MauronCode_timeSpan():base(DataType_timeSpan.Instance) {}

		//The masterclock for the timespan
		private MauronCode_eventClock CLOCK_clock;
		public MauronCode_eventClock Clock { get {
			if(CLOCK_clock==null){
				throw NullError("MauronCode_eventClock can not be null!", this,typeof(MauronCode_eventClock));
			}
			return CLOCK_clock;
		} }
		public MauronCode_timeSpan SetClock(MauronCode_eventClock clock) {
			CLOCK_clock=clock;
			return this;
		}

		//start of the timespan
		private MauronCode_timeUnit TU_start;
		public MauronCode_timeUnit Start {
			get {
				if(TU_start==null){
					throw NullError("Start can not be null!",this,typeof(MauronCode_timeUnit));
				}
				return TU_start;
			}
		}
		public MauronCode_timeSpan SetStart(MauronCode_timeUnit tu){
			TU_start=tu;
			return this;
		}

		//end of the timespan
		private MauronCode_timeUnit TU_end;
		public MauronCode_timeUnit End {
			get {
				if(TU_end==null){
					return Start;
				}
				return TU_end;
			}
		}
		public MauronCode_timeSpan SetEnd(MauronCode_timeUnit tu){
			TU_end=tu;
			return this;
		}

		//the potential intervals
		public long Ticks {
			get {
				if(Start==End){
					return Start.Ticks;
				}
				return End.Subtract(Start).Ticks;
			}
		}
	}

	//A class that contains data
	public sealed class DataType_timeSpan : DataType {

		#region singleton
		private static volatile DataType_timeSpan instance=new DataType_timeSpan();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_timeSpan ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_timeSpan();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "timeSpan"; } }


	}
}
