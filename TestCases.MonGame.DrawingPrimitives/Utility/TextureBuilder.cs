using Microsoft.Xna.Framework.Graphics;


namespace MauronAlpha.MonoGame.Utility {
	
	
	public class TextureBuilder:MonoGameComponent {
		RenderTarget2D RenderTarget;

		public void ToTexture(GraphicsDevice device, VertexBuffer buffer) {
			RenderTarget = new RenderTarget2D(
				device,
				device.PresentationParameters.BackBufferWidth,
				device.PresentationParameters.BackBufferHeight,
				false,
				device.PresentationParameters.BackBufferFormat,
				DepthFormat.Depth24);


		}
	}



}
