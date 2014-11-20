using System;

namespace MauronAlpha.Events.Units
{
    public class EventUnit_subscription:EventComponent_unit {

        public EventUnit_subscription(
            string code,
			I_eventSubscriber subscriber,
            EventHandler.DELEGATE_condition condition,
            EventHandler.DELEGATE_trigger trigger,
            int executionCount
        ):base() {
			IE_subscriber = subscriber;
			D_condition = condition;
			D_trigger = trigger;
			STR_code = code;			
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
        EventHandler.DELEGATE_condition D_condition;
        EventHandler.DELEGATE_trigger D_trigger;

		public bool Equals(EventUnit_subscription other) {
			if(STR_code!=other.Code)
				return false;
			return IE_subscriber.Equals(other.Subscriber);
		}
    }
}
