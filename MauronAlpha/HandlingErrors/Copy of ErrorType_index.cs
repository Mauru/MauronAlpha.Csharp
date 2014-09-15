using System;

namespace MauronAlpha.HandlingErrors {

	//A fatal error condition, caused by shitty programming
	public sealed class ErrorType_bug : ErrorType {
		#region Singleton
		private static volatile ErrorType_bug instance=new ErrorType_bug();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_bug ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_bug();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "bug"; }
		}
	}

}
