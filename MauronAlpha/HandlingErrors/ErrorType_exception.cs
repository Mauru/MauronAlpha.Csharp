using System;

namespace MauronAlpha.HandlingErrors {

	//A fatal error condition
	public sealed class ErrorType_exception : ErrorType {
		#region Singleton
		private static volatile ErrorType_exception instance=new ErrorType_exception();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_exception ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_exception();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "exception"; }
		}
	}

}
