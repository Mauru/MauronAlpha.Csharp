namespace MauronAlpha.MonoGame.Rendering.Interfaces {
	
	public interface I_SpriteDrawCall {



	}



}

namespace MauronAlpha.MonoGame.Rendering.Interfaces {
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public interface I_MonoGameSpriteInfo {

		Texture2D Texture { get; }

		Color Tint { get; }
		Vector2 Position { get; }
		Vector2 Offset { get; }
		float Rotation { get; }
		Vector2 Scale { get; }
		float ZIndex { get; }

		System.Nullable<Rectangle> Mask { get; }

	}
}
