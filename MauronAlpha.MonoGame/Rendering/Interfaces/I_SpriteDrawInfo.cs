namespace MauronAlpha.MonoGame.Rendering.Interfaces {
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	public interface I_SpriteDrawInfo {
		BlendMode BlendMode { get; }
		bool HasMask { get; }
	}
}
