using System;

namespace MauronAlpha.HandlingErrors {

	//A fatal error condition.
	/// <summary>
	/// CONDITION: 
	///		* two OR MORE units can be compared with a method (Equals).
	///		* The result SHOULD be equal within a MARGIN of acceptable Error.
	///		* The RETURN of that method is outside of the required MARGIN.
	/// </summary>
	public sealed class ErrorType_precision : ErrorType {
		#region Singleton
		private static volatile ErrorType_precision instance=new ErrorType_precision();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_precision ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_precision();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "precision"; }
		}
	}

}