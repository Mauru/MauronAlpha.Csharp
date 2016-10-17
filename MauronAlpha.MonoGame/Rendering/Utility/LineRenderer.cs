namespace MauronAlpha.MonoGame.Rendering.Utility {
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Geometry.DataObjects;

	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Geometry;
	public class LineRenderer:MonoGameComponent {


		public static void DrawLines(GameRenderer renderer, long time) {

			I_GameScene scene = renderer.CurrentScene;
			SpriteBatch batch = renderer.DefaultSpriteBatch;
			GraphicsDevice device = renderer.GraphicsDevice;

			device.Clear(Color.DarkBlue);

			LineBuffer buffer = scene.LineBuffer;
			Texture2D pixel = renderer.PixelTexture;

			batch.Begin();

			foreach (MonoGameLine line in buffer) {
				Rectangle r = line.Rectangle;
				batch.Draw(pixel, line.Rectangle, null,	line.Color, line.AngleAsRadFloat, Vector2.Zero, SpriteEffects.None, 1f);
			}
			batch.End();



		}

	

}
}
