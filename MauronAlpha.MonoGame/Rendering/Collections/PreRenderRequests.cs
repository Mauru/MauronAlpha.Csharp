
namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Events;
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	public class PreRenderRequests : MonoGameComponent {

		Stack<ShapeBuffer> _shapes;
		public Stack<ShapeBuffer> Shapes { get { return _shapes; } }
		public bool TryShapes(ref Stack<ShapeBuffer> result) {
			if (_shapes == null)
				return false;
			result = _shapes;
			return true;
		}

		Stack<LineBuffer> _lines;
		public Stack<LineBuffer> Lines { get { return _lines; } }
		public bool TryLines(ref Stack<LineBuffer> result) {
			if (_lines == null)
				return false;
			result = _lines;
			return true;
		}

		Stack<SpriteBuffer> _sprites;
		public Stack<SpriteBuffer> Sprites { get { return _sprites; } }
		public bool TrySprites(ref Stack<SpriteBuffer> result) {
			if (_sprites == null)
				return false;
			result = _sprites;
			return true;
		}

		Stack<CompositeBuffer> _composites;
		public Stack<CompositeBuffer> Composites { get { return _composites; } }
		public bool TryComposites(ref Stack<CompositeBuffer> result) {
			if (_composites == null)
				return false;
			result = _composites;
			return true;
		}

		long _lastDrawCycle = -1;
		public long LastDrawCycle { get { return _lastDrawCycle; } }

		public void Add(CompositeBuffer obj) {
			if (_composites == null)
				_composites = new Stack<CompositeBuffer>();
			_composites.Add(obj);
		}


	}

}