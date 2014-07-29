using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.GameEngine {
	public class GameCodeError : Exception {
		public object ErrorSource;
		public GameCodeError(string message,object source):base(message) {
			MauronCode.Debug(message,source);
			ErrorSource=source;
			Environment.Exit(1);
		}
	}
	public class GameCodeException : Exception {
		public object ErrorSource;
		public GameCodeException (string message, object source)
			: base(message) {
			MauronCode.Debug(message, source);
			ErrorSource=source;
		}
	}

}
