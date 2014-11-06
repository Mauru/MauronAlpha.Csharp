using System;

namespace MauronAlpha.HandlingErrors {

	/// <summary>Accessing a procedural (EXPENSIVE) property before it is Set.</summary>
	/// <remarks>EXAMPLE: Accessing content of a file while it is still being loaded.</remarks>
	/// <remarks>PUROSE: Prevention of infinite loops.</remarks>
	public sealed class ErrorType_outOfSynch : ErrorType {
		#region Singleton
		private static volatile ErrorType_outOfSynch instance=new ErrorType_outOfSynch();
		private static object syncRoot = new Object();

		//constructor singleton multithread safe
		static ErrorType_outOfSynch ( ) { }

		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance = new ErrorType_outOfSynch();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "outOfSynch"; }
		}

	}

}