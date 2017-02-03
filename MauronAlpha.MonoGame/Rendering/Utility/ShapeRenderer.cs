namespace MauronAlpha.MonoGame.Rendering.Utility {
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Geometry;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

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

			foreach (TriangulationData data in buffer) {
				if(data.VertexShaderMode==VertexShaderMode.VertexPosition2d)
					device.DrawUserPrimitives<VertexPosition>(PrimitiveType.TriangleList, data.VertexPosition, 0, data.TriangleCount);
				else
					device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, data.VertexPositionColor, 0, data.TriangleCount);
			}
		}

		/// <summary>Solve  PreRenderProcess for a shape</summary>
		public static void HandlePreRenderProcess(GameRenderer renderer, long time, PreRenderProcess process) {

			ShapeBuffer buffer = null;
			if (!process.TryShapes(ref buffer))
				return;

			GraphicsDevice device = renderer.GraphicsDevice;

			I_Shader shader = null;
			if (!process.TryShader(ref shader))
				shader = renderer.CurrentShader;

			shader.Apply();

			foreach (TriangulationData data in buffer) {
				if (data.VertexShaderMode == VertexShaderMode.VertexPosition2d)
					device.DrawUserPrimitives<VertexPosition>(PrimitiveType.TriangleList, data.VertexPosition, 0, data.TriangleCount);
				else
					device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, data.VertexPositionColor, 0, data.TriangleCount);
			}		
		}
		
		//blind instanced render
		public static void Render(GameRenderer renderer, I_Shader shader, ShapeBuffer buffer) {
			GraphicsDevice device = renderer.GraphicsDevice;

			shader.Apply();
			foreach (TriangulationData data in buffer) {
				if (data.VertexShaderMode == VertexShaderMode.VertexPosition2d)
					device.DrawUserPrimitives<VertexPosition>(PrimitiveType.TriangleList, data.VertexPosition, 0, data.TriangleCount);
				else
					device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, data.VertexPositionColor, 0, data.TriangleCount);
			}
		}
	}
}
