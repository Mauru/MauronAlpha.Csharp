using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.Data {

	//A subscription to an event
	public class EventSubscription:MauronCode_dataObject {

		//constructor
		public EventSubscription(string message, I_eventReceiver receiver):base(DataType_locked.Instance){
			SetMessage(message);
			SetReceiver(receiver);
		}

		//The Target of the subscription
		private I_eventReceiver RECEIVER_eventObject;
		public I_eventReceiver Receiver {
			get {
				if(RECEIVER_eventObject==null){
					Error("I_eventReceiver can not be null!", this);
				}
				return RECEIVER_eventObject;
			}
		}
		public EventSubscription SetReceiver(I_eventReceiver receiver){
			RECEIVER_eventObject=receiver;
			return this;
		}

		//The message of the event
		private string STR_message;
		public string Message { get { return STR_message; } }
		public EventSubscription SetMessage(string message){
			STR_message=message;
			return this;
		}

		//The condition for the event to trigger
		private MauronCode_event.Delegate_condition DEL_condition;
		public MauronCode_event.Delegate_condition Condition { get {
			if(DEL_condition==null) {
				Error("Invalid condition!", this);
			}
			return DEL_condition;
		} }
		public EventSubscription SetCondition(MauronCode_event.Delegate_condition condition){
			DEL_condition=condition;
			return this;
		}

		//The Trigger function
		private MauronCode_event.Delegate_trigger DEL_trigger;
		public MauronCode_event.Delegate_trigger Trigger {
			get {
				if( DEL_trigger==null ) {
					Error("Invalid trigger!", this);
				}
				return DEL_trigger;
			}
		}
		public EventSubscription SetTrigger (MauronCode_event.Delegate_trigger trigger) {
			DEL_trigger=trigger;
			return this;
		}
		
		//The Event
		private MauronCode_event EVENT_object;
		public MauronCode_event Event {
			get {
				if(EVENT_object==null){
					Error("MauronCode_event can not be null!", this);
				}
				return EVENT_object;
			}
		}
		public EventSubscription SetEvent(MauronCode_event e){
			EVENT_object=e;
			return this;
		}
	
		//The TestHistory
		public MauronCode_timeStamp TimeStamp {}
	}

}
