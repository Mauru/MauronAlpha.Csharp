namespace MauronAlpha.MonoGame.Rendering.Utility {
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using MauronAlpha.MonoGame.Rendering.Collections;

	public class CompositeRenderer:MonoGameComponent {

		public static GameRenderer.DrawMethod RenderMethod {
			get {
				return Draw;
			}
		}

		public static void Draw(GameRenderer renderer, long time) {
			I_GameScene scene = renderer.CurrentScene;
			CompositeBuffer buffer = scene.CompositeBuffer;
			if (buffer.IsBusy)
				return;

			RenderComposite composite = null;
			if (!buffer.TryAdvanceQueue(ref composite))
				return;

			buffer.SetIsBusy(true);

			PreRenderChain chain = composite.Chain;

			if (chain.IsBusy)
				return;

			PreRenderProcess process = null;
			if (!chain.TryAdvanceQueue(ref process))
				return;

			chain.SetIsBusy(true);

			ExecuteProcess(renderer,process,time);
			




		}


		//Dealing with PreRenderProcesses
		public static void ExecuteProcess(GameRenderer renderer, PreRenderProcess process, long time) {

		}

	}
}

namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	public class CompositeBuffer : List<RenderComposite> {

		bool _isBusy = false;
		public bool IsBusy { get { return _isBusy; } }
		public void SetIsBusy(bool state) {
			_isBusy = state;
		}

		int _index = -1;
		RenderComposite _current;
		public bool TryAdvanceQueue(ref RenderComposite composite) { 
			if (_isBusy)
				return false;
			//start queue
			if(_current == null) {
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
	}
}
