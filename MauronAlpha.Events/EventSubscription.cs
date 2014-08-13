using MauronAlpha.HandlingData;
using MauronAlpha.Events.Units;
using MauronAlpha.ErrorHandling;

namespace MauronAlpha.Events {

	//A subscription to an event
	public class EventSubscription:MauronCode_dataObject {

		//constructor
		public EventSubscription(MauronCode_eventClock clock, string message, I_eventReceiver receiver):base(DataType_locked.Instance){
			SetClock(clock);
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

		//The related event clock
		private MauronCode_eventClock CLOCK_events;
		public MauronCode_eventClock Clock { get {
			if (CLOCK_events == null) {
				Error ("Clock can not be null!", this);
			}
			return CLOCK_events;
		}}
		public EventSubscription SetClock(MauronCode_eventClock clock){
			CLOCK_events = clock;
			return this;
		}

		//Testing an event against a subscription
		public EventSubscription CheckAgainst(MauronCode_event e){
			//event code
			if (e.Message != Message) {
				return this;
			}
			History.SetLastChecked (Clock.Time);
			//condition
			History.SetLastExecuted (Clock.Time);
			Receiver.ReceiveEvent (e);
			History.SetExecutionCount (History.ExecutionCount + 1);

			return this;
		}
	
		//The TestHistory
		private EventHistory HISTORY_events; 
		public EventHistory History {
			get {
				if(HISTORY_events==null){
					SetHistory(new EventHistory (Clock));
				}
				return HISTORY_events;
			}
		}
		public EventSubscription SetHistory(EventHistory h){
			HISTORY_events=h;
			return this;
		}
	}

}
