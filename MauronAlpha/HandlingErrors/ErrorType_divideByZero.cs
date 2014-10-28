using System;

namespace MauronAlpha.HandlingErrors {

	//A fatal error condition
	public sealed class ErrorType_divideByZero : ErrorType {
		#region Singleton
		private static volatile ErrorType_divideByZero instance=new ErrorType_divideByZero();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_divideByZero ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_divideByZero();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "divideByZero"; }
		}
	}

}
