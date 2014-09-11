using System;
using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingErrors;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.HandlingExceptions {
	
	//A object with built in Exception Awareness
	public class MauronCode_exceptionAwareObject:MauronCode_dataObject, I_eventSender, I_eventReceiver {
		
		//constructor
		public MauronCode_exceptionAwareObject(int overFlowLimit):base(DataType_exceptionAwareObject.Instance) {
			SubscribeToEvents();
		}

		private MauronCode_dataList<MauronCode_exception> DATA_exceptions;
		public MauronCode_dataList<MauronCode_exception> Exceptions {
			get {
				if(DATA_exceptions==null){
					DATA_exceptions=new MauronCode_dataList<MauronCode_exception>();
				}
				return DATA_exceptions;
			}
		}

		#region send an Event
		public MauronCode_exceptionAwareObject SendEvent(MauronCode_eventClock clock, MauronCode_event e) {
			clock.SubmitEvent(e);
			return this;
		}
		public MauronCode_exceptionAwareObject SubscribeToEvents() {
			ExceptionClock.Instance.SubscribeToEvent("Exception",this);
			return this;
		}
		public MauronCode_exceptionAwareObject SubscribeToEvent(string message, MauronCode_eventClock clock) {
			clock.SubscribeToEvent("Exception",this);
			return this;
		}
		public MauronCode_eventClock ExceptionHandler {
			get {
				return ExceptionClock.Instance;
			}
		}
		#endregion

		#region Handle an exception
		public static  void HandleException(ExceptionClock clock, MauronCode_exception e, MauronCode_exceptionAwareObject source) {
			clock.SubmitEvent(new MauronCode_event(source,"Exception"));
		}
		public static MauronCode_exception Exception (string msg, MauronCode_exceptionAwareObject o, ErrorResolution resolution) {
			MauronCode_exception e=new MauronCode_exception(msg, o, resolution);
			HandleException(ExceptionClock.Instance, e,o);
			return e;
		}
		#endregion

		#region I_eventSender
		I_eventSender I_eventSender.SendEvent (MauronCode_eventClock clock, MauronCode_event e) {
			return SendEvent(clock,e);
		}
		#endregion

		I_eventReceiver I_eventReceiver.SubscribeToEvents ( ) {
			return SubscribeToEvents();
		}

		I_eventReceiver I_eventReceiver.SubscribeToEvent (MauronCode_eventClock clock, string message) {
			return SubscribeToEvent(clock,message);
		}

		I_eventReceiver I_eventReceiver.ReceiveEvent (MauronCode_eventClock clock, MauronCode_event e) {
			return ReceiveEvent(clock,e);
		}
	}

	// DataType description
	public sealed class DataType_exceptionAwareObject:DataType {
		#region singleton
		private static volatile DataType_exceptionAwareObject instance=new DataType_exceptionAwareObject();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static DataType_exceptionAwareObject ( ) { }
		public static DataType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DataType_exceptionAwareObject();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "exceptionAwareObject"; } }
	}

}
