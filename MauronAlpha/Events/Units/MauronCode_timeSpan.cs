using System;
using MauronAlpha.ExplainingCode;

namespace MauronAlpha.Events.Units {
	
	//A Class that keeps track of a timespan
	public class MauronCode_timeSpan:MauronCode {

		//constructor
		public MauronCode_timeSpan():base(CodeType_timeSpan.Instance) {}

		//The masterclock for the timespan
		private MauronCode_eventClock CLOCK_clock;

		//start of the timespan
		private MauronCode_timeUnit TU_start;

		//end of the timespan
		private MauronCode_timeUnit TU_end;

		//the potential intervals
		private long INT_ticks;
		private long INT_ms;

	}

	//A class that contains data
	public sealed class CodeType_timeSpan : CodeType {

		#region singleton
		private static volatile CodeType_timeSpan instance=new CodeType_timeSpan();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_timeSpan ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_timeSpan();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "timeSpan"; } }

	}
}
