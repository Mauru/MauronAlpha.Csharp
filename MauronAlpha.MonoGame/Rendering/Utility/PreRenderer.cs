﻿namespace MauronAlpha.MonoGame.Rendering.Utility {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

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
			SpriteBatch batch = renderer.DefaultSpriteBatch;

			while (!renderer.Queue.IsEmpty) {
				obj = renderer.Queue.Pop;
				orders = obj.RenderOrders;

				foreach (PreRenderOrder cycle in orders)
					if (cycle.RenderType.Equals(RenderTypes.Shape))
						ShapeRenderer.Render(renderer, cycle.Shader, cycle.Shapes);
					else if (cycle.RenderType.Equals(RenderTypes.Lines))
						LineRenderer.Render(renderer, cycle.Lines, cycle.Color);
					else if (cycle.RenderType.Equals(RenderTypes.Sprite))
						TextureRenderer.Render(renderer, cycle.Sprites, cycle.Color);

				texture = ExtractTexture(stage, obj.Bounds);
				obj.SetRenderResult(texture, time);
				sprite = new MonoGameTexture(renderer.Game,texture);
				result.Add(new SpriteData(sprite));
			}
			device.SetRenderTarget(null);
			renderer.HandlePreRenderEvent(renderer, result, time);
		}

		public static Texture2D ExtractTexture(RenderStage stage, Polygon2dBounds bounds) {
			return ExtractTexture(stage, (int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height);
		}
		public static Texture2D ExtractTexture(RenderStage stage, Rectangle bounds) {

			int size = bounds.Width * bounds.Height;

			Color[] data = new Color[size];

			stage.RenderTarget.GetData<Color>(0, bounds, data, 0, size);

			Texture2D result = new Texture2D(stage.Game.Renderer.GraphicsDevice, bounds.Width, bounds.Height);
			result.SetData<Color>(data);
			return result;
		}

		public static Texture2D ExtractTexture(RenderStage stage, int x, int y, int width, int height) {
			Rectangle bounds = new Rectangle(x, y, width, height);
			return ExtractTexture(stage, bounds);
		}
	}
}
