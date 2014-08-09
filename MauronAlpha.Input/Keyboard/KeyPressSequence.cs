using MauronAlpha.HandlingData;
using MauronAlpha.Events;

namespace MauronAlpha.Input.Keyboard {

	
	public class KeyPressSequence:MauronCode_dataList<KeyPress>,I_eventReceiver,I_eventSender {
		
		public static bool Delegate_condition(I_eventReceiver receiver, MauronCode_event e){
			return false;
		}

		public static void Delegate_trigger(I_eventReceiver receiver, MauronCode_event e){
			return;
		}

		public I_eventReceiver ReceiveEvent (MauronCode_event e) {
			throw new System.NotImplementedException();
		}

		public bool IsEventCondition (MauronCode_event e) {
			throw new System.NotImplementedException();
		}

		public MauronCode_eventClock EventClock {
			get { throw new System.NotImplementedException(); }
		}

		public I_eventSender SubscribeToEvents ( ) {
			return this;
		}

		public I_eventSender SendEvent (MauronCode_eventClock clock, string code, MauronCode_dataSet data) {
			throw new System.NotImplementedException();
		}
	}

}