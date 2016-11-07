namespace MauronAlpha.MonoGame.Assets.DataObjects {
	using MauronAlpha.MonoGame.DataObjects;

	public class AssetError:GameError {

		public AssetError(string message, object source) : base(message, source) { }
	}
}
