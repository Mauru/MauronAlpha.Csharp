namespace MauronAlpha.MonoGame.Rendering.Utility {
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public class TextureRenderer:MonoGameComponent {

		public static void DrawMethod(GameRenderer renderer, long time) {

			I_GameScene scene = renderer.CurrentScene;

			SpriteBuffer buffer = scene.SpriteBuffer;

			SpriteBatch batch = renderer.DefaultSpriteBatch;

			renderer.GraphicsDevice.Clear(Color.Purple);

			batch.Begin();
			foreach (SpriteData data in buffer)
				batch.Draw(data.Texture.AsTexture2d, data.Texture.SizeAsRectangle, data.Mask, data.Color);
			batch.End();
		}
	}

}
