using System;

namespace MauronAlpha.ErrorHandling {

	//A fatal error condition
	public sealed class ErrorType_fatal:ErrorType {
		#region Singleton
		private static volatile ErrorType_fatal instance=new ErrorType_fatal();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static ErrorType_fatal ( ) { }
		public static ErrorType Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new ErrorType_fatal();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name {
			get { return "fatal"; }
		}
	}

}
