using System;

namespace MauronAlpha.HandlingErrors {

	public class MauronCode_exception : Exception {

		public object ErrorSource;

		public MauronCode_exception (string message, object source)
			: base(message) {
			MauronCode.Debug(message, source);
			ErrorSource=source;
		}

	}

}