using System;
using MauronAlpha.Events.Interfaces;

namespace MauronAlpha.Events.Units
{
    public class EventUnit_subscription:EventComponent_unit {

        public EventUnit_subscription(
            string code,
			I_eventSubscriber subscriber,
            I_eventSubscriptionModel subscriptionModel
        ):base() {
			IE_subscriber = subscriber;
			IE_subscriptionModel = subscriptionModel;
			STR_code = code;			
        }

		private I_eventSubscriptionModel IE_subscriptionModel;
		public I_eventSubscriptionModel SubscriptionModel {
			get {
				return IE_subscriptionModel;
			}
		}
		
		private I_eventSubscriber IE_subscriber;
		public I_eventSubscriber Subscriber {
			get {
				return IE_subscriber;
			}
		}

		private string STR_code;
		public string Code {
			get {
				return STR_code;
			}
		}

		public static bool CONDITION_compareEventCode(EventUnit_event unit, EventUnit_subscription subscription){
			return unit.Code == subscription.Code;
		}

		private long INT_executionCount = 0;
		public long ExecutionCount {
			get {
				return INT_executionCount;
			}
		}
		public EventUnit_subscription ItterateExecutionCount(EventUnit_timeStamp timeStamp) {
			INT_executionCount++;
			return this;
		}

		public bool Equals(EventUnit_subscription other) {
			if(STR_code!=other.Code)
				return false;
			if(!SubscriptionModel.Equals(other.SubscriptionModel))
				return false;
			return IE_subscriber.Equals(other.Subscriber);
		}
    }
}
