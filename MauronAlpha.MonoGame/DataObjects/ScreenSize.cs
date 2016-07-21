using Microsoft.Xna.Framework.Graphics;
namespace MauronAlpha.MonoGame.DataObjects {
	
	public class ScreenSize:MonoGameComponent {

		GraphicsDevice Device;

		public ScreenSize(GraphicsDevice device):base() {
			Device = device;
		}

		public double Width {
			get { return Device.Viewport.Width;}
		}
		
		public double Height {
			get { return Device.Viewport.Height;}
		}

	}
}
