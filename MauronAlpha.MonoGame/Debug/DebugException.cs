namespace MauronAlpha.MonoGame.Debug {

	/// <summary>An Exception for detailed state reports at runtime (do not use in final product unless you try/catch) </summary>
	public class DebugException :System.Exception {

		//constructors
		public DebugException(object source, string data):base(
			DebugException.IdentifyType(source)+":"+data+"#DEBUG.END#"
		) {}
		public DebugException(object source, long data):base(
			DebugException.IdentifyType(source)+":"+data+"#DEBUG.END#"
		) {}
		public DebugException(object source, System.Collections.Generic.ICollection<string> data):base(
			DebugException.IdentifyType(source)+":"+DebugException.SerializeMessages(data)
		) {}

		//Helper methods
		public static string IdentifyType(object source) {

			return source.GetType().ToString();

		}
		public static string SerializeMessages(System.Collections.Generic.ICollection<string> data) {

			string result = "";

			foreach(string s in data) {

				result += "#" + s + ",";

			}

			result += "#DEBUG.END#";
			return result;

		}

	}

}