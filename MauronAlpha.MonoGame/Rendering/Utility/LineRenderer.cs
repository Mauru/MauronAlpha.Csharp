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
			I_GameScene scene = renderer.CurrentScene;
			SpriteBatch batch = renderer.DefaultSpriteBatch;
			GraphicsDevice device = renderer.GraphicsDevice;

			device.Clear(Color.DarkBlue);

			LineBuffer buffer = scene.LineBuffer;
			Texture2D pixel = renderer.PixelTexture;

			batch.Begin();

			float angle;
			int c = 0;
			Color[] colors = new Color[4] { Color.Red, Color.Green, Color.Yellow, Color.Black };
			foreach (MonoGameLine line in buffer) {
				Rectangle r = line.Rectangle;
				angle = (float) line.AngleAsRad;
				batch.Draw(pixel, line.Rectangle, line.Rectangle, Color.White, angle, Vector2.Zero, SpriteEffects.None, 1f);
				c++;
			}
			batch.End();
		}

		public static VertexPositionColor VertexPositionColor(double x, double y, Color color) {
			return new VertexPositionColor(new Vector3((float)x, (float)y, 0f), color);
		}

		public static List<VertexPositionColor> Pixel(Color color) {
			List<VertexPositionColor> result = new List<VertexPositionColor>() {
				VertexPositionColor(0,0,color),
				VertexPositionColor(1,0,color),
				VertexPositionColor(1,1,color),
				VertexPositionColor(0,0,color),
				VertexPositionColor(1,1,color),
				VertexPositionColor(0,1,color)
			};
			return result;
		}

		public static void Render(GameRenderer renderer, LineBuffer lines, Color color) {
			SpriteBatch batch = renderer.DefaultSpriteBatch;
			batch.Begin();
			Texture2D pixel = renderer.PixelTexture;
			foreach (MonoGameLine line in lines)
				batch.Draw(pixel, line.Rectangle, null, color, (float)line.AngleAsRad, Vector2.Zero, SpriteEffects.None, 1f);
			batch.End();
		}

		public static void Print(long val) {
			Print("" + val);
		}
		public static void Print(string val) {
			System.Diagnostics.Debug.Print(val);
		}
		public static void Print(Rectangle r) {
			Print("{[x:" + r.X + ",y:" + r.Y + ",w:" + r.Width + ",h:" + r.Height + "]}");
		}
	}
}
