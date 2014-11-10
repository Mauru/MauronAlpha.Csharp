using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Events.Singletons;


namespace MauronAlpha.Events.Units {
	
	//A TimeStamp of an Object
	public class MauronCode_timeStamp : EventComponent_unit {

		//DataTrees
		private static string[] KEYS_default = new string[]{"created","updated","instanced"};
		private MauronCode_dataTree<string,MauronCode_timeUnit> TREE_timeUnits = new MauronCode_dataTree<string,MauronCode_timeUnit>(KEYS_default);

		public Clock_systemTime SystemTime {
			get {
				return SharedEventSystem.Instance.SystemTime;
			}
		}

/*==========================================================================================
Properties
==========================================================================================*/

		//the time when this unit was created
		public MauronCode_timeUnit Created {
			get {
				return TREE_timeUnits.Value("created");
			}
		}
		private MauronCode_timeStamp SetCreated (MauronCode_timeUnit time) {
			TREE_timeUnits.SetValue("created", time);
			return this;
		}
		
		//the time when this unit was last updated
		public MauronCode_timeUnit Updated {
			get {
				if(!TREE_timeUnits.IsSet("updated") ) {
					return (IsInstance)? Instanced:Created;
				}
				return TREE_timeUnits.Value("updated");
			}
		}
		protected MauronCode_timeStamp SetUpdated(MauronCode_timeUnit time) {
			TREE_timeUnits.SetValue("updated",time);
			return this;
		}
		
		public MauronCode_timeUnit Instanced {
			get {
				if( !TREE_timeUnits.IsSet("instanced") ) {
					Exception("TIME_instanced not set!,(Instanced)",this,ErrorResolution.Create_unreliable);
					return Created;
				}
				return TREE_timeUnits.Value("instanced");
			}
		}
		public bool IsInstance {
			get { return TREE_timeUnits.IsSet("instanced"); }
		}
		private MauronCode_timeStamp SetInstanced(MauronCode_timeUnit time){
			TREE_timeUnits.SetValue("instanced",time);
			return this;
		}

		private MauronCode_eventClock CLOCK_event;
		public MauronCode_eventClock Clock {
			get {
				if(CLOCK_event == null) {
					CLOCK_event= new MauronCode_eventClock(true);
				}	
				return CLOCK_event;
			}
		}

		//constructor
		public MauronCode_timeStamp(MauronCode_eventClock clock, MauronCode_timeUnit time) : base() {
			CLOCK_event = clock;
			SetCreated(time);
		}
		private MauronCode_timeStamp(MauronCode_timeStamp instance) {
			CLOCK_event = instance.Clock;
			SetCreated(instance.Created);
			SetUpdated(instance.Updated);
			SetInstanced(SystemTime.Time);
		}

		public MauronCode_timeStamp Instance {
			get {
				return new MauronCode_timeStamp(this);
			}
		}
	
	}
}