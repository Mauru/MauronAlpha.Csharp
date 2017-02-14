namespace MauronAlpha.MonoGame.DataObjects {

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public class MonoGameWindow :MonoGameComponent, I_sender<WindowSizeChangedEvent> {

		GameManager _game;
		public GameManager Game {
			get { return _game; }
		}

		GameWindow _window;
		public MonoGameWindow(GameManager game, GameWindow window): base() {
			_game = game;
			_window = window;
			Rectangle bounds = _window.ClientBounds;
			_size = new Vector2d(bounds.X,bounds.Y);
		}

		Vector2d _size = new Vector2d();
		public Vector2d Size { get { return _size; } }

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
				return Rectangle2d.CreateAlignTopLeft(bounds.Width,bounds.Height); 
			}
		}

		public Vector2d Center {
			get {
				GraphicsDevice d = Game.Engine.GraphicsDevice;
				Rectangle bounds = d.Viewport.Bounds;

				Vector2d result = new Vector2d(
					bounds.Width / 2,
					bounds.Height / 2
				);
				return result;
			}
		}

		public Vector2 CenterAsVector2 {
			get {
				Vector2d c = Center;
				return new Vector2(c.FloatX, c.FloatY);
			}
		}

		public void OnWindowResize(object sender, System.EventArgs arguments) {
			Vector2d old = _size;
			Rectangle newSize = _window.ClientBounds;
			_size = new Vector2d(newSize.Width,newSize.Height);
			_subscriptions.ReceiveEvent(new WindowSizeChangedEvent(this, old, _size));
		}
		Subscriptions<WindowSizeChangedEvent> _subscriptions;
		public void Subscribe(I_subscriber<WindowSizeChangedEvent> s) {
			if(_subscriptions == null)
				_subscriptions = new Subscriptions<WindowSizeChangedEvent>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<WindowSizeChangedEvent> s) {
			if(_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}
	
	}
}

namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.Events.Units;

	using MauronAlpha.Geometry.Geometry2d.Units;

	public class WindowSizeChangedEvent :EventUnit_event {

		Vector2d _old;
		Vector2d _new;

		MonoGameWindow _window;

		public WindowSizeChangedEvent(MonoGameWindow window, Vector2d oldSize, Vector2d newSize) : base("Resized.") {
			_old = oldSize;
			_new = newSize;
			_window = window;
		}
	}

}