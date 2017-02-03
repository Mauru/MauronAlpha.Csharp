namespace MauronAlpha.MonoGame.Rendering.Utility {
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Geometry.DataObjects;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;
	public class MixedRenderer : MonoGameComponent {

		public static void DrawMethod(GameRenderer renderer, long time) {
			GraphicsDevice device = renderer.GraphicsDevice;

			device.Clear(Color.DarkSlateBlue);
			BasicEffect effect = new BasicEffect(device);
			I_GameScene scene = renderer.CurrentScene;
			SpriteBuffer sprites = scene.SpriteBuffer;
			ShapeBuffer shapes = scene.ShapeBuffer;
			LineBuffer lines = scene.LineBuffer;




			I_Shader shader = renderer.CurrentShader;
			shader.Apply();
			foreach (TriangulationData data in shapes) {
				if (data.VertexShaderMode == VertexShaderMode.VertexPosition2d)
					device.DrawUserPrimitives<VertexPosition>(PrimitiveType.TriangleList, data.VertexPosition, 0, data.TriangleCount);
				else
					device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, data.VertexPositionColor, 0, data.TriangleCount);
			}	

			SpriteDrawManager batch = renderer.SpriteDrawManager;
			batch.Begin();
			foreach(MonoGameLine line in lines)
				batch.Draw(renderer.PixelTexture, line.Rectangle, null, Color.White, (float) line.AngleAsRad, Vector2.Zero, SpriteEffects.None, 1f);
			batch.End();
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
