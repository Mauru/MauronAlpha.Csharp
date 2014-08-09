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
				Error("MauronCode_eventClock can not be null!", this);
			}
			return CLOCK_clock;
		} }

		//start of the timespan
		private MauronCode_timeUnit TU_start;

		//end of the timespan
		private MauronCode_timeUnit TU_end;

		//the potential intervals
		private long INT_ticks;
		private long INT_ms;

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
