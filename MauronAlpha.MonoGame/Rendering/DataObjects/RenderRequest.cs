namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.MonoGame.Rendering.Events;
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.Events.Units;

	public class RenderRequest:MonoGameComponent, I_sender<RenderEvent> {

		long _time = 0;
		public long Time {
			get {
				return _time;
			}
		}

		public long TimeCompleted {
			get {
				if(_result == null)
					return 0;
				return _result.Time;
			}
		}

		I_Renderable _target;
		public I_Renderable Target {
			get {
				return _target;
			}
		}

		public RenderRequest(I_Renderable target, long time):base() {
			_target = target;
			_time = time;
		}

		public Vector2d RenderTargetSize {
			get {
				return _target.RenderTargetSize;
			}
		}

		Subscriptions<RenderEvent> _subscriptions;

		public void Subscribe(I_subscriber<RenderEvent> s) {
			if(_subscriptions == null)
				_subscriptions = new Subscriptions<RenderEvent>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<RenderEvent> s) {
			if(_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}

		I_RenderResult _result;
		public I_RenderResult Result {
			get {
				return _result;
			}
		}
		public void SetResult(I_RenderResult result) {
			_result = result;
			if(_subscriptions == null)
				return;
			RenderEvent e = new RenderEvent(this);
			_subscriptions.ReceiveEvent(e);
		}
	}
}
