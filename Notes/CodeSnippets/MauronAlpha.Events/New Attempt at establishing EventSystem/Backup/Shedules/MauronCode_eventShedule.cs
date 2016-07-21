using System;
using MauronAlpha.ExplainingCode;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events.Shedules {

	//A event Shedule that is executed in intervals
	public class MauronCode_eventShedule:MauronCode {

		//constructor
		public MauronCode_eventShedule (EventUnit_clock clock, MauronCode_timeSpan interval):base(CodeType_eventShedule.Instance){
			SetClock (clock);
			SetInterval (interval);
		}

		#region The interval for when this shedule is executed
		private MauronCode_timeSpan TIME_interval;
		public MauronCode_timeSpan Interval {
			get { 
				if (TIME_interval == null) {
					NullError ("Interval can not be null!,(Interval)", this, typeof(MauronCode_timeSpan));
				}
				return TIME_interval;
			}
		}
		public MauronCode_eventShedule SetInterval(MauronCode_timeSpan interval){
			TIME_interval = interval;
			return this;
		}
		#endregion
		#region The EventClock this shedule belongs to
		private EventUnit_clock CLOCK_events;
		public EventUnit_clock Clock {
			get { 
				if (CLOCK_events==null) {
					NullError ("Clock can not be null!", this, typeof(EventUnit_clock));
				}
				return CLOCK_events;
			}
		}
		public MauronCode_eventShedule SetClock(EventUnit_clock clock){
			CLOCK_events = clock;
			return this;
		}
		#endregion

	}

	//Class decription
	public sealed class CodeType_eventShedule : CodeType {
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
		public override string Name { get { return "event"; } }
	}

}

