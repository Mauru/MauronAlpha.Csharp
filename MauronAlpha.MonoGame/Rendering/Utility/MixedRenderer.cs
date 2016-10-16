namespace MauronAlpha.MonoGame.Rendering.Utility {
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;
	public class MixedRenderer : MonoGameComponent {



		public static void DrawMethod(GameRenderer renderer, long time) {
			GraphicsDevice device = renderer.GraphicsDevice;

			device.Clear(Color.DarkSlateBlue);

			I_GameScene scene = renderer.CurrentScene;
			SpriteBuffer sprites = scene.SpriteBuffer;
			ShapeBuffer shapes = scene.ShapeBuffer;

			I_Shader shader = renderer.CurrentShader;
			shader.Apply();
			foreach (TriangulationData data in shapes.TriangulatedObjects)
				device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, data.Vertices, 0, data.TriangleCount);

			SpriteBatch batch = renderer.DefaultSpriteBatch;
			batch.Begin();
			foreach (SpriteData data in sprites) {
				Rectangle position = data.PositionAsRectangle;
				Rectangle mask = data.Mask;
				batch.Draw(data.Texture.AsTexture2d, position, mask, Color.White);
			}
			batch.End();
		}

	}
}
