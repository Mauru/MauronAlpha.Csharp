using System;

namespace MauronAlpha.Mathematics {
	
	public interface I_mathComponent:ICloneable,IComparable<double>,IEquatable<double> {
		I_mathComponent Add(double n);
		I_mathComponent Subtract(double n);
		I_mathComponent Multiply(double n);
		I_mathComponent Divide(double n);		
		bool SmallerOrEqual(double n);
		bool LargerOrEqual(double n);
	}
}
