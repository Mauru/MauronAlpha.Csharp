using System;

namespace MauronAlpha.Events.Defaults {

	//Base class for the execution of an event
	public abstract class EventTrigger : MauronCode_subtype {
		public abstract string Name { get; }
		public static EventTrigger Nothing { get {
			return EventTrigger_nothing.Instance;
		} }
	}

	//A trigger that does nothing
	public sealed class EventTrigger_nothing : EventTrigger {
		#region singleton
		private static volatile EventTrigger_nothing instance=new EventTrigger_nothing();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static EventTrigger_nothing ( ) { }
		public static EventTrigger Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new EventTrigger_nothing();
					}
				}
				return instance;
			}
		}
		#endregion

		//the name
		public override string Name {
			get { return "nothing"; }
		}

		//The delegate check function (it always returns false)
		public delegate void Delegate_trigger (I_eventReceiver receiver, MauronCode_event e);
		public static MauronCode_event.Delegate_trigger Delegate {
			get {
				return Execute;
			}
		}
		public static void Execute (I_eventReceiver receiver, MauronCode_event e) {
			return;
		}
	}
}
