namespace MauronAlpha.MonoGame.Assets.Interfaces {
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	public interface I_MonoGameTexture {
		Texture2D AsTexture2d { get; }

		double Width { get; }
		double Height { get; }

		Rectangle SizeAsRectangle { get; }
	}
}
