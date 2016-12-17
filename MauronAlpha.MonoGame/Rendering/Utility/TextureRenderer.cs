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
			foreach (SpriteData data in buffer) {
				Texture2D d = data.Texture.AsTexture2d;
				batch.Draw(d,d.Bounds,data.Color);
			}
			batch.End();
		}

		public static void Render(GameRenderer renderer, SpriteBuffer buffer, Color color) {
			SpriteBatch batch = renderer.DefaultSpriteBatch;
			Texture2D texture;
			batch.Begin();
			foreach (SpriteData data in buffer) {
				texture = data.Texture.AsTexture2d;
				if(data.HasMask)
					batch.Draw(texture, data.PositionAsRectangle, data.Mask, data.Color);
				else
					batch.Draw(texture, texture.Bounds, data.Color);
			}
			batch.End();
		}
	}

}
