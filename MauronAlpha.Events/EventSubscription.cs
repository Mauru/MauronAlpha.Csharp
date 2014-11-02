using MauronAlpha.HandlingData;
using MauronAlpha.Events.Units;
using MauronAlpha.HandlingErrors;

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
					NullError("I_eventReceiver can not be null!", this,typeof(I_eventReceiver));
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

		//The related event clock
		private MauronCode_eventClock CLOCK_events;
		public MauronCode_eventClock Clock { get {
			if (CLOCK_events == null) {
				NullError ("Clock can not be null!", this, typeof(MauronCode_eventClock));
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
			Receiver.ReceiveEvent (Clock,e);
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
