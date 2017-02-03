namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Events;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.MonoGame.Rendering.Utility;

	public class CompositeRenderRequest : MonoGameComponent, I_sender<CompositeRenderRequestComplete>, I_subscriber<PreRenderProcessComplete> {

		public CompositeRenderRequest(GameManager game, RenderComposite composite) : base() {
			_composite = composite;
			_game = game;
		}

		GameManager _game;
		public GameManager Game { get { return _game; } }

		RenderComposite _composite;
		Subscriptions<CompositeRenderRequestComplete> _subscriptions;
		public void Subscribe(I_subscriber<CompositeRenderRequestComplete> s) {
			if (_subscriptions == null)
				_subscriptions = new Subscriptions<CompositeRenderRequestComplete>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<CompositeRenderRequestComplete> s) {
			if (_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}

		int _index = -1;
		PreRenderProcess _current;

		public void Cycle() {
			PreRenderChain chain = _composite.Chain;
			_index++;
			PreRenderProcess process = null;
			if (!chain.TryIndex(_index, ref process)) {
				_subscriptions.ReceiveEvent(new CompositeRenderRequestComplete(this));
				return;
			}

			GameRenderer renderer = Game.Renderer;
			process.Subscribe(this);
			PreRenderer.ExecutePreRenderProcess(renderer, process, renderer.Time);
		}

		public void Finalize() {
			PreRenderChain chain = _composite.Chain;

			GameRenderer renderer = Game.Renderer;

			RenderStage stage = renderer.DefaultRenderTarget;

		}
		public bool ReceiveEvent(PreRenderProcessComplete e) {
			PreRenderProcess process = e.Target;
			process.UnSubscribe(this);
			Cycle();
			return true;
		}
		public bool Equals(I_subscriber<PreRenderProcessComplete> other) {
			return Id.Equals(other.Id);
		}
	}


}
namespace MauronAlpha.MonoGame.Rendering.Events {
	using MauronAlpha.Events.Units;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	public class CompositeRenderRequestComplete:EventUnit_event {
		CompositeRenderRequest _target;
		public CompositeRenderRequest Target { get { return _target; } }
		public CompositeRenderRequestComplete(CompositeRenderRequest target): base("Complete") {
			_target = target;
		}
	}
}
