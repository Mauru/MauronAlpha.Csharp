namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Events;

	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;

	/// <summary>A cmbination of several preRenderables combines with a blendMode</summary>
	public class RenderComposite:MonoGameComponent, I_PreRenderable {

		PreRenderChain _chain;
		/// <summary>A chain of renderables which interact with each other</summary>
		public PreRenderChain Chain { get { return _chain; } }

		SpriteBuffer _buffer;
		public SpriteBuffer SpriteBuffer {
			get {
				return _buffer;
			}
		}

		BlendMode _blendMode;
		public BlendMode BlendMode {
			get { return _blendMode; }
		}

		GameManager _game;
		public GameManager Game { get { return _game; } }

		public RenderComposite(GameManager game, PreRenderChain chain, BlendMode mode) : base() {
			_chain = chain;
			_blendMode = mode;
			_game = game;
		}

		public void SynchToSpriteBuffer(PreRenderProcess process) {

			PreRenderedTexture texture = PreRenderedTexture.FromPreRenderProcess(Game.Renderer,process);
			SpriteData data = new SpriteData(texture);

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

		public RenderOrders RenderOrders {
			get { throw new System.NotImplementedException(); }
		}

		Polygon2dBounds _bounds;
		public Polygon2dBounds Bounds {
			get {
				if (_bounds == null)
					_bounds = RenderComposite.GenerateBounds(this);
				return _bounds;
			}
		}

		public static Polygon2dBounds GenerateBounds(RenderComposite obj) {
			Polygon2dBounds result = null;
			foreach (PreRenderProcess p in obj.Chain) {
				if (result == null)
					result = p.Bounds.Copy;
				else
					result = Polygon2dBounds.Combine(p.Bounds, result);
			}
			return result;
		}


	}
}
