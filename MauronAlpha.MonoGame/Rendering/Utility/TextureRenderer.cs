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

			SpriteDrawManager batch = renderer.SpriteDrawManager;

			renderer.GraphicsDevice.Clear(Color.Purple);

			batch.Begin();
			foreach (SpriteDrawCall data in buffer) {
				Texture2D d = data.Texture.AsTexture2d;
				batch.Draw(d,d.Bounds,data.Color);
			}
			batch.End();
		}

		public static void Render(GameRenderer renderer, SpriteBuffer buffer, Color color) {
			SpriteDrawManager batch = renderer.SpriteDrawManager;
			Texture2D texture;
			batch.Begin();
			foreach (SpriteDrawCall data in buffer) {
				texture = data.Texture.AsTexture2d;
				if(data.HasMask)
					batch.Draw(texture, data.PositionAsRectangle, data.Mask, data.Color);
				else
					batch.Draw(texture, texture.Bounds, data.Color);
			}
			batch.End();
		}

		public static void HandlePreRenderProcess(GameRenderer renderer, long time, PreRenderProcess process) {
			SpriteBuffer buffer = null;
			if (!process.TrySprites(ref buffer))
				return;
			SpriteDrawManager batch = renderer.SpriteDrawManager;
			Texture2D texture;
			batch.Begin();
			foreach (SpriteDrawCall data in buffer) {
				texture = data.Texture.AsTexture2d;
				if (data.HasMask)
					batch.Draw(texture, data.PositionAsRectangle, data.Mask, data.Color);
				else
					batch.Draw(texture, texture.Bounds, data.Color);
			}
			batch.End();
		}
	}

}
