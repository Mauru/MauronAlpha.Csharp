using MauronAlpha.HandlingData;
using MauronAlpha.Events;

namespace MauronAlpha.Input.Keyboard {

	
	public class KeyPressSequence:MauronCode_dataList<KeyPress>,I_eventReceiver,I_eventSender {

		#region I_eventReceiver
	
		public I_eventReceiver SubscribeToEvents ( ) {
			return this;
		}

		public I_eventReceiver SubscribeToEvent (MauronCode_eventClock clock, string message) {
			return this;
		}

		//Receive an event
		public I_eventReceiver ReceiveEvent (MauronCode_event e){
			return this;
		}

		#endregion

		#region I_eventSender
		public MauronCode_eventClock EventClock {
			get { throw new System.NotImplementedException(); }
		}

		public I_eventSender SendEvent (MauronCode_eventClock clock, string code, MauronCode_dataSet data) {
			throw new System.NotImplementedException();
		}
		#endregion

	}

}