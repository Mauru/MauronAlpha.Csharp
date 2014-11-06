using MauronAlpha.HandlingData;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events {
	
	//A Event
	public class MauronCode_event:MauronCode_eventComponent {

		#region Constructors
		private MauronCode_event():base(){}
		public MauronCode_event(I_eventSender sender, string code):this() {
			SetMessage(code);			
			SetSender(sender);
		}
		#endregion

		#region The Sender of the event
		
		private I_eventSender IE_sender;
		public I_eventSender Sender {
			get {
			if(IE_sender==null) {
				throw NullError("Event Sender can not be null", this, typeof(I_eventSender));
			}
			return IE_sender;
		} }
		public T SenderAs<T>(){
			return (T) Sender;
		}
		public MauronCode_event SetSender (I_eventSender sender) {
			IE_sender=sender;
			return this;
		}

		#endregion

		#region The Event Message
		private string STR_message;
		public string Message { 
			get {
				return STR_message;
			}
		}
		public MauronCode_event SetMessage(string message) {
			STR_message=message;
			return this;
		}
		#endregion

		#region Let an event submit itself to an event clock
		public MauronCode_event Submit(MauronCode_eventClock clock) {
			clock.SubmitEvent (this);
			return this;
		}
		#endregion
	
	}

}