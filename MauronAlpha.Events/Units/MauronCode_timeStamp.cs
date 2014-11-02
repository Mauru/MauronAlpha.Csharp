using System;

using MauronAlpha.HandlingData;


namespace MauronAlpha.Events.Units {
	
	//A TimeStamp of an Object
	public class MauronCode_timeStamp:MauronCode_dataObject {

		public static MauronCode_timeStamp CONVERT_synchronizeClock(MauronCode_eventClock clock, MauronCode_timeUnit time) {
			MauronCode_eventClock result = clock.SYNCHRONIZE_fromTimeUnit(time);
			return new MauronCode_timeStamp(result,result.Time);
		}

		//the time when this unit was created
		private MauronCode_timeUnit TIME_created;
		public MauronCode_timeUnit Created {
			get {
				return TIME_created;
			}
		}
		
		//the time when this unit was last updated
		private MauronCode_timeUnit TIME_updated;
		public MauronCode_timeUnit Updated {
			get {
				if( TIME_updated==null ) {
					SetUpdated(TIME_created.Instance);
				}
				return TIME_updated;
			}
		}
		public MauronCode_timeStamp SetUpdated (MauronCode_timeUnit time) {
			if(!time.Clock.Equals(Clock)) {
				CONVERT_synchronizeClock(Clock, time);
			}
			TIME_updated=time;
			return this;
		}

		private MauronCode_eventClock CLOCK_event;
		public MauronCode_eventClock Clock {
			get {
				return CLOCK_event;
			}
		}

		//constructor
		public MauronCode_timeStamp(MauronCode_eventClock clock, MauronCode_timeUnit time):base(DataType_timeStamp.Instance){
			CLOCK_event = clock;
			TIME_created = time;
			TIME_updated = time;
		}
	}

	//A description of the dataType
	public sealed class DataType_timeStamp:DataType {
		#region singleton
		private static volatile DataType_timeStamp instance=new DataType_timeStamp();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_timeStamp ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_timeStamp();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "timeStamp"; } }
		
	}

}