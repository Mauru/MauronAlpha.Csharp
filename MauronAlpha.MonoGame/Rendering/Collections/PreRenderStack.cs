namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;
	using MauronAlpha.Events.Units;

	public class PreRenderStack:List<PreRenderCall>, I_subscriber<Event_PreRenderCallComplete>, I_sender<Event_PreRenderStackComplete> {
		int _rendered = 0;

		public new void Add(PreRenderCall call) {
			call.Subscribe(this);
		}

		public bool ReceiveEvent(Event_PreRenderCallComplete e) {
			_rendered++;
			if (_rendered >= Count)
				_subscriptions.ReceiveEvent(new Event_PreRenderStackComplete(this));
			return true;
		}
		public bool Equals(I_subscriber<Event_PreRenderCallComplete> other) {
			return Id.Equals(other.Id);
		}

		Subscriptions<Event_PreRenderStackComplete> _subscriptions;
		public void Subscribe(I_subscriber<Event_PreRenderStackComplete> s) {
			if (_subscriptions == null)
				_subscriptions = new Subscriptions<Event_PreRenderStackComplete>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<Event_PreRenderStackComplete> s) {
			if (_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}
	}

	public class Event_PreRenderStackComplete : EventUnit_event {
		PreRenderStack _target;
		public PreRenderStack Target { get { return _target; } }
		public Event_PreRenderStackComplete(PreRenderStack target) : base("Complete") {
			_target = target;
		}
	}
}
