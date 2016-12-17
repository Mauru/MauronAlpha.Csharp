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

		public GameScene(GameManager game) : base() {
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

		public abstract GameRenderer.DrawMethod DrawMethod { get; }

	}

}

