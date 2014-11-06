using System;

using MauronAlpha.Events.Units;
using MauronAlpha.Events.Shedules;
using MauronAlpha.Events.Utility;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Events {

	//A class keeping Time
	public class MauronCode_eventClock : MauronCode_eventComponent, I_protectable , IEquatable<MauronCode_eventClock> {

		//Is this clock the System Time
		public MauronCode_timeUnit SystemTime {
			get { return MauronAlpha.Events.SystemTime.Instance.Time; }
		}

		#region Event Precision
		private EventUtility_precision UTILITY_precision;
		public EventUtility_precision PrecisionHandler {
			get { return UTILITY_precision; }
		}
		#endregion

		#region Stores a SystemTimeStamp of when the eventclock was created
		private MauronCode_timeStamp TIME_created;
		public MauronCode_timeStamp Time_created {
			get {
				return TIME_created;
			}
		}
		private MauronCode_eventClock SetTime_created (MauronCode_timeStamp time) {
			TIME_created=time;
			return this;
		}
		#endregion

		//constructor
		protected MauronCode_eventClock() : base() {
			TIME_created = MauronAlpha.Events.SystemTime.TimeStamp;

			if(!IsSystemTime) {
				throw Error("Only the systemTime can have a empty constructor!",this,ErrorType_constructor.Instance);
			}
			UTILITY_precision = new EventUtility_precision(EventPrecisionRuleSet.SystemTime);
		}
		public MauronCode_eventClock (EventUtility_precision precision) : base() {
			TIME_created = MauronAlpha.Events.SystemTime.TimeStamp;

			UTILITY_precision=precision;
		}
		public MauronCode_eventClock (MauronCode_eventClock clock) : this(clock.PrecisionHandler) {
			TIME_created = clock.Time_created.Instance;

			SetMasterClock(clock);
		}

		#region MasterClock (Usually SystemTime)
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
		#endregion

		#region Get The Time
		//As TimeUnit
		private MauronCode_timeUnit TU_time; 
		public virtual MauronCode_timeUnit Time {
			get { 
				if(TU_time==null) {
					TU_time=new MauronCode_timeUnit(0, this);
				}
				return TU_time; 
			}
		}

		//As TimeStamp
		public virtual MauronCode_timeStamp TimeStamp {
			get {
				return new MauronCode_timeStamp(this, Time);
			}
		}
		#endregion

		#region Modify The Time

		//Set the Time
		public MauronCode_eventClock SetTime(MauronCode_timeUnit time) {
			if(IsSystemTime) {
				throw Error("SystemTime is out of scope!,(SetTime)", this, ErrorType_scope.Instance);
			}
			if( IsReadOnly ) {
				throw Error("IsProtected!,(AdvanceTime)", this, ErrorType_protected.Instance);
			}
			TU_time=time;
			return this;
		}
		public MauronCode_eventClock SetTime (long n) {
			return SetTime(new MauronCode_timeUnit(n,this));
		}

		//Advance the internal Time by one
		public MauronCode_eventClock AdvanceTime ( ) {
			SetTime(Time.Ticks+1);

			//ExecuteShedules();
			return this;
		}
		
		#endregion

		#region Subscriptions
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
		#endregion

		#region Shedules
		//The Shedule Map <TimeUnit.Tick>
		private MauronCode_dataIndex<MauronCode_dataList<MauronCode_eventShedule>> INDEX_shedules;
		public MauronCode_dataIndex<MauronCode_dataList<MauronCode_eventShedule>> EventShedulesByTick {
			get {
				if( INDEX_shedules==null ) {
					SetShedulesByTick(new MauronCode_dataIndex<MauronCode_dataList<MauronCode_eventShedule>>());
				}
				return INDEX_shedules;
			}
		}
		private MauronCode_eventClock SetShedulesByTick (MauronCode_dataIndex<MauronCode_dataList<MauronCode_eventShedule>> shedules) {
			INDEX_shedules=shedules;
			return this;
		}

		//Get all applicable	
		public MauronCode_dataList<MauronCode_eventShedule> ShedulesByTick (long n) {
			if( !EventShedulesByTick.ContainsKey(n) ) {
				EventShedulesByTick.SetValue(n, new MauronCode_dataList<MauronCode_eventShedule>());
			}
			return EventShedulesByTick.Value(n);
		}

		//Execute any event shedules for the current tick
		private MauronCode_eventClock ExecuteShedules ( ) {
			foreach( MauronCode_eventShedule shedule in ShedulesByTick(Time.Ticks) ) {
				//DO SOMETHING HERE
				if( shedule.Clock.Equals(this) ) { }

			}
			return this;
		}

		//The registered event shedules
		private MauronCode_dataList<MauronCode_eventShedule> DATA_eventShedules;
		public MauronCode_dataList<MauronCode_eventShedule> Shedules {
			get {
				if( DATA_eventShedules==null ) {
					SetShedules(new MauronCode_dataList<MauronCode_eventShedule>());
				}
				return DATA_eventShedules;
			}
		}
		public MauronCode_eventClock SetShedules (MauronCode_dataList<MauronCode_eventShedule> shedules) {
			DATA_eventShedules=shedules;
			return this;
		}
		#endregion


		//Trigger an event at the time of the clock
		public MauronCode_eventClock SubmitEvent(MauronCode_event e){
			foreach (EventSubscription subscription in Subscriptions) {
				subscription.CheckAgainst (e);
			}
			return this;
		}

		#region Equality Checks, Boolean Interaction

		#region SystemClock, ExceptionClock
		public virtual bool IsSystemTime { get { return false; } }
		public virtual bool IsExceptionClock {
			get { return false; }
		}
		#endregion

		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		protected MauronCode_eventClock SetIsReadOnly(bool state) {
			B_isReadOnly = state;
			return this;
		}

		#region Equality, I_Equatable<T>	
		public bool Equals(MauronCode_eventClock other) {
			return EQUALS(this, other, UTILITY_precision);
		}
		
		public static bool EQUALS_TimeCreated (MauronCode_eventClock source, MauronCode_eventClock other, EventUtility_precision precisionHandler) {
			return precisionHandler.EQUALS_long(
				source.Time_created.Created.Ticks,
				other.Time_created.Created.Ticks
			);
		}
		public static bool EQUALS_Ticks (MauronCode_eventClock source, MauronCode_eventClock other, EventUtility_precision precisionHandler) {
			return precisionHandler.EQUALS_long(
				source.Time.Ticks,
				other.Time.Ticks
			);
		}
		public static bool EQUALS (MauronCode_eventClock source, MauronCode_eventClock other, EventUtility_precision precisionHandler) {

			//create a instance of this handler...
			///<remarks> ...just in case we need to share the following complex information mid-calculation (you never know)</remarks>
			EventUtility_synchronization handler=new EventUtility_synchronization(precisionHandler);

			//We are dealing with exceptions or system time - these are shortcuts
			if( source.IsSystemTime	&& other.IsSystemTime ) {
				#region Try to find out if we are dealing with an exception-clock [return true|false]
				if( source.IsExceptionClock==other.IsExceptionClock ){
					Exception("Comparing two ExceptionClocks... Could create an infinite loop!!,(EVENTCLOCK_Equals)",
					source, ErrorResolution.Delayed);
					return true;
				}

				//Either is an exception clock, the other is SystemTime
				if(	source.IsExceptionClock	|| other.IsExceptionClock )	return false;

				#endregion
			}

			//Compare creation-time of the EventClock and ticks
			if(	!EQUALS_Ticks(source, other, precisionHandler)
				&&!EQUALS_TimeCreated(source, other, precisionHandler)
			)
			return false;

			return true;
		}

		bool IEquatable<MauronCode_eventClock>.Equals (MauronCode_eventClock other) {
			return Equals(other);
		}
		#endregion
	
		#endregion
	}
}