using System;

using MauronAlpha.Events.Units;
using MauronAlpha.Events.Shedules;
using MauronAlpha.Events.Utility;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Events {

	//A class keeping Time
	public class MauronCode_eventClock : MauronCode_eventComponent, IEquatable<MauronCode_eventClock> {


		//Is this clock the System Time
		public MauronCode_timeUnit SystemTime {
			get { return MauronAlpha.Events.SystemTime.Instance.Time; }
		}
		public virtual bool IsSystemTime { get { return false; } }
		public virtual bool IsExceptionClock {
			get { return false;	}
		}
		
		private EventUtility_synchronization UTILITY_synchronize;
		public EventUtility_synchronization SynchronizationHandler {
			get { return UTILITY_synchronize; }
		}
		public MauronCode_eventClock SYNCHRONIZE_fromTimeUnit(MauronCode_timeUnit time) {
			MauronCode_eventClock result = EventUtility_synchronization.SetClockFromTimeUnit(this,time,PrecisionHandler);
			return result;
		}

		private EventUtility_precision UTILITY_precision;
		public EventUtility_precision PrecisionHandler {
			get { return UTILITY_precision; }
		}

		private MauronCode_timeStamp TIME_created;
		public MauronCode_timeStamp Time_created {
			get {
				return TIME_created;
			}
		}

		//constructor
		protected MauronCode_eventClock():base() {
			TIME_created = MauronAlpha.Events.SystemTime.TimeStamp;
			if(!IsSystemTime) {
				throw Error("Only the systemTime can have a empty constructor!",this,ErrorType_constructor.Instance);
			}
			UTILITY_precision = new EventUtility_precision(EventPrecisionRuleSet.SystemTime);
			UTILITY_synchronize = new EventUtility_synchronization(UTILITY_precision);
		}
		public MauronCode_eventClock (EventUtility_synchronization synchronizationHandler, EventUtility_precision precision) : base() {
			TIME_created = MauronAlpha.Events.SystemTime.TimeStamp;
			UTILITY_precision=precision;
			UTILITY_synchronize=synchronizationHandler;	
		}
		public MauronCode_eventClock (MauronCode_eventClock clock) : this(clock.SynchronizationHandler, clock.PrecisionHandler) {
			TIME_created = clock.Time_created.Instance;
			SetMasterClock(clock);
		}

		//MasterClock
		private MauronCode_eventClock CLOCK_master;
		public MauronCode_eventClock MasterClock {
			get {
				if(CLOCK_master==null) {
					return MauronAlpha.Events.SystemTime.Instance;
				}
				return CLOCK_master;
			}
		}
		public MauronCode_eventClock SetMasterClock(MauronCode_eventClock clock) {
			CLOCK_master=clock;
			return this;
		}

		//Get the time
		private MauronCode_timeUnit TU_time; 
		public virtual MauronCode_timeUnit Time {
			get { 
				if(TU_time==null) {
					TU_time=new MauronCode_timeUnit(0, this);
				}
				return TU_time; 
			}
		}
		public MauronCode_eventClock SetTime(MauronCode_timeUnit time) {
			if(IsSytemTime) {
				throw Error("SystemTime is out of scope!,(SetTime)", this, ErrorType_scope.Instance);
			}
			TU_time=time;
			return this;
		}
		public MauronCode_eventClock SetTime (long n) {
			return SetTime(new MauronCode_timeUnit(n,this));
		}

		//Get a TimeStamp
		public virtual MauronCode_timeStamp TimeStamp { 
			get {
				return new MauronCode_timeStamp(this,Time);
			}
		}

		//Advance the internal Time by one
		public MauronCode_eventClock AdvanceTime() {
			if(IsSystemTime) {
				throw Error("SystemTime is out of scope!,(AdvanceTime)",this,ErrorType_scope.Instance);
			}
			SetTime(Time.Ticks+1);
			ExecuteShedules();
			return this;
		}

		//Event Subscriptions
		private MauronCode_dataRegistry<EventSubscription> DATA_subscriptions;
		public MauronCode_dataRegistry<EventSubscription> Subscriptions { 
			get { 
				if (DATA_subscriptions == null) {
					SetSubscriptions(new MauronCode_dataRegistry<EventSubscription>());
				}
				return DATA_subscriptions;
			}
		}
		private MauronCode_eventClock SetSubscriptions (MauronCode_dataRegistry<EventSubscription> subscriptions) {
			DATA_subscriptions=subscriptions;
			return this;
		}

		//Subscribe to a event
		public MauronCode_eventClock SubscribeToEvent(string message, I_eventReceiver receiver){
			EventSubscription s = new EventSubscription(this,message,receiver);
			Subscriptions.AddValue(message,s);
			return this;
		}

		//Register a event
		public MauronCode_eventClock SubmitEvent(MauronCode_event e){
			foreach (EventSubscription subscription in Subscriptions) {
				subscription.CheckAgainst (e);
			}
			return this;
		}

		//The Shedule Map <TimeUnit.Tick>
		private MauronCode_dataIndex<MauronCode_dataList<MauronCode_eventShedule>>  INDEX_shedules;
		public MauronCode_dataIndex<MauronCode_dataList<MauronCode_eventShedule>> EventShedulesByTick {
			get {
				if(INDEX_shedules==null){
					SetShedulesByTick(new MauronCode_dataIndex<MauronCode_dataList<MauronCode_eventShedule>>());
				}
				return INDEX_shedules;
			}
		}
		private MauronCode_eventClock SetShedulesByTick(MauronCode_dataIndex<MauronCode_dataList<MauronCode_eventShedule>> shedules){
			INDEX_shedules=shedules;
			return this;
		}

		//Get all applicable	
		public MauronCode_dataList<MauronCode_eventShedule> ShedulesByTick(long n){
			if(!EventShedulesByTick.ContainsKey(n)){
				EventShedulesByTick.SetValue(n,new MauronCode_dataList<MauronCode_eventShedule>());
			}
			return EventShedulesByTick.Value(n);
		}

		//Execute any event shedules for the current tick
		private MauronCode_eventClock ExecuteShedules(){
			foreach (MauronCode_eventShedule shedule in ShedulesByTick(Time.Ticks)) {
				//DO SOMETHING HERE
				if(shedule.Clock.Equals(this)){}

			}
			return this;
		}

		//The registered event shedules
		private MauronCode_dataList<MauronCode_eventShedule> DATA_eventShedules;
		public MauronCode_dataList<MauronCode_eventShedule> Shedules { 
			get {
				if (DATA_eventShedules == null) {
					SetShedules (new MauronCode_dataList<MauronCode_eventShedule>());
				}
				return DATA_eventShedules; 
			}
		}
		public MauronCode_eventClock SetShedules(MauronCode_dataList<MauronCode_eventShedule> shedules){
			DATA_eventShedules = shedules;
			return this;
		}

		public bool Equals(MauronCode_eventClock other) {
			return UTILITY_synchronize.EVENTCLOCK_Equals(this, other, UTILITY_precision);
		}

		bool IEquatable<MauronCode_eventClock>.Equals (MauronCode_eventClock other) {
			return Equals(other);
		}
	}
}