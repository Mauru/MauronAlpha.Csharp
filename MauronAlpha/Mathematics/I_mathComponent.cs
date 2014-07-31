using System;

namespace MauronAlpha.Mathematics {
	
	public interface I_mathComponent:ICloneable,IComparable<long>,IEquatable<long> {
		I_mathComponent Add(long n);
		I_mathComponent Subtract(long n);
		I_mathComponent Multiply(long n);
		I_mathComponent Divide(long n);		
		bool SmallerOrEqual(long n);
		bool LargerOrEqual(long n);
	}
}
