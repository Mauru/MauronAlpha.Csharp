namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	using MauronAlpha.Events.Interfaces;

	using MauronAlpha.Geometry.Geometry2d.Units;

	/// <summary> Holds Composite (combined) Renderables and information on their renderstate </summary>
	public class CompositeBuffer : List<RenderComposite>, I_PreRenderableCollection {

		Polygon2dBounds _bounds;
		public Polygon2dBounds Bounds {
			get {
				if (_bounds == null)
					_bounds = GenerateBounds(this);
				return _bounds;
			}
		}
		public void SetBounds(Polygon2dBounds _bounds) {
			_bounds = Bounds;
		}
		public static Polygon2dBounds GenerateBounds(CompositeBuffer buffer) {
			Polygon2dBounds result = null;
			foreach (RenderComposite obj in buffer) {
				if (result == null)
					result = obj.Bounds;
				else
					result = Polygon2dBounds.Combine(result, obj.Bounds);
			}
			return result;
		}

	}
}