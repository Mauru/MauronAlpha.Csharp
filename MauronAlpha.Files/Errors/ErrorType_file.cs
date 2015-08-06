using MauronAlpha.HandlingErrors;

namespace MauronAlpha.Files.Errors {
	public class ErrorType_file:ErrorType {
		public override string Name {
			get { return "file"; }
		}
		public static ErrorType_file Instance { get { return new ErrorType_file(); } }
	}
}
