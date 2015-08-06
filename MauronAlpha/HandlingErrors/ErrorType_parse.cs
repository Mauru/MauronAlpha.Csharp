using System;

namespace MauronAlpha.HandlingErrors {

	//A fatal error condition that is thrown if a parser tries to parse an object but fails
	public sealed class ErrorType_parse : ErrorType {
		#region Singleton
		private static volatile ErrorType_parse instance=new ErrorType_parse();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static ErrorType_parse() { }
		public static ErrorType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance = new ErrorType_parse();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "parse"; }
		}
	}

}