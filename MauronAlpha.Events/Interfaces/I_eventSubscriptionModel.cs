using MauronAlpha.HandlingData;

namespace MauronAlpha.Events.Interfaces {
	
	/// <summary>
	/// Describes an Event Subscription Setup
	/// </summary>	
	public interface I_eventSubscriptionModel {
		
		//how often is the subscription checked until it is discarded
		long ExecutionLimit { get; }

		//how long is the interval between checks?
		long Interval { get; }
		
		//Generic EventRelay we might pass things to
		I_eventHandler EventRelay_Generic { get; }
		
		//EventRelay we might use on completion of executeRepeats (when done)
		I_eventHandler EventRelay_OnComplete { get; }

		MauronCode_dataTree<string,bool> AsTree_booleanStates { get; }

		//Do we ignore repeats? i.e.: Do we check every cycle?
		bool IsContinous { get; }
		
		//Is the EventSubscription discarded on Trigger:true?
		bool IsSingle { get; }
		
		//Should the EventSubscription be handled like a Relay?
		bool IsRelay { get; }
		
		//Is Repeats > 0?
		bool UsesExecutionLimit { get; }
		
		//Is Interval > -1?
		bool UsesInterval { get; }
		
		//Does the EventSubscription have a condition set?
		bool UsesCondition { get; }

		//Does the EventSubscription have a Tigger set?
		bool UsesTrigger { get; }

		//Does the EventSubScription have a Relay set?
		bool UsesRelay { get; }

		

		//Does the Subscription Model trigger another Event on Executions>=Repeats?
		bool TriggerEventOnComplete { get; }
		
		//Does the Subscription Model reset itself on Executions>=Repeats?
		bool ResetOnComplete { get; }



		//Compare one Subscription Model with another
		bool Equals (I_eventSubscriptionModel other);

	}

}