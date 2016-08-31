namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.MonoGame.UI.Interfaces;
	using MauronAlpha.MonoGame.UI.Collections;
	using MauronAlpha.MonoGame.Interfaces;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.TextProcessing.Units;

	public class GameWindow:UIElement {
		public override bool CanHaveChildren { get { return true; } }
		public override bool CanHaveParent { get { return false; } }

		public GameWindow(GameManager game) : base(game) {}

		public Vector2d WindowSize {
			get {
				return Game.Engine.GameWindow.SizeAsVector2d;
			}
		}
		public override Polygon2dBounds Bounds {
			get { return Game.Engine.GameWindow.Bounds; }
		}

		public override List<Polygon2d> Shapes {
			get { 
				return new List<Polygon2d>();
			}
		}

		public override List<MonoGameTexture> Sprites {
			get { return new List<MonoGameTexture>(); }
		}

		MonoGameTexture _rendered;
		public override MonoGameTexture Rendered {
			get {
				if(_rendered == null)
					return Game.Renderer.UnrenderedTexture;
				return _rendered;
			}
		}
	}

}

