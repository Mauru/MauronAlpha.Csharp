using System;

namespace MauronAlpha.HandlingErrors {

	//A fatal error condition
	public sealed class ErrorType_limit : ErrorType {
		#region Singleton
		private static volatile ErrorType_limit instance=new ErrorType_limit();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_limit ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_limit();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "limit"; }
		}
	}

}
