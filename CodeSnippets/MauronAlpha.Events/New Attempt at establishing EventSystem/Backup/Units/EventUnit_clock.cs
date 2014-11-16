using System;

using MauronAlpha.Events.Units;
using MauronAlpha.Events.Shedules;
using MauronAlpha.Events.Utility;
using MauronAlpha.Events.Singletons;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Events {

	//A class keeping Time
	public class EventUnit_clock : EventComponent_unit, I_protectable , IEquatable<EventUnit_clock> {

		//constructors
		public EventUnit_clock (string name ) : base() {
            TIME_created = new EventUnit_timeStamp(SystemClock.Time);
            
            STR_name = name;

			UTILITY_precision = new EventUtility_precision(EventPrecisionRuleSet.SystemTime);

			B_isSystemTime=true;
		}

        public static EventUnit_clock SystemClock {
            get {
                return SharedEventSystem.Instance.SystemClock;
            }
        }

        //This is the identifier of the clock
        private string STR_name;
        public string Name {
            get {
                return STR_name;
            }
        }

		//Does the depend on another clock?
		private EventUnit_clock CLOCK_master;
		public EventUnit_clock MasterClock {
			get {
				if( CLOCK_master==null ) {
					throw NullError("MasterClock can not be null!,(MasterClock)", this, typeof(EventUnit_clock));
				}
				return CLOCK_master;
			}
		}
		public bool HasMasterClock {
			get {
				return CLOCK_master == null;
			}
		}

		//Get the system-time
		public EventUnit_time SystemTime {
			get { return Clock_systemTime.Time; }
		}

		#region Event Precision
		private EventUtility_precision UTILITY_precision;
		public EventUtility_precision PrecisionHandler {
			get { return UTILITY_precision; }
		}
		#endregion

		#region Stores when this clock was created
		private long TICKS_created = 0;
		public EventUnit_timeStamp Time_created {
			get {
				return new EventUnit_timeStamp(this,TICKS_created);
			}
		}
		#endregion

		#region Get The Time
		//As TimeUnit
		private EventUnit_time TU_time; 
		public virtual EventUnit_time Time {
			get { 
				if(IsSystemTime) {
					return new EventUnit_time(this,Clock_systemTime.Ticks);
				}
				if(TU_time==null) {
					TU_time=new EventUnit_time(this,0);
				}
				return TU_time; 
			}
		}
		
		//As TimeStamp
		public virtual EventUnit_timeStamp TimeStamp {
			get {
				return new EventUnit_timeStamp(this, Time.Ticks);
			}
		}
		#endregion

		#region Modify The Time

		//Set the Time
		public EventUnit_clock SetTime(EventUnit_time time) {
			if(IsSystemTime) {
				throw Error("SystemTime is out of scope!,(SetTime)", this, ErrorType_scope.Instance);
			}
			if( IsReadOnly ) {
				throw Error("IsProtected!,(AdvanceTime)", this, ErrorType_protected.Instance);
			}
			TU_time=time;
			return this;
		}
		public EventUnit_clock SetTime (long n) {
			return SetTime(new EventUnit_time(this,n));
		}

		//Advance the internal Time by one
		public EventUnit_clock AdvanceTime ( ) {
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
		private EventUnit_clock SetSubscriptions (MauronCode_dataRegistry<EventSubscription> subscriptions) {
			DATA_subscriptions=subscriptions;
			return this;
		}

		//Subscribe to a event
		public EventUnit_clock SubscribeToEvent(string message, I_eventReceiver receiver){
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
		private EventUnit_clock SetShedulesByTick (MauronCode_dataIndex<MauronCode_dataList<MauronCode_eventShedule>> shedules) {
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
		private EventUnit_clock ExecuteShedules ( ) {
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
		public EventUnit_clock SetShedules (MauronCode_dataList<MauronCode_eventShedule> shedules) {
			DATA_eventShedules=shedules;
			return this;
		}
		#endregion

		//Trigger an event at the time of the clock
		public EventUnit_clock SubmitEvent(MauronCode_event e){
			foreach (EventSubscription subscription in Subscriptions) {
				subscription.CheckAgainst (e);
			}
			return this;
		}

		#region Equality Checks, Boolean Interaction

		#region STATIC Equality Checks
		public static bool EQUALS_TimeCreated (EventUnit_clock source, EventUnit_clock other, EventUtility_precision precisionHandler) {
			return precisionHandler.EQUALS_long(
				source.Time_created.Created.Ticks,
				other.Time_created.Created.Ticks
			);
		}
		public static bool EQUALS_Ticks (EventUnit_clock source, EventUnit_clock other, EventUtility_precision precisionHandler) {
			return precisionHandler.EQUALS_long(
				source.Time.Ticks,
				other.Time.Ticks
			);
		}
		public static bool EQUALS (EventUnit_clock source, EventUnit_clock other, EventUtility_precision precisionHandler) {

			//create a instance of this handler...
			///<remarks> ...just in case we need to share the following complex information mid-calculation (you never know)</remarks>
			EventUtility_synchronization handler=new EventUtility_synchronization(precisionHandler);

			//We are dealing with exceptions or system time - these are shortcuts
			if( source.IsSystemTime&&other.IsSystemTime ) {
				#region Try to find out if we are dealing with an exception-clock [return true|false]
				if( source.IsExceptionClock==other.IsExceptionClock ) {
					Exception("Comparing two ExceptionClocks... Could create an infinite loop!!,(EVENTCLOCK_Equals)",
					source, ErrorResolution.Delayed);
					return true;
				}

				//Either is an exception clock, the other is SystemTime
				if( source.IsExceptionClock||other.IsExceptionClock )
					return false;

				#endregion
			}

			//Compare creation-time of the EventClock and ticks
			if( !EQUALS_Ticks(source, other, precisionHandler)
				&&!EQUALS_TimeCreated(source, other, precisionHandler)
			)
				return false;

			return true;
		}
		#endregion

		#region SystemClock, ExceptionClock
		private bool B_isSystemTime = false;
		public virtual bool IsSystemTime { get { return B_isSystemTime; } }
		
		private bool B_isExceptionHandler = false;
		public virtual bool IsExceptionClock {
			get { return B_isExceptionHandler; }
		}
		#endregion

		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		protected EventUnit_clock SetIsReadOnly(bool state) {
			B_isReadOnly = state;
			return this;
		}

		#region Equality, I_Equatable<T>	
		public bool Equals(EventUnit_clock other) {
			return EQUALS(this, other, UTILITY_precision);
		}
		bool IEquatable<EventUnit_clock>.Equals (EventUnit_clock other) {
			return Equals(other);
		}
		#endregion
	
		#endregion
	}

}