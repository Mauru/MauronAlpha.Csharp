namespace MauronAlpha.MonoGame.Rendering.Utility {
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Geometry;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	public class ShapeRenderer:MonoGameComponent {

		public static void RenderDirectlyToScreen(GameRenderer renderer, long time) {
			I_GameScene scene = renderer.CurrentScene;
			I_Shader shader = renderer.CurrentShader;
			GraphicsDevice device = renderer.GraphicsDevice;

			device.Clear(Color.Magenta);

			ShapeBuffer buffer = scene.ShapeBuffer;

			shader.Apply();

			foreach (TriangulationData data in buffer)
				device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, data.Vertices, 0, data.TriangleCount);
		}
		
		//blind instanced render
		public static void Render(GameRenderer renderer, I_Shader shader, ShapeBuffer buffer) {
			GraphicsDevice device = renderer.GraphicsDevice;
			shader.Apply();
			foreach (TriangulationData data in buffer)
				device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, data.Vertices, 0, data.TriangleCount);
		}
	}
}
