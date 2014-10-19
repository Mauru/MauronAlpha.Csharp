using System;

namespace MauronAlpha.HandlingErrors {

	//A fatal error condition
	public sealed class ErrorType_constructor : ErrorType {

		#region Singleton
		private static volatile ErrorType_constructor instance=new ErrorType_constructor();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_constructor ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_constructor();
					}
				}
				return instance;
			}
		}
		#endregion

		//Information on the Error
		public override string Name {
			get { return "constructor"; }
		}
		public string Description {
			get {
				return "Invalid Constructor!";
			}
		}
		public string Hint {
			get {
				return "Check the constructor Arguments for the object.";
			}
		}
	
	}

}
