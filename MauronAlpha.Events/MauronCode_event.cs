using System;

using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingData;
using MauronAlpha.Events.Units;

namespace MauronAlpha.Events {
	
	//A Event
	public class MauronCode_event:MauronCode {

		#region Constructors
		private MauronCode_event():base(CodeType_event.Instance){}
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
				Error("Event Sender can not be null", this);
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


	//Class decription for an event
	public sealed class CodeType_event : CodeType {
		#region singleton
		private static volatile CodeType_event instance=new CodeType_event();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_event ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_event();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name { get { return "event"; } }
	}
}