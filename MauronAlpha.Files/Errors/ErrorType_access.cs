using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Files.Errors {
	public class ErrorType_access:ErrorType {
		public override string Name { get { return "access"; } }

		public static ErrorType_access Instance { get { return new ErrorType_access(); } }
	}
}

