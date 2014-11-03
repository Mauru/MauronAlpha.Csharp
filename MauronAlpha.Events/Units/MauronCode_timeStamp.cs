using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;


namespace MauronAlpha.Events.Units {
	
	//A TimeStamp of an Object
	public class MauronCode_timeStamp:MauronCode_dataObject {

		//Utility
		/// <summary>
		/// Tries to set Clock to time
		/// </summary>
		/// <param name="clock">The clock to be set</param>
		/// <param name="time">the time </param>
		/// <returns>returns the updated clock as timestamp</returns>
		/// <remarks> Might want to move this elsewhere or at least observe the dependency chain more </remarks>

		public static MauronCode_timeStamp CONVERT_synchronizeClock(MauronCode_eventClock clock, MauronCode_timeUnit time) {
			MauronCode_eventClock result = clock.SYNCHRONIZE_fromTimeUnit(time);
			return new MauronCode_timeStamp(result,result.Time);
		}

/*==========================================================================================
Properties
==========================================================================================*/

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

		private MauronCode_timeUnit TIME_instanced;
		public MauronCode_timeUnit Instanced {
			get {
				if(TIME_instanced == null){
					Exception("TIME_instanced was not set!,(Instanced)",this,ErrorResolution.CreateUnreliable);
				}
			}
		}

		private MauronCode_eventClock CLOCK_event;
		public MauronCode_eventClock Clock {
			get {
				if(CLOCK_event == null)
				return CLOCK_event;
			}
		}

		//constructor
		public MauronCode_timeStamp(MauronCode_eventClock clock, MauronCode_timeUnit time):base(DataType_timeStamp.Instance){
			CLOCK_event = clock;
			TIME_created = time;
			TIME_updated = time;
		}
		private MauronCode_timeStamp(MauronCode_timeStamp instance) {
			CLOCK_event = instance.Clock;
			TIME_created = instance.Created;
			TIME_updated = instance.Updated;
			TIME_instanced = SystemTime.Time;
		}

		public MauronCode_timeStamp Instance {
			get {
				return new MauronCode_timeStamp(this);
			}
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