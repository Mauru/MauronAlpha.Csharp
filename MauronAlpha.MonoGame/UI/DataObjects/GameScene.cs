namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	public class UIInfo :Polygon2dBounds {
		public UIInfo() : base(0,0) { }
	}

	/// <summary> Gives a renderer Information on what to render </summary>
	public abstract class GameScene :MonoGameComponent, I_GameScene {

		GameManager _game;
		public GameManager Game {
			get { return _game; }
		}

		public GameScene(GameManager game): base() {
			_game = game;
		}

		bool _initialized = false;
		public bool IsInitialized {
			get { return _initialized; }
		}

		public virtual void Initialize() {
			_initialized = true;
		}
		public virtual void RunLogicCycle(long time) { }


		ShapeBuffer _shapes;
		public virtual ShapeBuffer ShapeBuffer {
			get {
				return _shapes;
			}
		}
		public virtual void SetShapeBuffer(ShapeBuffer buffer) {
			_shapes = buffer;
		}
		public bool HasShapeBuffer {
			get {
				return _shapes != null;
			}
		}
		public virtual bool TryShapeBuffer(ref ShapeBuffer result) { 
			if(_shapes==null)
				return false;
			result = _shapes;
			return true;
		}

		SpriteBuffer _sprites;
		public virtual SpriteBuffer SpriteBuffer {
			get {
				return _sprites;
			}
		}
		public virtual void SetSpriteBuffer(SpriteBuffer buffer) {
			_sprites = buffer;
		}
		public virtual bool HasSpriteBuffer {
			get {
				return _sprites != null;
			}
		}
		public virtual bool TrySpriteBuffer(ref SpriteBuffer result) {
			if (_sprites == null)
				return false;
			result = _sprites;
			return true;
		}


		CompositeBuffer _composites;
		public virtual CompositeBuffer CompositeBuffer {
			get { return _composites; }
		}
		public virtual void SetCompositeBuffer(CompositeBuffer buffer) {
			_composites = buffer;
		}
		public virtual bool HasCompositeBuffer {
			get {
				return _composites != null;
			}
		}
		public virtual bool TryCompositeBuffer(ref CompositeBuffer result) {
			if (_composites == null)
				return false;
			result = _composites;
			return true;
		}


		LineBuffer _lines;
		public virtual LineBuffer LineBuffer {
			get {
				return _lines;
			}
		}
		public virtual void SetLineBuffer(LineBuffer buffer) {
			_lines = buffer;
		}
		public virtual bool HasLineBuffer {
			get {
				return _lines != null;
			}
		}
		public virtual bool TryLineBuffer(ref LineBuffer result) {
			if (_lines == null)
				return false;
			result = _lines;
			return true;
		}


		PreRenderRequests _requests;
		public virtual PreRenderRequests PreRenderRequests {
			get {
				if (_requests == null)
					_requests = new PreRenderRequests();
				return _requests;
			}
		}
		public virtual bool TryPreRenderRequests(ref PreRenderRequests result) {
			if (_requests == null)
				return false;
			result = _requests;
			return true;
		}


		RenderOrders _orders;
		public virtual RenderOrders RenderOrders {
			get {
				return _orders;
			}
		}
		public virtual void SetRenderOrders(RenderOrders orders) {
			_orders = orders;
		}
		public virtual bool HasRenderOrders {
			get {
				return _orders != null;
			}
		}
		public virtual bool TryRenderOrders(ref RenderOrders result) {
			if (_orders == null)
				return false;
			result = _orders;
			return true;
		}

		public abstract GameRenderer.DrawMethod DrawMethod { get; }

		/// <summary> Mostly for debugging and error reports. </summary>
		public virtual void ReceiveStatus(I_RenderStatus status) { }

	}

}

