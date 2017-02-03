namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Assets.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

	using MauronAlpha.MonoGame.Rendering.Interfaces;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public class PreRenderedTexture : MonoGameComponent, I_MonoGameTexture {

		I_RenderResult _result;
		public PreRenderedTexture(I_RenderResult result): base() {
			_result = result;
		}
		public long LastUpdated { get { return _result.Time; } }

		public Texture2D AsTexture2d {
			get { return _result.Result; }
		}

		public double Width {
			get { return _result.Result.Width; }
		}
		public double Height {
			get { return _result.Result.Height; }
		}
	
		public static PreRenderedTexture FromPreRenderProcess(GameRenderer renderer, PreRenderProcess process) {
			PreRenderedTexture texture = new PreRenderedTexture(process.RenderResult);
			return texture;
		}

		public Rectangle SizeAsRectangle {
			get { return _result.Result.Bounds; }
		}
	}
}