namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;
	
	public class RenderComposite:MonoGameComponent, I_subscriber<PreRenderChainComplete>, I_sender<RenderCompositeComplete> {

		PreRenderChain _composites;
		/// <summary>A chain of renderables which interact with each other</summary>
		public PreRenderChain Chain { get { return _composites; } }

		BlendMode _blendMode;

		GameManager _game;
		public GameManager Game { get { return _game; } }


		public RenderComposite(GameManager game, PreRenderChain chain, BlendMode mode) : base() {
			_composites = chain;
			_blendMode = mode;
			_game = game;
		}

		bool _usesEvents = true;

		// Render-Results
		I_RenderResult _result;
		public I_RenderResult Result { get { return _result; } }
		void SetResult(I_RenderResult result) {
			_result = result;
			if (_usesEvents && _subscriptions != null)
				_subscriptions.ReceiveEvent(new RenderCompositeComplete(this));
		}
		public bool HasResult {
			get {
				if (_result == null)
					return false;
				return _result.HasResult;
			}
		}


		// Events
		public bool ReceiveEvent(PreRenderChainComplete e) {
			PreRenderChain chain = e.Target;
			I_RenderResult result;
			foreach(PreRenderProcess process in chain)
				result = process.Result;
			return true;			
		}
		public bool Equals(I_subscriber<PreRenderChainComplete> other) {
			return Id.Equals(other.Id);
		}
		Subscriptions<RenderCompositeComplete> _subscriptions;
		public void Subscribe(I_subscriber<RenderCompositeComplete> s) {
			if (_subscriptions == null)
				_subscriptions = new Subscriptions<RenderCompositeComplete>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<RenderCompositeComplete> s) {
			if (_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}
	}

	public class RenderCompositeComplete : EventUnit_event {
		RenderComposite _target;
		RenderComposite Target { get { return _target; } }
		public RenderCompositeComplete(RenderComposite target): base("Complete") {
			_target = target;
		}
	}
}
