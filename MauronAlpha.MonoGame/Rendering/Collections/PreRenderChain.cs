namespace MauronAlpha.MonoGame.Rendering.Collections {

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.Geometry.Geometry2d.Interfaces;

	/// <summary> A combination of PreRenderProcesses </summary>
	public class PreRenderChain:List<PreRenderProcess> {

		I_subscriber<PreRenderProcessComplete> _eventRelay;

		GameManager _game;
		public GameManager Game { get { return _game; } }

		public PreRenderChain(GameManager game):base() {
			_game = game;
		}
		public PreRenderChain(GameManager game, I_subscriber<PreRenderProcessComplete> listener):base() {
			_game = game;
			_eventRelay = listener;
		}

		public override HandlingData.MauronCode_dataList<PreRenderProcess> Add(PreRenderProcess obj) {
			obj.Prepare();
			if (_eventRelay != null)
				obj.Subscribe(_eventRelay);
			return base.Add(obj);
		}
	}
}
