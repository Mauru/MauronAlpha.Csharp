using System;

namespace MauronAlpha.HandlingErrors {

	//A Error condition that mean a class that was supposed to look for something did not find it (an index in an array for example)
	public sealed class ErrorType_bounds : ErrorType {
		#region Singleton
		private static volatile ErrorType_bounds instance=new ErrorType_bounds();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_bounds ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_bounds();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "bounds"; }
		}

		public override bool IsFatal {
			get { return true; }
		}
	}
}

