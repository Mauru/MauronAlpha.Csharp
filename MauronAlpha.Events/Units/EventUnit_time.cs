using System;
using MauronAlpha.Mathematics;
using MauronAlpha.ExplainingCode;

namespace MauronAlpha.Events.Units {
	
	//A unit of time
	public class EventUnit_time: EventComponent_unit,
		IComparable<EventUnit_time>,
		IComparable<long>,
		IEquatable<EventUnit_time>,
		IEquatable<long>,
		I_mathComponent
	{
		
		#region invalid constructors, kept for explanatory sake
			/*public MauronCode_timeUnit():base(CodeType_timeUnit.Instance) {}
			internal MauronCode_timeUnit (long ticks) : base(CodeType_timeUnit.Instance) { 
				SetTicks(ticks);
			}*/
		#endregion
		
		//Feature Complete constructors
		protected EventUnit_time():base() {
		
		}
		public EventUnit_time(long ticks, EventUnit_clock clock):base(){
			SetTicks(ticks);
			SetClock(clock);
		}
		

		//clonable
		public EventUnit_time (EventUnit_time tu)
			: base() {
			SetTicks(tu.Ticks);
			SetClock(tu.Clock);
		}

		//The clock
		private EventUnit_clock EC_clock;
		public EventUnit_clock Clock {
			get {
				if(EC_clock==null) {
					throw NullError("Clock can not be null!",this,typeof(EventUnit_clock));
				}
				return EC_clock;
			}
		}
		public EventUnit_time SetClock(EventUnit_clock clock){
			EC_clock=clock;
			return this;
		}

		//Manual Ticks
		private long INT_ticks=0;
		public long Ticks { 
			get {
				return INT_ticks;
			}
		}
		public EventUnit_time SetTicks(long ticks) {
			INT_ticks=ticks;
			return this;
		}

		//Create a timestamp
		public EventUnit_timeStamp TimeStamp {
			get {
				return new EventUnit_timeStamp(Clock,this);
			}
		}

		//Advance the timeunit one step
		public EventUnit_time Tick() {
			INT_ticks++;
			return this;
		}

		#region IComparable < {timeunit,long] >
		public int CompareTo (EventUnit_time other) {
			if(other.Ticks==this.Ticks) return 0; 
			if(other.Ticks>this.Ticks) return -1;
			return 1;
		}
		public int CompareTo (long other) {
			if( other==this.Ticks )	return 0;
			if( other>this.Ticks ) return -1;
			return 1;
		}
		#endregion

		#region IEquatable < {timeunit,long} >
		public bool Equals (EventUnit_time other) {
			return other.Ticks==this.Ticks;
		}
		public bool Equals (long other) {
			return other==this.Ticks;
		}
		#endregion

		#region I_mathComponent < {timeunit,long} >
		//Add
		public I_mathComponent Add (long n) {
			SetTicks(Ticks+n);
			return this;
		}
		public EventUnit_time Add (EventUnit_time tu) {
			return (EventUnit_time) Add(tu.Ticks);
		}
		//Subtract
		public I_mathComponent Subtract (long n) {
			SetTicks(Ticks-n);
			return this;
		}
		public EventUnit_time Subtract (EventUnit_time tu) {
			return (EventUnit_time) Subtract(tu.Ticks);
		}
		//Multiply
		public I_mathComponent Multiply (long n) {
			SetTicks(Ticks*n);
			return this;
		}
		public EventUnit_time Multiply (EventUnit_time tu) {
			return (EventUnit_time) Multiply(tu.Ticks);
		}
		//Divide
		public I_mathComponent Divide (long n) {
			SetTicks(Ticks/n);
			return this;
		}
		public EventUnit_time Divide (EventUnit_time tu) {
			return (EventUnit_time) Divide(tu.Ticks);
		}
		//Smaller or Equal
		public bool SmallerOrEqual (long n) {
			return n<=Ticks;
		}
		public bool SmallerOrEqual (EventUnit_time tu) {
			return SmallerOrEqual(tu.Ticks);
		}
		//Larger or Equal
		public bool LargerOrEqual (long n) {
		 return n>=Ticks;
		}
		public bool LargerOrEqual (EventUnit_time tu) {
			return LargerOrEqual(tu.Ticks);
		}
		#endregion

		#region I_Instantiable
		//Instantiate
		public EventUnit_time Instance {
			get {
				return new EventUnit_time(this);
			}
		}
		public object Clone ( ) {
			return Instance;
		}
		#endregion

	}

	//A class that contains data
	public sealed class CodeType_timeUnit : CodeType {
		#region singleton
		private static volatile CodeType_timeUnit instance=new CodeType_timeUnit();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static CodeType_timeUnit ( ) { }
		public static CodeType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new CodeType_timeUnit();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "timeUnit"; } }

	}
}