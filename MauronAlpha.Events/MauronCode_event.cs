using System;

using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingData;
using MauronAlpha.Events.Units;
using MauronAlpha.Events.Data;
using MauronAlpha.Events.Defaults;

namespace MauronAlpha.Events {
	
	//A Event
	public class MauronCode_event:MauronCode {

		//constructor
		private MauronCode_event():base(CodeType_event.Instance){}
		public MauronCode_event(MauronCode_eventClock clock, I_eventSender sender, Delegate_condition condition, Delegate_trigger trigger, string code):this() {
			
			//Assign required fields
			SetMessage(code);			
			SetSender(sender);
			SetCondition(condition);
			SetTrigger(trigger);
			
			//generate the event shedule
			MauronCode_eventShedule shedule = new MauronCode_eventShedule(clock);
			shedule.SetEvent(this);
			SetShedule(shedule);
		}
		public MauronCode_event(MauronCode_eventClock clock, I_eventSender sender, string code, MauronCode_dataSet data):this(clock,sender,EventCondition.Always,EventTrigger.Nothing,code){
			SetData (data);
		}

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

		#region The Receiver of the event

		private I_eventReceiver IE_receiver;
		public I_eventReceiver Receiver { get { return IE_receiver; } }
		public T ReceiverAs<T> ( ) {
			return (T) Receiver;
		}
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
			//set the receiver
			e.SetReceiver (receiver);

			//update shedule
			e.Shedule.SetExecutions(e.Shedule.Executions+1);
			e.Shedule.SetLastExecuted(e.Shedule.Clock.Time);

			//trigger the event
			trigger(receiver,e);

			if(e.Shedule.MaxExecutions>0&&e.Shedule.MaxExecutions>=e.Shedule.Executions) {
				e.Shedule.SheduleDone();
			}
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
		private MauronCode_dataSet DS_data;
		public MauronCode_dataSet Data { get { 
			if(DS_data==null) {
				SetData(new MauronCode_dataSet("Event Data"));
			}
			return DS_data; 
		} }
		public MauronCode_event SetData(MauronCode_dataSet data) {
			DS_data=data;
			return this;
		}
		/*public MauronCode_event SetData(MauronCode_dataObject data){
			if(data.DataType.IsConvertibleTo(Data.GetType())){
				SetData(
					data.DataType.Convert<MauronCode_dataSet>(data)
				);
			}
			return this;
		}*/
		public MauronCode_event SetData(string key, object val) {
			Data.SetValue(key,val);
			return this;
		}
		public MauronCode_event SetData<T>(string key, T val) {
			Data.SetValue<T>(key, val);
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