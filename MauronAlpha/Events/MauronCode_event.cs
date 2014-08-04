using System;

using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Events {
	
	//A Event
	public class MauronCode_event:MauronCode {

		//constructor
		public MauronCode_event():base(CodeType_event.Instance){}

		#region Set the caller
		private I_eventSender IE_caller;
		public I_eventSender Caller {
			get {
			if(IE_caller==null) {
				Error("Event Caller can not be null", this);
			}
			return IE_caller;
		} }
		public MauronCode_event SetCaller (I_eventSender caller) {
			IE_caller=caller;
			return this;
		}
		#endregion

		#region Set the receiver
		private I_eventReceiver IE_receiver;
		public I_eventReceiver Receiver { get {return IE_receiver; } }
		public MauronCode_event SetReceiver (I_eventReceiver receiver) {
			IE_receiver=receiver;
			return this;
		}
		#endregion

		#region Condition for an Event
		public delegate bool Delegate_condition(I_eventReceiver receiver, MauronCode_event e);
		public static bool CheckCondition(I_eventReceiver receiver, MauronCode_event e, Delegate_condition check) {
			return check(receiver,e);
		}
		public bool IsCondition { 
			get {
				return Condition(Receiver,this);
			}
		}
		private Delegate_condition DEL_condition;
		public Delegate_condition Condition { get {
			if(DEL_condition==null) {
				Error("Condition can not be null!", this);
			}
			return DEL_condition;
		} }
		public MauronCode_event SetCondition(Delegate_condition condition){
			DEL_condition=condition;
			return this;
		}
		#endregion

		#region Trigger Method
		public delegate void Delegate_trigger(I_eventReceiver receiver, MauronCode_event e);
		public static void Execute(I_eventReceiver receiver, MauronCode_event e, Delegate_trigger trigger) {
			trigger(receiver,e);
		}
		private Delegate_trigger DEL_trigger;
		public Delegate_trigger Trigger {
			get {
				if(DEL_trigger==null) {
					Error("Trigger can not be null!", this);
				}
				return DEL_trigger;
			}
		}
		public MauronCode_event SetTrigger (Delegate_trigger trigger) {
			DEL_trigger=trigger;
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

		#region The Event Data
		private MauronCode_dataSet DS_data= new MauronCode_dataSet();
		public MauronCode_dataSet Data { get { return DS_data; } }
		public MauronCode_event SetData(MauronCode_dataSet data) {
			DS_data=data;
			return this;
		}
		public MauronCode_event SetData(string key, object val) {
			Data.Store(key,val);
			return this;
		}
		#endregion
	
		#region the Event Shedule
		private MauronCode_eventShedule ES_eventShedule;
		public MauronCode_eventShedule Shedule { 
			get { 
				if(ES_eventShedule==null) {
					Error("EventShedule can not be null!", this);
				}
				return ES_eventShedule; 
			}
		}
		public MauronCode_event SetShedule(MauronCode_eventShedule eventShedule) {
			ES_eventShedule=eventShedule;
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
