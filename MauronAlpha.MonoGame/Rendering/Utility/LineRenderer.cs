namespace MauronAlpha.MonoGame.Rendering.Utility {
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Geometry.DataObjects;

	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Geometry;

	using MauronAlpha.MonoGame.Collections;

	public class LineRenderer:MonoGameComponent {

		public static void DrawLines(GameRenderer renderer, long time) {

			Matrix m = Matrix.Identity;
			m.Translation = new Vector3(1, 1, 1);
			Vector3 test = new Vector3(0, 0, 0);



			I_GameScene scene = renderer.CurrentScene;
			SpriteBatch batch = renderer.DefaultSpriteBatch;
			GraphicsDevice device = renderer.GraphicsDevice;

			device.Clear(Color.DarkBlue);

			LineBuffer buffer = scene.LineBuffer;
			Texture2D pixel = renderer.PixelTexture;

			batch.Begin();

			foreach (MonoGameLine line in buffer) {
				Rectangle r = line.Rectangle;
				batch.Draw(pixel, line.Rectangle, null,	line.Color, (float) line.AngleAsRad, Vector2.Zero, SpriteEffects.None, 1f);
			}
			batch.End();



		}

		public static VertexPositionColor VertexPositionColor(double x, double y, Color color) {
			return new VertexPositionColor(new Vector3((float)x, (float)y, 0f), color);
		}

		public static List<VertexPositionColor> Pixel(Color color) {
			List<VertexPositionColor> result = new List<VertexPositionColor>() {
				LineRenderer.VertexPositionColor(0,0,color),
				LineRenderer.VertexPositionColor(1,0,color),
				LineRenderer.VertexPositionColor(1,1,color),
				LineRenderer.VertexPositionColor(0,0,color),
				LineRenderer.VertexPositionColor(1,1,color),
				LineRenderer.VertexPositionColor(0,1,color)
			};
			return result;
		}

}
}
