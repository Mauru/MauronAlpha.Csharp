using System;

namespace MauronAlpha.HandlingErrors {

	//A fatal error condition
	public sealed class ErrorType_nullError : ErrorType {
		#region Singleton
		private static volatile ErrorType_nullError instance=new ErrorType_nullError();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_nullError ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_nullError();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "nullError"; }
		}
	}

}
