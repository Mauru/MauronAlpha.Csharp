namespace MauronAlpha.MonoGame.DataObjects {
	using Microsoft.Xna.Framework;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	public class MonoGameWindow :MonoGameComponent {

		GameWindow _window;
		public MonoGameWindow(GameWindow window): base() {
			_window = window;
		}

		public Polygon2dBounds Bounds {
			get {
				Rectangle bounds = _window.ClientBounds;
				return new Polygon2dBounds(bounds.Width, bounds.Height);
			}
		}

		public Vector2d SizeAsVector2d {
			get {
				Rectangle bounds = _window.ClientBounds;
				return new Vector2d(bounds.Width, bounds.Height);
			}
		}

		public Polygon2d AsPolygon {
			get { 
				Rectangle bounds = _window.ClientBounds;
				return new Rectangle2d(0,0,bounds.Width,bounds.Height); 
			}
		}

	}

}