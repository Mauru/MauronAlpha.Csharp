namespace MauronAlpha.MonoGame.Rendering {
	using MauronAlpha.MonoGame.Interfaces;

	using Microsoft.Xna.Framework.Graphics;

	public class RenderResult :MonoGameComponent, I_RenderResult {

		public RenderResult(): base() {}
		public RenderResult(long time, I_Renderable target, Texture2D result) {
			_result = result;
			_time = time;
			target.SetRenderResult(this);
		}


		long _time = 0;
		public long Time { get { return _time; } }

		public static RenderResult Empty { get { return new RenderResult(); } }

		Texture2D _result;
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