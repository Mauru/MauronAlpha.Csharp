namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.Geometry.Geometry2d.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Transformation;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Collections;


	public class PreRenderProcess: MonoGameComponent, I_sender<PreRenderProcessComplete> {

		GameManager _game;
		public PreRenderProcess(GameManager game, string name, I_polygonShape2d shape) : base() {
			_name = name;
			_game = game;
			_matrix = Matrix2d.Identity;
			_position = Vector2d.Zero;
		}
		public PreRenderProcess(GameManager game, string name, I_polygonShape2d shape, Vector2d position): base() {
			_name = name;
			_game = game;
			_matrix = Matrix2d.Identity;
			_position = position;
		}

		string _name;
		public string Name { get { return _name; } }

		Vector2d _position;
		Matrix2d _matrix;
		I_polygonShape2d _shape;
		public I_polygonShape2d Shape {
			get {
				return _shape;
			}
		}

		public string RenderType {
			get {
				if(_shape!=null)
					return RenderTypes.Shape;
				return RenderTypes.Error;
			}
		}

		bool _usesEvents = true;

		I_RenderResult _result;
		public I_RenderResult Result { get { return _result; } }
		void SetResult(I_RenderResult result) {
			_result = result;
			if (_subscriptions != null && _usesEvents)
				_subscriptions.ReceiveEvent(new PreRenderProcessComplete(this));
		}

		Subscriptions<PreRenderProcessComplete> _subscriptions;
		public void Subscribe(I_subscriber<PreRenderProcessComplete> s) {
			if (_subscriptions == null)
				_subscriptions = new Subscriptions<PreRenderProcessComplete>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<PreRenderProcessComplete> s) {
			if (_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}
	}

	public class PreRenderProcessComplete:EventUnit_event {

		PreRenderProcess _target;
		public PreRenderProcess Target { get { return _target; } }
		public PreRenderProcessComplete(PreRenderProcess target):base("Complete") {
			_target = target;
		}

	}
}
