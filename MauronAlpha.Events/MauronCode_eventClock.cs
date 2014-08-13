using System;
using MauronAlpha.ExplainingCode;
using MauronAlpha.Events.Units;
using MauronAlpha.HandlingData;

namespace MauronAlpha.Events {

	//A class keeping Time
	public class MauronCode_eventClock:MauronCode {

		//Is this clock the System Time
		public MauronCode_timeUnit SystemTime {
			get { return MauronAlpha.Events.SystemTime.Instance.Time; }
		}
		public virtual bool IsSytemTime { get { return false; } }
		
		//constructor
		public MauronCode_eventClock():base(CodeType_eventClock.Instance) {}
		public MauronCode_eventClock (MauronCode_eventClock clock) : this() { 
			SetMasterClock(clock);
		}

		//MasterClock
		private MauronCode_eventClock EC_masterClock;
		public MauronCode_eventClock MasterClock {
			get {
				if(EC_masterClock==null) {
					return MauronAlpha.Events.SystemTime.Instance;
				}
				return EC_masterClock;
			}
		}
		public MauronCode_eventClock SetMasterClock(MauronCode_eventClock clock) {
			EC_masterClock=clock;
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
				Error("Can not set System Time",this);
			}
			TU_time=time;
			return this;
		}
		public MauronCode_eventClock SetTime (long n) {
			return SetTime(new MauronCode_timeUnit(n,this));
		}

		//Advance the internal Time by one
		public MauronCode_eventClock AdvanceTime() {
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

	}

	//Code Description
	public sealed class CodeType_eventClock:CodeType {
		#region singleton
		private static volatile CodeType_eventClock instance=new CodeType_eventClock();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_eventClock ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_eventClock();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "eventClock"; } }
	}
}
