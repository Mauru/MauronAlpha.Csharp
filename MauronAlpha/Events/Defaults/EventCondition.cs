using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.Events.Defaults {


	public abstract class EventCondition : MauronCode_subtype {
		public abstract string Name { get; }
	}

	//A event condition that is never triggered
	public sealed class EventCondition_never : EventCondition {
		#region singleton
		private static volatile EventCondition_never instance=new EventCondition_never();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static EventCondition_never ( ) { }
		public static EventCondition Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new EventCondition_never();
					}
				}
				return instance;
			}
		}
		#endregion

		//the name
		public override string Name {
			get { return "never"; }
		}

		//The delegate check function (it always returns false)
		public delegate bool Delegate_condition(I_eventReceiver receiver, MauronCode_event e);
		public static MauronCode_event.Delegate_condition Delegate { 
			get {
				return Check;
			}
		}
		public static bool Check(I_eventReceiver receiver, MauronCode_event e) {
			return false;
		}
	}

	//A event condition that is ALWAYS triggered
	public sealed class EventCondition_always : EventCondition {
		#region singleton
		private static volatile EventCondition_always instance=new EventCondition_always();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static EventCondition_always ( ) { }
		public static EventCondition Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new EventCondition_always();
					}
				}
				return instance;
			}
		}
		#endregion

		//the name
		public override string Name {
			get { return "always"; }
		}

		//The delegate check function (it always returns false)
		public delegate bool Delegate_condition (I_eventReceiver receiver, MauronCode_event e);
		public static MauronCode_event.Delegate_condition Delegate {
			get {
				return Check;
			}
		}
		public static bool Check (I_eventReceiver receiver, MauronCode_event e) {
			return true;
		}
	}
}