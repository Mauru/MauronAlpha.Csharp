namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.HandlingErrors;

	public class GameError:MauronCode_error {
		public GameError(string code, object source):base(code,source,ErrorType_fatal.Instance) { }
	}

}