namespace MauronAlpha.MonoGame.Rendering {
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using Microsoft.Xna.Framework.Graphics;

	public class RenderResult :MonoGameComponent, I_RenderResult {

		public RenderResult(): base() {}
		public RenderResult(long time, I_Renderable target, Texture2D result) {
			_result = result;
			_time = time;
			target.SetRenderResult(this);
			//_size = actualSize;
			//_position = positionOfObject;
		}

		Vector2d _size;
		public Vector2d ActualObjectSize { get { return _size; } }
		Vector2d _position;
		public Vector2d Position { get { return _position; } }

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