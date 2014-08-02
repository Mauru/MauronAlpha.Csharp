using System;
using MauronAlpha.Mathematics;

namespace MauronAlpha.Events {
	
	//A unit of time
	public class MauronCode_timeUnit: MauronCode,
		IComparable<MauronCode_timeUnit>,
		IComparable<long>,
		IEquatable<MauronCode_timeUnit>,
		IEquatable<long>,
		I_mathComponent
	{
		
		#region invalid constructors, kept for explanatory sake
			/*public MauronCode_timeUnit():base(CodeType_timeUnit.Instance) {}
			public MauronCode_timeUnit (long ticks) : base(CodeType_timeUnit.Instance) { 
				SetTicks(ticks);
			}*/
		#endregion
		
		//Feature Complete constructors
		public MauronCode_timeUnit(long ticks, MauronCode_eventClock clock):base(CodeType_timeUnit.Instance){
			SetTicks(ticks);
			SetClock(clock);
		}
		//clonable
		public MauronCode_timeUnit (MauronCode_timeUnit tu)
			: base(CodeType_timeUnit.Instance) {
			SetTicks(tu.Ticks);
			SetClock(tu.Clock);
		}

		//The clock
		private MauronCode_eventClock EC_clock;
		public MauronCode_eventClock Clock {
			get {
				if(EC_clock==null) {
					Error("Clock can't be null!",this);
				}
				return EC_clock;
			}
		}
		public MauronCode_timeUnit SetClock(MauronCode_eventClock clock){
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
		public MauronCode_timeUnit SetTicks(long ticks) {
			INT_ticks=ticks;
			return this;
		}

		//Advance the timeunit one step
		public MauronCode_timeUnit Tick() {
			INT_ticks++;
			return this;
		}

		#region IComparable < {timeunit,long] >
		public int CompareTo (MauronCode_timeUnit other) {
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
		public bool Equals (MauronCode_timeUnit other) {
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
		public MauronCode_timeUnit Add (MauronCode_timeUnit tu) {
			return (MauronCode_timeUnit) Add(tu.Ticks);
		}
		//Subtract
		public I_mathComponent Subtract (long n) {
			SetTicks(Ticks-n);
			return this;
		}
		public MauronCode_timeUnit Subtract (MauronCode_timeUnit tu) {
			return (MauronCode_timeUnit) Subtract(tu.Ticks);
		}
		//Multiply
		public I_mathComponent Multiply (long n) {
			SetTicks(Ticks*n);
			return this;
		}
		public MauronCode_timeUnit Multiply (MauronCode_timeUnit tu) {
			return (MauronCode_timeUnit) Multiply(tu.Ticks);
		}
		//Divide
		public I_mathComponent Divide (long n) {
			SetTicks(Ticks/n);
			return this;
		}
		public MauronCode_timeUnit Divide (MauronCode_timeUnit tu) {
			return (MauronCode_timeUnit) Divide(tu.Ticks);
		}
		//Smaller or Equal
		public bool SmallerOrEqual (long n) {
			return n<=Ticks;
		}
		public bool SmallerOrEqual (MauronCode_timeUnit tu) {
			return SmallerOrEqual(tu.Ticks);
		}
		//Larger or Equal
		public bool LargerOrEqual (long n) {
		 return n>=Ticks;
		}
		public bool LargerOrEqual (MauronCode_timeUnit tu) {
			return LargerOrEqual(tu.Ticks);
		}
		#endregion

		#region I_Instantiable
		//Instantiate
		public MauronCode_timeUnit Instance {
			get {
				return new MauronCode_timeUnit(this);
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
