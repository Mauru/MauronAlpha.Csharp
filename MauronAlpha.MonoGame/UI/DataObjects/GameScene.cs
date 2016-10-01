namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.MonoGame.Rendering;

	public class UIInfo :Polygon2dBounds {
		public UIInfo() : base(0,0) { }
	}

	/// <summary> Gives a renderer Information on what to render </summary>
	public abstract class GameScene :MonoGameComponent, I_GameScene {

		GameManager _game;
		public GameManager Game {
			get { return _game; }
		}

		List<I_Renderable> _children = new List<I_Renderable>();
		public List<I_Renderable> Children { get { return _children; } }
		public void AddChild(I_Renderable r) {
			_children.Add(r);
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

		public abstract void RequestRender();

	}

}

