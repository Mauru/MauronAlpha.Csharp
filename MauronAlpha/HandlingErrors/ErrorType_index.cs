using System;

namespace MauronAlpha.HandlingErrors {

	//A fatal error condition
	public sealed class ErrorType_index : ErrorType {
		#region Singleton
		private static volatile ErrorType_index instance=new ErrorType_index();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_index ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_index();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "index"; }
		}
	}

}
