namespace MauronAlpha.MonoGame.Rendering.Utility {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	/// <summary> RenderUtility that PreRenders to a texture </summary>
	public class PreRenderer:MonoGameComponent {

		public static void DrawMethod(GameRenderer renderer, long time) {
			I_GameScene scene = renderer.CurrentScene;
			RenderStage stage = renderer.DefaultRenderTarget;
			GraphicsDevice device = renderer.GraphicsDevice;
			device.SetRenderTarget(stage.RenderTarget);

			I_PreRenderable obj;
			RenderOrders orders;
			Texture2D texture;
			SpriteBuffer result = new SpriteBuffer();
			MonoGameTexture sprite;
			SpriteDrawManager batch = renderer.SpriteDrawManager;

			while (!renderer.Queue.IsEmpty) {
				obj = renderer.Queue.Pop;
				orders = obj.RenderOrders;

				foreach (PreRenderProcess cycle in orders)
					if (cycle.RenderType.Equals(RenderTypes.Shape))
						ShapeRenderer.Render(renderer, renderer.CurrentShader , cycle.Shapes);
					else if (cycle.RenderType.Equals(RenderTypes.Lines))
						LineRenderer.Render(renderer, cycle.Lines, cycle.Color);
					else if (cycle.RenderType.Equals(RenderTypes.Sprite))
						TextureRenderer.Render(renderer, cycle.Sprites, cycle.Color);

				texture = ExtractTexture(stage, obj.Bounds);
				obj.SetRenderResult(new RenderResult(time,texture));
				sprite = new MonoGameTexture(renderer.Game,texture);
				result.Add(new SpriteData(sprite));
			}
			device.SetRenderTarget(null);
			renderer.HandlePreRenderEvent(renderer, result, time);
		}

		public static void ExecutePreRenderProcess(GameRenderer renderer, PreRenderProcess process, long time) {
			RenderStage stage = renderer.DefaultRenderTarget;
			GraphicsDevice device = renderer.GraphicsDevice;
			device.SetRenderTarget(stage.RenderTarget);

			Texture2D texture;
			string type = process.RenderType;
			if (type.Equals(RenderTypes.Shape))
				ShapeRenderer.HandlePreRenderProcess(renderer, time, process);
			else if (type.Equals(RenderTypes.Lines))
				LineRenderer.HandlePreRenderProcess(renderer, time, process);
			else if (type.Equals(RenderTypes.Sprite))
				TextureRenderer.HandlePreRenderProcess(renderer, time, process);

			texture = PreRenderer.ExtractTexture(stage, process.Bounds);
			process.SetRenderResult(texture, time);
		}

		public static void ExecutePreRenderProcess(GameRenderer renderer, RenderStage stage, PreRenderProcess process, long time, ProcessMethod shapes, ProcessMethod lines, ProcessMethod sprites) {
			GraphicsDevice device = renderer.GraphicsDevice;
			renderer.SetActiveStage(stage);

			Texture2D texture;
			string type = process.RenderType;
			if (type.Equals(RenderTypes.Shape))
				shapes(renderer, time, process);
			else if (type.Equals(RenderTypes.Lines))
				lines(renderer, time, process);
			else if (type.Equals(RenderTypes.Sprite))
				lines(renderer, time, process);

			texture = PreRenderer.ExtractTexture(stage, process.Bounds);
			process.SetRenderResult(texture, time);
		}

		public static Texture2D ExtractTexture(RenderStage stage, Polygon2dBounds bounds) {
			return ExtractTexture(stage, (int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height);
		}
		public static Texture2D ExtractTexture(RenderStage stage, Rectangle bounds) {
			bounds = OffsetToZero(bounds, stage);

			int size = bounds.Width * bounds.Height;

			Color[] data = new Color[size];

			stage.RenderTarget.GetData<Color>(0, bounds, data, 0, size);

			Texture2D result = new Texture2D(stage.Game.Renderer.GraphicsDevice, bounds.Width, bounds.Height,false,SurfaceFormat.Color);
			result.SetData<Color>(data);
			return result;
		}
		public static Texture2D ExtractTexture(RenderStage stage, int x, int y, int width, int height) {
			Rectangle bounds = new Rectangle(x, y, width, height);
			return ExtractTexture(stage, bounds);
		}

		public static void UpdateRenderResult(I_PreRenderable obj, Texture2D texture, long time) {
			I_RenderResult result = null;
			if (!obj.TryRenderResult(ref result)) {
				result = new RenderResult(time, texture);
				return;
			}
			result.SetResult(texture, time);
		}

		public static Rectangle OffsetToZero(Rectangle bounds, RenderStage stage) {
			if (bounds.X < 0)
				bounds.X = 0;
			if (bounds.Y < 0)
				bounds.Y = 0;
			if (bounds.X + bounds.Width > stage.Width)
				bounds.Width = (int) (stage.Width-bounds.X);
			if (bounds.Y + bounds.Height > stage.Height)
				bounds.Height = (int) (stage.Height-bounds.Y);
			return bounds;
		}

		public delegate void ProcessMethod(GameRenderer renderer, long time, PreRenderProcess process);

	}
}
