namespace MauronAlpha.MonoGame.Rendering {
	using Microsoft.Xna.Framework.Graphics;

	public class BackBuffer:RenderTarget2D {

		public BackBuffer(GameManager game) : base(game.Engine.GraphicsDevice, game.Renderer.ScreenSize.IntX, game.Renderer.ScreenSize.IntY, false, SurfaceFormat.Color, DepthFormat.None) { }

		public void SetAsRenderTarget() {
			GraphicsDevice.SetRenderTarget(this);
		}
	
	}
}
