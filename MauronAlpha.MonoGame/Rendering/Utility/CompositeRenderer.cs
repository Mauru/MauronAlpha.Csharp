namespace MauronAlpha.MonoGame.Rendering.Utility {
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using Microsoft.Xna.Framework.Graphics;

	public class CompositeRenderer:MonoGameComponent {

		public static GameRenderer.DrawMethod RenderMethod {
			get {
				return Draw;
			}
		}

		public static void Draw(GameRenderer renderer, long time) {
			I_GameScene scene = renderer.CurrentScene;

			//Get the composite buffer
			CompositeBuffer buffer = scene.CompositeBuffer;
			if (buffer.IsBusy)
				return;

			//Get the currently handled RenderComposite
			RenderComposite composite = null;
			if (!buffer.TryAdvanceQueue(ref composite))
				return;

			buffer.SetIsBusy(true);
			PreRenderChain chain = composite.Chain;

			if (chain.IsBusy)
				return;

			PreRenderProcess process = null;
			if (!chain.TryAdvanceQueue(ref process))
				return;

			chain.SetIsBusy(true);

			ExecuteProcess(renderer,process,time);

			chain.SetIsBusy(false);
			buffer.SetIsBusy(false);
		}

		//Dealing with PreRenderProcesses
		public static void ExecuteProcess(GameRenderer renderer, PreRenderProcess process, long time) {
			RenderStage stage = renderer.DefaultRenderTarget;
			GraphicsDevice device = renderer.GraphicsDevice;
			device.SetRenderTarget(stage.RenderTarget);

			I_Shader shader = renderer.CurrentShader;

			Texture2D texture;
			string type = process.RenderType;
			if (type.Equals(RenderTypes.Shape))
				ShapeRenderer.Render(renderer, shader, process.Shapes);
			else if (type.Equals(RenderTypes.Lines))
				LineRenderer.Render(renderer, process.Lines, process.Color);
			else if (type.Equals(RenderTypes.Sprite))
				TextureRenderer.Render(renderer, process.Sprites, process.Color);

			texture = PreRenderer.ExtractTexture(stage, process.Bounds);
			process.SetRenderResult(texture, time);
		}

	}
}

