using MauronAlpha.Events.Units;

using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.Utility {


	//A utility class that can synchronize two clocks (or at least tries to do it)
	public class EventUtility_synchronization:EventUtility {

		//constructor
		public EventUtility_synchronization(EventUtility_precision precisionHandler) {
			UTILITY_precision = precisionHandler;
		}
		private EventUtility_precision UTILITY_precision;

		public EventPrecisionRuleSet PrecisionRuleSet { get { return UTILITY_precision.RuleSet; } }

		//Attempt to "FastForward" time of a clock
		public static MauronCode_eventClock SetClockFromTimeUnit(MauronCode_eventClock targetClock, MauronCode_timeUnit time, EventUtility_precision precisionHandler) {

			//generate an instance
			EventUtility_synchronization handler=new EventUtility_synchronization(precisionHandler);

			//unwrap to eventClock
			MauronCode_eventClock sourceClock=time.Clock;
			
			//We are comparing SystemTime which is always equal, so no setting of any clocks needs to happen
			if(sourceClock.IsSystemTime && targetClock.IsSystemTime)
				return sourceClock;

			if( !targetClock.PrecisionHandler.IsCompatibleWith(precisionHandler) ) {
				//The clocks do NOT share the same precision
				throw Error("Differing PrecisionHandlers!", precisionHandler, ErrorType_precision.Instance);
			}

			//We know the precision is compatible...

			//get the number of Subscriptions each Share
			MauronCode_dataRegistry<EventSubscription> subs_source = sourceClock.Subscriptions.Instance;				
			MauronCode_dataRegistry<EventSubscription> subs_target = targetClock.Subscriptions.Instance;

			
			if(subs_source.Count!=subs_target.Count) {}


			//Do a really basic comparison of keys



			
			//determine the source clock

					
		}

		public static bool EVENTCLOCK_Equals(MauronCode_eventClock source, MauronCode_eventClock other, EventUtility_precision precisionHandler) {
			
			//create a instance of this handler...
			///<remarks> ...just in case we need to share the following complex information mid-calculation (you never know)</remarks>
			EventUtility_synchronization handler = new EventUtility_synchronization(precisionHandler);
			
			//We are dealing with exceptions or system time - these are shortcuts
			if(source.IsSystemTime && other.IsSystemTime) {
				if(source.IsExceptionClock == other.IsExceptionClock)
					Exception("Comparing two ExceptionClocks... Could create an infinite loop!!,(EVENTCLOCK_Equals)",handler,ErrorResolution.Delayed);
					return true;
				if(source.IsExceptionClock)
					return false;
				if(other.IsExceptionClock)
					return false;
			}

			//create a timestamp of both
			MauronCode_timeStamp timeStamp_source	= new MauronCode_timeStamp(source,source.Time);
			MauronCode_timeStamp timeStamp_other	= new MauronCode_timeStamp(other,other.Time);

			long ticks_source	= timeStamp_source.Created.Ticks;
			long min_source		= ticks_source - precisionHandler.RuleSet.Limit_past;
			long max_source		= ticks_source + precisionHandler.RuleSet.Limit_future;

			long ticks_other	= timeStamp_other.Created.Ticks;
			long min_other		= ticks_other - precisionHandler.RuleSet.Limit_past;
			long max_other		= ticks_other + precisionHandler.RuleSet.Limit_future;

			#region //now we need to dig down (comparison by eventsubscription)
			//These are the subscriptions we are measuring, just need to fill them
			MauronCode_dataIndex<MauronCode_dataList<EventSubscription>> source_subscriptions;
			MauronCode_dataIndex<MauronCode_dataList<EventSubscription>> other_subscriptions;

			
			// These are CURRENTLY ACTIVE eventSubscriptions.
			/// <remark> ... which could end up out of synch because they might get calculated at the same time as this check.</remark>
			MauronCode_dataList<EventSubscription> source_active;
			MauronCode_dataList<EventSubscription> other_active;

			
			foreach(MauronCode_dataIndex<EventSubscription> index in source.Subscriptions.AsIndexRange()){
				//We now have a LIST of EventSubscriptions of the source for an unknown key

				//we want to extract the LAST few Subscriptions
				MauronCode_dataIndex<EventSubscription> minValues_source = index.Range((int) minBounds, (int) sourceTicks);
				MauronCode_dataIndex<EventSubscription> maxValues_source = index.Range((int) sourceTicks+1, (int) EndMax);

				TODO("DO THE SAME FOR TARGETTICKS");

				minLimit = timestamp_other.Created.Ticks - precisionHandler.RuleSet.Limit_past;
				startMin=timestamp_other.Created.Ticks;
				maxLimit=timestamp_other.Created.Ticks+precisionHandler.RuleSet.Limit_future;
				EndMax=timestamp_other.Created.Ticks;

				long otherTicks=timestamp_other.Created.Ticks;

				MauronCode_dataList<EventSubscription> minValues_other=list.Range((int) startMin, (int) otherTicks);
				MauronCode_dataList<EventSubscription> maxValues_other=list.Range((int) otherTicks+1, (int) EndMax);

			}
			#endregion

			//Do a superbasic comparison of ticks
			if(ticks_source==ticks_other)



		}

		public EventUtility_synchronization Instance {
			get {
				return new EventUtility_synchronization(UTILITY_precision);
			}
		}

	}
}
