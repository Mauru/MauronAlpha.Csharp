namespace MauronAlpha.MonoGame.Utility {
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Geometry;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Interfaces;

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	public class ShapeRenderer:MonoGameComponent, I_Renderer {

		public static I_RenderResult RenderShapeInWorldSpace(RenderStage stage, I_Renderable target, long time) {
			GameManager game = target.Game;
			I_MonoShape shape = target.AsMonoShape();
			GraphicsDevice device = game.Engine.GraphicsDevice;

			TriangulationData data = shape.TriangulationData;
			TriangleList tt = data.Triangles;

			// This step should be skippable / or placable elsewhere... maybe in the update loop?
			VertexPositionColor[] vv = tt.AsPositionColor;
			VertexBuffer buffer = data.GetVertexBuffer(device);

			Camera camera = game.Renderer.DefaultCamera;

			DefaultShader shader = (DefaultShader) game.Renderer.GetShader("Default");
			shader.World = camera.WorldMatrixOfRenderable(target);
			shader.View = camera.TopDownView;
			shader.Projection = camera.TopDownProjection;
			shader.VertexColorEnabled = true;

			RasterizerState rr = new RasterizerState();
			rr.CullMode = CullMode.None;

			device.RasterizerState = rr;
			device.SetVertexBuffer(buffer);
			device.SetRenderTarget(stage);

			foreach (EffectPass pass in shader.CurrentTechnique.Passes){
				pass.Apply();
				device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
			}
			Texture2D result = stage.AsTexture2D;
			
			// Get data from the rendertarget and set it to a texture
			return new RenderResult(time, target, result);
		}

		public static void RenderToScreen (PolyShape target, GraphicsDevice device){
			GameManager game = target.Game;

			//triangulate shape
			TriangulationData data = target.TriangulationData;
			TriangleList tt = data.Triangles;
			VertexPositionColor[] vv = tt.AsPositionColor;
			VertexBuffer buffer = data.GetVertexBuffer(device);

			Camera camera = game.Renderer.DefaultCamera;

			DefaultShader shader = (DefaultShader) game.Renderer.GetShader("Default");
			shader.World = camera.WorldMatrixOfRenderable(target);
			shader.View = camera.TopDownView;
			shader.Projection = camera.TopDownProjection;
			shader.VertexColorEnabled = true;

			RasterizerState rr = new RasterizerState();
			rr.CullMode = CullMode.None;
			device.RasterizerState = rr;
			device.SetVertexBuffer(buffer);
			foreach (EffectPass pass in shader.CurrentTechnique.Passes){
				pass.Apply();
				device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
			}
		}

		public MonoGameTexture RenderToTexture(PolyShape target, GraphicsDevice device, RenderTarget2D scene) {
			GameManager game = target.Game;

			MauronAlpha.Geometry.Geometry2d.Units.Polygon2dBounds bounds = target.Bounds;

			TriangulationData data = target.TriangulationData;
			TriangleList tt = data.Triangles;

			VertexPositionColor[] vv = tt.AsPositionColor;
			VertexBuffer buffer = data.GetVertexBuffer(device);

			Camera camera = game.Renderer.DefaultCamera;

			DefaultShader shader = (DefaultShader) game.Renderer.GetShader("Default");
			shader.World = camera.WorldMatrixOfRenderable(target);
			shader.View = camera.TopDownView;
			shader.Projection = camera.TopDownProjection;
			shader.VertexColorEnabled = true;

			RasterizerState rr = new RasterizerState();
			rr.CullMode = CullMode.None;

			device.SetRenderTarget(scene);
			device.RasterizerState = rr;

			device.SetVertexBuffer(buffer);
			foreach (EffectPass pass in shader.CurrentTechnique.Passes){
				pass.Apply();
				device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
			}
			
			// Get data from the rendertarget and set it to a texture
			Vector2d size = bounds.Size;
			Texture2D texture = new Texture2D(device, size.IntX, size.IntY);
			Color[] colorData = new Color[size.IntX*size.IntY];
			scene.GetData<Color>(colorData);
			texture.SetData<Color>(colorData);

			return new MonoGameTexture(game,texture);
		}

		public GameRenderer.RenderMethod RenderMethod {
			get { return ShapeRenderer.RenderShapeInWorldSpace; }
		}
	}
}
