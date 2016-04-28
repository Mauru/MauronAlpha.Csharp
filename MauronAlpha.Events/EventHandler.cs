using System;
using MauronAlpha.HandlingData;

using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events.Collections;

namespace MauronAlpha.Events {
	
	//A base class for a event handler
	public class EventHandler:MauronCode_eventComponent, I_eventHandler  {

		//constructor
		public EventHandler( ) : this( new EventUnit_clock() ) {}
		public EventHandler(I_eventHandler source):base() {
			Source = source;
		}
		public EventHandler(EventUnit_clock clock) : base() {
			CLOCK_events = clock;
		}

		private EventUnit_clock CLOCK_events;
		public EventUnit_clock Clock {
			get {
				if(CLOCK_events == null) {
					if(HasSource)
						return Source.Clock;
					throw NullError("No EventClock and no Source Handler!,(Clock)",this,typeof(EventUnit_clock));
				}
				return CLOCK_events;
			}
		}

		private EventSubscriberList Subscribers = new EventSubscriberList();
		public void SubscribeToCode( string eventCode, I_eventSubscriber source, I_eventSubscriptionModel model ) {
			Subscribers.RegisterByCode(eventCode, source, model);
			return;
		}
		public void SubscribeToCode(string eventCode, I_eventSubscriber source) {
			Subscribers.RegisterByCode(eventCode, source, EventModels.Continous);
			return;
		}
		public EventSubscriberList Subscriptions {
			get {
				return Subscribers.Instance.SetIsReadOnly(true);
			}
		}

		protected I_eventHandler Source;
		
		//Booleans
		public bool Equals (EventHandler other) {
			if( HasSource!=other.HasSource )
				return false;
			if( HasSource )
				return Source.Equals(other.Source);
			return Subscriptions.Equals(other.Subscriptions);
		}
		public bool HasSource {
			get { return Source != null; }
		}

		//Check if an event code matches a subscription : Return the number of Processed Receivers
		public long CheckForTrigger (EventUnit_event e, I_eventSender sender, EventUnit_timeStamp timestamp) {
			MauronCode_dataList<EventUnit_subscription> subscriptions = Subscriptions.ByCode(e.Code);

			long count_processed = 0;
			bool result = true;

			MauronCode_dataList<EventUnit_subscription> unsubribe_these = new MauronCode_dataList<EventUnit_subscription>();

			foreach( EventUnit_subscription subscription in subscriptions ) {

				result = subscription.Subscriber.ReceiveEvent(e);
				if(result)
					count_processed++;


				//Does the subscription only receive the check once, regardLess of the result of condition
				if( subscription.SubscriptionModel.IsSingle )
					unsubribe_these.Add(subscription);

				//Can the subscription only be checked N times ?
				if( subscription.SubscriptionModel.UsesExecutionLimit ) {
					subscription.ItterateExecutionCount(timestamp);

					if( subscription.SubscriptionModel.ExecutionLimit<=subscription.ExecutionCount )
						unsubribe_these.Add(subscription);
				}

			}

			//Check for any Parent EventListeners and pass the event on
			if( HasSource ) {

				long processed_source = Source.CheckForTrigger(e, sender, timestamp);
				count_processed += processed_source;

			}

			return count_processed;
		}

		public bool DoNothing(EventUnit_event e) {
			 return false;
		}
		public DELEGATE_trigger NothingHappens {
			get { return DoNothing; }
		}

		//Boolean Delegate : Receive an event
		public delegate bool DELEGATE_ReceiveEvent(EventUnit_event e);
		//Boolean Delegate : Send an event
		public delegate bool DELEGATE_SubmitEvent(EventUnit_event e);
		
		//Boolean Delegate : The condition for a trigger to occure
		public delegate bool DELEGATE_condition(EventUnit_event e, EventUnit_subscription subscription);
		//Boolean Delegate : The trigger to execute
		public delegate bool DELEGATE_trigger(EventUnit_event e);

		public EventUnit_event GenerateEvent(string code, I_eventSender sender) {
			EventUnit_event e = new EventUnit_event(sender, code);
			return e;
		}

		public EventHandler SubmitEvent(EventUnit_event e, I_eventSender sender) {
			CheckForTrigger(e, sender, SystemClock.TimeStamp);
			return this;
		}

		public EventUnit_timeStamp TimeStamp {
			get {
				if(!HasSource)
					return Clock.TimeStamp;
				return Source.TimeStamp;
			}
		}

		bool I_eventHandler.Equals (I_eventHandler other) {
			return Equals(other);
		}

		EventUnit_timeStamp I_eventHandler.TimeStamp {
			get { return TimeStamp; }
		}
	
	}

}