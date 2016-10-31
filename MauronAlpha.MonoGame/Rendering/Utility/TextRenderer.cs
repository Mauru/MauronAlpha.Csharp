namespace MauronAlpha.MonoGame.Rendering.Utility {
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	using MauronAlpha.TextProcessing.Collections;
	using MauronAlpha.TextProcessing.Units;

	using MauronAlpha.FontParser.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.UI.DataObjects;
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public class TextRenderer:MonoGameComponent {

		public static void DrawMethod(GameRenderer renderer, long time) {
			I_GameScene scene = renderer.CurrentScene;

			GraphicsDevice device = renderer.GraphicsDevice;
			SpriteBatch batch = renderer.DefaultSpriteBatch;

			SpriteBuffer buffer = scene.SpriteBuffer;

			int index = 0;
			device.Clear(Color.Purple);
			batch.Begin();
			foreach (SpriteData data in buffer) {
				Rectangle position = data.PositionAsRectangle;
				Rectangle mask = data.Mask;
				batch.Draw(data.Texture.AsTexture2d, position, mask, Color.White);
				index++;

			}
			batch.End();
		}

		///<summary> Generates SpriteData.Mask using GameFont.PositionData</summary>
		public static Rectangle GenerateMaskFromPositionData(PositionData data) {
			return new Rectangle(data.X, data.Y, data.Width, data.Height);
		}
	
	}
}
