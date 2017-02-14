namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Events;

	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;

	/// <summary>A combination of several preRenderables combined with a blendMode</summary>
	public class RenderComposite:MonoGameComponent, I_RenderComposite {

		PreRenderedTextures _textures;
		/// <summary> The different components to be combined </summary>
		public PreRenderedTextures Textures {
			get {
				return _textures;
			}
		}

		CompositePreRenderProcess _instructions;

		string _name;
		string Name {
			get { return _name; }
		}

		PreRenderOrders _preRequisites;

		public RenderComposite(string name, PreRenderOrders preRequisites, CompositePreRenderProcess instructions, Polygon2dBounds bounds) : base() {
			_name = name;
			_instructions = instructions;
			_preRequisites = preRequisites;
			_bounds = bounds;
		}

		// Render-Results
		I_RenderResult _result;
		public I_RenderResult RenderResult { get { return _result; } }
		public void SetRenderResult(I_RenderResult result) {
			_result = result;
		}
		public bool TryRenderResult(ref I_RenderResult result) {
			if (_result == null)
				return false;
			result = _result;
			return true;
		}
		public bool HasResult {
			get {
				if (_result == null)
					return false;
				return _result.HasResult;
			}
		}

		Polygon2dBounds _bounds;
		public Polygon2dBounds Bounds {
			get {
				return _bounds;
			}
		}
	}
}