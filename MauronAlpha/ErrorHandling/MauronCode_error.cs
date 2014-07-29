using System;


namespace MauronAlpha {

	public class MauronCode_error : Exception {

		public object ErrorSource;
		
		public MauronCode_error (string message, object source)	: base(message) {
			MauronCode.Debug(message,source);
			ErrorSource=source;
			Environment.Exit(1);
		}

	}

	

}
