using System;

namespace MauronAlpha.HandlingErrors {

	//A fatal error condition
	public sealed class ErrorType_protected : ErrorType {
		#region Singleton
		private static volatile ErrorType_protected instance=new ErrorType_protected();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_protected ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_protected();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "protected"; }
		}
	}

}
