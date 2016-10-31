namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.MonoGame.UI.Interfaces;
	using MauronAlpha.MonoGame.UI.Collections;
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.TextProcessing.Units;

	public class GameWindow:UIElement {
		public override bool CanHaveChildren { get { return true; } }
		public override bool CanHaveParent { get { return false; } }

		public GameWindow(GameManager game) : base(game) {}

		Vector2d _measuredSize;
		public override Vector2d SizeAsVector2d {
			get {
				if(_measuredSize == null)
					_measuredSize = new Vector2d(Game.Engine.Window.ClientBounds.Width, Game.Engine.Window.ClientBounds.Height);
				return _measuredSize;
			}
		}
		public override Polygon2dBounds Bounds {
			get { return Game.Engine.GameWindow.Bounds; }
		}



	}

}

