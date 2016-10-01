namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Interfaces;

	using Microsoft.Xna.Framework.Graphics;

	public class RenderOrder {

		BlendMode _mode;
		I_Renderable _target;
		bool _isBusy = false;

		public delegate Texture2D RenderMethod(GameRenderer renderer, I_Renderable target, RenderOrder order);

		public I_RenderResult Render(GameRenderer renderer, I_Renderable target, RenderMethod method, long time) {

			_isBusy = true;
			Texture2D result = method(renderer, target, this);
			RenderResult rendered = new RenderResult(time, target, result);
			target.SetRenderResult(rendered);
			_isBusy = false;
			return rendered;

		}

	}

}