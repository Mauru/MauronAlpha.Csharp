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
				if (_shapes != null)
					return _shapes;

				_shapes = new ShapeBuffer();
				return _shapes;
			}
		}
		public virtual void SetShapeBuffer(ShapeBuffer buffer) {
			_shapes = buffer;
		}

		SpriteBuffer _sprites;
		public virtual SpriteBuffer SpriteBuffer {
			get {
				if (_sprites != null)
					return _sprites;
				_sprites = new SpriteBuffer();
				return _sprites;
			}
		}
		public virtual void SetSpriteBuffer(SpriteBuffer buffer) {
			_sprites = buffer;
		}

		LineBuffer _lines;
		public virtual LineBuffer LineBuffer {
			get {
				if (_lines != null)
					return _lines;
				_lines = new LineBuffer();
				return _lines;
			}
		}
		public virtual void SetLineBuffer(LineBuffer buffer) {
			_lines = buffer;
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

