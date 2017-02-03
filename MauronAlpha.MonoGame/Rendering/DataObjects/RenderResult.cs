namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.MonoGame.Rendering.Interfaces;

	public class RenderResult :MonoGameComponent, I_RenderResult {

		public RenderResult(): base() {}
		public RenderResult(long time, Texture2D result) {
			_result = result;
			_time = time;
		}

		long _time = 0;
		public long Time { get { return _time; } }

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

		public static RenderResult Empty { get { return new RenderResult(); } }
	}

}
