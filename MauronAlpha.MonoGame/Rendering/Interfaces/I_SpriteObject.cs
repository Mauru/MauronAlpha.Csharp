namespace MauronAlpha.MonoGame.Rendering.Interfaces {

	using Microsoft.Xna.Framework.Graphics;

	public interface I_SpriteObject {

		Texture2D Texure { get; }
		I_SpriteDrawCall SpriteDrawInfo { get; }

	}

}
