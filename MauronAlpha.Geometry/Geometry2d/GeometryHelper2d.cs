namespace MauronAlpha.Geometry {
	using System;

	//Base class for generic mathematical operations
	public class GeometryHelper2d:MauronCode_utility {
		
		//A precision value to avoid 0
		public static double NotQuiteZero=0.00001;
	
		// degrees to radians
		public static double Deg2Rad (double n) {
			return Math.PI*n/180;
		}

		// radians to degrees
		public static double Rad2Deg (double n) {
			return n*(180/Math.PI);
		}

		//turn any angle into a number 0<360
		public static int NormaliseAngle(int degrees) {
			int retval=degrees%360;
			if( retval<0 )
				retval+=360;
			return retval;
		}

		//return PI
		public static double PI {
			get {
				return Math.PI;
			}
		}
	}

}
