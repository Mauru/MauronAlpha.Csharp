using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Events.Singletons;


namespace MauronAlpha.Events.Units {
	
	//A TimeStamp of an Object
	public class EventUnit_timeStamp : EventComponent_unit {

		//DataTrees
		private static string[] KEYS_default = new string[]{"created","updated","instanced"};
		private MauronCode_dataTree<string, long> TREE_timeUnits=new MauronCode_dataTree<string, long>(KEYS_default);

		//constructor
		public EventUnit_timeStamp (EventUnit_clock clock, long ticks)
			: base() {
			CLOCK_event=clock;
			SetCreated(ticks);
		}
		private EventUnit_timeStamp (EventUnit_timeStamp instance) {
			CLOCK_event=instance.Clock;
			SetCreated(instance.Created.Ticks);
			SetUpdated(instance.Updated.Ticks);
			SetInstanced(SystemTime.Ticks);
		}
		public EventUnit_timeStamp(EventUnit_time time):this(time.Clock,time.Ticks) {}

		public EventUnit_timeStamp Instance {
			get {
				return new EventUnit_timeStamp(this);
			}
		}
		public EventUnit_time SystemTime {
			get {
				return Clock_systemTime.Time;
			}
		}

/*==========================================================================================
Properties
==========================================================================================*/

		//Return time as string
		public string AsString {
			get {
				return "{ EventUnit_timeStamp[ Ticks : " + TREE_timeUnits.Value("created") + "] }";
			}
		}

		//the time when this unit was created
		public EventUnit_time Created {
			get {
				return new EventUnit_time(Clock,TREE_timeUnits.Value("created"));
			}
		}
		private EventUnit_timeStamp SetCreated (long ticks) {
			TREE_timeUnits.SetValue("created", ticks);
			return this;
		}
		
		//the time when this unit was last updated
		public EventUnit_time Updated {
			get {
				if(!TREE_timeUnits.IsSet("updated") ) {
					return (IsInstance)? Instanced:Created;
				}
				return new EventUnit_time(Clock,TREE_timeUnits.Value("updated"));
			}
		}
		protected EventUnit_timeStamp SetUpdated(long ticks) {
			TREE_timeUnits.SetValue("updated", ticks);
			return this;
		}
		
		public EventUnit_time Instanced {
			get {
				if( !TREE_timeUnits.IsSet("instanced") ) {
					Exception("TIME_instanced not set!,(Instanced)",this,ErrorResolution.Create_unreliable);
					return Created;
				}
				return new EventUnit_time(Clock,TREE_timeUnits.Value("instanced"));
			}
		}
		public bool IsInstance {
			get { return TREE_timeUnits.IsSet("instanced"); }
		}
		private EventUnit_timeStamp SetInstanced(long ticks){
			TREE_timeUnits.SetValue("instanced", ticks);
			return this;
		}

		private EventUnit_clock CLOCK_event;
		public EventUnit_clock Clock {
			get {
				if(CLOCK_event == null) {
					CLOCK_event= new EventUnit_clock();
				}	
				return CLOCK_event;
			}
		}

	}
}