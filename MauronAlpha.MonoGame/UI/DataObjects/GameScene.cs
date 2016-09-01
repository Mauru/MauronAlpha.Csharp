namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	public class UIInfo :Polygon2dBounds {
		public UIInfo() : base(0,0) { }
	}

	/// <summary> Gives a renderer Information on what to render </summary>
	public abstract class GameScene :MonoGameComponent {

		GameManager _game;
		public GameManager Game {
			get { return _game; }
		}

		public GameScene(GameManager game) : base() {
			_game = game;
		}

		public abstract void RequestRender();

	}

}

