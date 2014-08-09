using MauronAlpha.HandlingData;
using MauronAlpha.Events;

namespace MauronAlpha.Input.Keyboard {

	
	public class KeyPressSequence:MauronCode_dataList<KeyPress>,I_eventReceiver,I_eventSender {



		#region I_eventReceiver
		public I_eventReceiver SubscribeToEvents ()	{
			throw new System.NotImplementedException ();
		}

		public I_eventReceiver SubscribeToEvent (MauronCode_event e){
			throw new System.NotImplementedException ();
		}

		public I_eventReceiver ReceiveEvent (MauronCode_event e){
			throw new System.NotImplementedException ();
		}

		public bool IsEventCondition (MauronCode_event e){
			throw new System.NotImplementedException ();
		}

		public I_eventSender SendEvent (MauronCode_eventClock clock, string code, MauronCode_dataSet data){
			throw new System.NotImplementedException ();
		}

		public MauronCode_eventClock EventClock {
			get {
				throw new System.NotImplementedException ();
			}
		}
		#endregion

	}

}