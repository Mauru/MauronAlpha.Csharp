namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Interfaces;

	using Microsoft.Xna.Framework.Graphics;

	public class RenderOrder {

		BlendMode _mode;
		I_Renderable _target;
		bool _isBusy = false;

		public static delegate Texture2D RenderMethod(GameRenderer renderer, I_Renderable target, RenderOrder order);

		public I_RenderResult Render(GameRenderer renderer, I_Renderable target, RenderMethod method, long time) {

			_isBusy = true;
			Texture2D result = method(renderer, target, this);
			RenderResult rendered = new RenderResult(time,target,result);
			_isBusy = false;
			return rendered;

		}

	}

}
namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Interfaces;

	using Microsoft.Xna.Framework.Graphics;

	public class RenderResult :MonoGameComponent, I_RenderResult {

		public RenderResult()
			: base() {

		}
		public RenderResult(long time, I_Renderable target, Texture2D result) {
		_result = result;
		_time = time;
		target.SetRenderResult(this);
		}

		Texture2D _result;
		long _time = 0;

		public static RenderResult Empty { get { return new RenderResult(); } }

		public Texture2D Result {
			get { return _result; }
		}

		public bool HasResult {
			get { return _result == null; }
		}

		public void SetResult(Texture2D result, long time) {
		_result = result;
		_time = time;
		}
	}

}

namespace MauronAlpha.MonoGame.Rendering {
	using Microsoft.Xna.Framework.Graphics;
	
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;

	public class TextRenderer :MonoGameComponent {

		RenderOrder.RenderMethod RenderMethod { get { return TextRenderer.Render; } }

		public static Texture2D Render(GameRenderer renderer, I_Renderable target, RenderOrder order) {
			
			//0: Cast target as text-object

			I_TextInterpreter text = target.TargetAs<I_TextInterpreter>();

			//1: create a texture atlas
			List<MonoGameTexture> textures = text.Textures;
			

		}
	}


}