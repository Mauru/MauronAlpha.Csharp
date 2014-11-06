using System;

namespace MauronAlpha.HandlingErrors {

	/// <summary>Performing an action is outside of the capabilities of the executing code.</summary>
	/// <remarks>EXAMPLE: tring to set SystemTime</remarks>
	public sealed class ErrorType_scope : ErrorType {
		#region Singleton
		private static volatile ErrorType_scope instance=new ErrorType_scope();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_scope ( ) { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new ErrorType_scope();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "scope"; }
		}

	}

}