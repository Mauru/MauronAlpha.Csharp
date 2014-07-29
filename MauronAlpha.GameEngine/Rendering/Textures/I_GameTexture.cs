using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Rendering.Textures {

	public interface I_GameTexture: I_Drawable {
		
		TextureType TextureType { get; }


		GameAsset[] Assets { get; }
		GameAsset Asset { get; }
		bool Loaded { get; }

		Polygon2d Mask { get; }
		Rectangle2d PixelArea { get; }

		I_GameTexture CreateFromAsset(GameAsset Asset, I_Drawable parent, string name);
	}
}

