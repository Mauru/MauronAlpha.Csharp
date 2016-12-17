namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.Geometry.Geometry2d.Interfaces;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Collections;

	public class PreRenderCall:MonoGameComponent, I_sender<Event_PreRenderCallComplete> {

		ShapeBuffer _shapes;
		ShapeBuffer Shapes { get { return _shapes; } }

		string _name;
		string Name {
			get {
				return _name;
			}
		}

		public PreRenderCall(I_polygonShape2d shape, string name): base() {
			_shapes = new ShapeBuffer(shape);
			_name = name;
		}
		
		RenderResult _result;
		public RenderResult Result {
			get {
				return _result;
			}
		}
		void SetRenderResult(RenderResult result) {
			_result = result;
			if (_subscriptions != null)
				_subscriptions.ReceiveEvent(new Event_PreRenderCallComplete(this));
		}

		Subscriptions<Event_PreRenderCallComplete> _subscriptions;
		public void Subscribe(I_subscriber<Event_PreRenderCallComplete> s) {
			if (_subscriptions == null)
				_subscriptions = new Subscriptions<Event_PreRenderCallComplete>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<Event_PreRenderCallComplete> s) {
			if (_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}
	}

	public class Event_PreRenderCallComplete : EventUnit_event {
		PreRenderCall _target;
		public PreRenderCall Target { get { return _target; } }

		public Event_PreRenderCallComplete(PreRenderCall target) : base("Complete") {
			_target = target;
		}
	}
}
