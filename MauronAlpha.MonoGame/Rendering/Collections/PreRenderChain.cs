namespace MauronAlpha.MonoGame.Rendering.Collections {

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.Geometry.Geometry2d.Interfaces;

	public class PreRenderChain:List<PreRenderProcess>, I_sender<PreRenderChainComplete> {

		GameManager _game;
		public PreRenderChain(GameManager game):base() {
			_game = game;
		}

		bool _isBusy = false;
		public bool IsBusy { get { return _isBusy; } }
		public void SetIsBusy(bool state) {
			_isBusy = state;
		}
		
		int _index = -1;
		PreRenderProcess _current;
		public bool TryAdvanceQueue(ref PreRenderProcess composite) {
			if (_isBusy)
				return false;
			//start queue
			if (_current == null) {
				_index++;
				if (!TryIndex(_index, ref composite))
					return false;
				_current = composite;
				return true;
			}
			//advance queue
			_index++;
			if (!TryIndex(_index, ref composite))
				return false;
			_current = composite;
			return true;
		}

		public void PrepareShapeBuffers() {
			foreach (PreRenderProcess process in this)
				process.PrepareShapeBuffer();
		}



		//events
		Subscriptions<PreRenderChainComplete> _subscriptions;
		public void Subscribe(I_subscriber<PreRenderChainComplete> s) {
			if (_subscriptions == null)
				_subscriptions = new Subscriptions<PreRenderChainComplete>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<PreRenderChainComplete> s) {
			if (_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}

	}

	public class PreRenderChainComplete : EventUnit_event {
		PreRenderChain _target;
		public PreRenderChain Target { get { return _target; } }
		public PreRenderChainComplete(PreRenderChain target): base("Complete") {
			_target = target;
		}
	}
}
