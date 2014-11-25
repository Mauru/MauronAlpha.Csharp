using MauronAlpha.Events.Interfaces;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.Units {

	/// <summary>
	/// Describes how a EventSubscription is handled
	/// </summary>
	public abstract	class EventUnit_subscriptionModel:EventComponent_unit, I_eventSubscriptionModel  {
		
		protected long INT_repeats = -1;
		public long ExecutionLimit {
			get { return INT_repeats; }
		}

		protected long INT_interval=1;
		public long Interval {
			get { throw new System.NotImplementedException(); }
		}

		protected I_eventHandler RELAY_generic;
		public I_eventHandler EventRelay_Generic {
			get { 
				if(RELAY_generic == null) {
					throw NullError("EventRelay not set!,(EventRelay_generic)",this,typeof(I_eventHandler));
				}
				return RELAY_generic;
			}
		}

		protected I_eventHandler RELAY_onComplete;
		public I_eventHandler EventRelay_OnComplete {
			get {
				if( RELAY_onComplete==null ) {
					throw NullError("EventRelay not set!,(EventRelay_OnComplete)", this, typeof(I_eventHandler));
				}
				return RELAY_onComplete;
			}
		}

		public virtual bool IsContinous {
			get { return true; }
		}
		public virtual bool IsSingle {
			get { return false; }
		}
		public virtual bool IsRelay {
			get { return false; }
		}
		public virtual bool UsesExecutionLimit {
			get { return false; }
		}
		public virtual bool UsesInterval {
			get { return false; }
		}
		public virtual bool UsesCondition {
			get { return false; }
		}
		public virtual bool UsesTrigger {
			get { return false; }
		}
		public virtual bool UsesRelay {
			get { return false; }
		}

		public virtual bool TriggerEventOnComplete {
			get { return false; }
		}
		public virtual bool ResetOnComplete {
			get { return false; }
		}

		public virtual string[] BooleanKeys { 
			get { 
				return new string[]{
					"IsContinous",
					"IsSingle",
					"IsRelay",
					"UsesRepeats",
					"UsesInterval",
					"UsesCondition",
					"UsesTrigger",
					"UsesRelay",
					"TriggerEventOnComplete",
					"ResetOnComplete"
				};
			}
		}
		public virtual bool[] BooleanStates {
			get {
				return new bool[]{
					IsContinous,
					IsSingle,
					IsRelay,
					UsesExecutionLimit,
					UsesInterval,
					UsesCondition,
					UsesTrigger,
					UsesRelay,
					TriggerEventOnComplete,
					ResetOnComplete
				};
			}
		}

		public virtual bool Equals (I_eventSubscriptionModel other) {

			MauronCode_dataTree<string, bool> source=AsTree_booleanStates;
			MauronCode_dataTree<string, bool> candidate=other.AsTree_booleanStates;

			if(!source.Equals(other))
				return false;			
			if(UsesInterval && other.Interval != Interval)
				return false;
			if( UsesExecutionLimit&&other.ExecutionLimit!=ExecutionLimit )
				return false;
			if(UsesRelay && !other.EventRelay_Generic.Equals(EventRelay_Generic))
				return false;
			if(TriggerEventOnComplete && !other.EventRelay_OnComplete.Equals(EventRelay_OnComplete))
				return false;

			return true;			
		}

		public virtual MauronCode_dataTree<string, bool> AsTree_booleanStates {
			get {
				MauronCode_dataTree<string, bool> result=new MauronCode_dataTree<string, bool>(BooleanKeys, BooleanStates);
				return result;
			}
		}

	}

}
