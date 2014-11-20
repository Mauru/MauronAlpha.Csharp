﻿using System;
using MauronAlpha.HandlingData;

using MauronAlpha.Events.Units;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events.Collections;

namespace MauronAlpha.Events {
    
	//A base class for a event handler
    public class EventHandler:MauronCode_eventComponent  {

        //constructor
        public EventHandler(I_eventHandler source):base() {
            Source = source;
        }
        public EventHandler(EventUnit_clock clock) : base() {
            CLOCK_events = clock;
        }

        private EventUnit_clock CLOCK_events;
		public EventUnit_clock Clock {
			get {
				return CLOCK_events;
			}
		}

        private EventSubscriberList Subscribers = new EventSubscriberList();
        public void Subscribe(string eventCode, I_eventSubscriber source) {
            if (Subscribers.ContainsKey(eventCode)) {
                foreach (EventUnit_subscription subscription in Subscribers.Value(eventCode)) { }
            }
        }
        public EventSubscriberList Subscriptions {
            get {
                return Subscribers.Instance.SetIsReadOnly(true);
            }
        }

        protected I_eventHandler Source;
		public bool HasSource {
			get { return Source != null; }
		}

        //Receive an event
        public delegate bool DELEGATE_ReceiveEvent(EventUnit_event e);
        //Send an event
        public delegate bool DELEGATE_SubmitEvent(EventUnit_event e);
        //The condition for a trigger to occure
        public delegate bool DELEGATE_condition(EventUnit_event e, I_eventSender sender);
        //The trigger to execute
        public delegate bool DELEGATE_trigger(EventUnit_event e);

        //Equality
        public bool Equals(EventHandler other) {
			if(HasSource!=other.HasSource)
				return false;
			if(HasSource)
				return Source.Equals(other.Source);	
			return Subscriptions.Equals(other.Subscriptions);
        }

		//Check if an event code matches a subscription
		public int CheckForTrigger(EventUnit_event e, I_eventSender sender, EventUnit_timeStamp timestamp){
			MauronCode_dataList<EventUnit_subscription> subscriptions = Subscriptions.ByCode(e.Code);
			foreach(EventUnit_subscription subscription in subscriptions) {
					return subscription.Condition (e, sender);
			}
		}
    }

}