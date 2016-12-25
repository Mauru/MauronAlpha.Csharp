namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	/// <summary> Holds Composite (combined) Renderables </summary>
	public class CompositeBuffer : List<RenderComposite> {

		bool _isBusy = false;
		public bool IsBusy { get { return _isBusy; } }
		public void SetIsBusy(bool state) {
			_isBusy = state;
		}

		int _index = -1;
		public int Index { get { return _index; } }
		RenderComposite _current;
		
		/// <summary>Sets the current active element of a compositeBuffer</summary>
		public bool TryAdvanceQueue(ref RenderComposite composite) {
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
	
	}
}