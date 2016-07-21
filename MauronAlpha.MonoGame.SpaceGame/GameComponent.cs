namespace MauronAlpha.MonoGame.SpaceGame {
	using MauronAlpha.MonoGame.Logic;
	using MauronAlpha.ExplainingCode;
	using MauronAlpha.HandlingData;
	using MauronAlpha.HandlingErrors;

	public class GameEvent<T> : GameComponent where T:GameEventType { }

	public class GameEventType : GameComponent { }

	public class CriticalGameError : MauronCode_error {
		public CriticalGameError(string message, MauronCode_component obj, string namespaceFunction) : base(message, obj, ErrorType_fatal.Instance) { }
	}
}
