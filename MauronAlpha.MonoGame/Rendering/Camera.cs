namespace MauronAlpha.MonoGame.Rendering {

	using MauronAlpha.Geometry.Geometry3d.Units;
	using MauronAlpha.Geometry.Geometry2d.Units;
	
	using Microsoft.Xna.Framework;

	using MauronAlpha.MonoGame.Rendering.Interfaces;

	public class Camera:MonoGameComponent {

		GameManager _game;
		public GameManager Game { get { return _game; } }

		string _name;
		public string Name { get { return _name; } }

		public Camera(GameManager game, string name): base() {
			_game = game;
			_name = name;
			SetUpCamera();
		}
		public Camera(GameManager game, string name, Matrix view, Matrix projection, Matrix world): base() {
			_game = game;
			_name = name;
			_view = view;
			_projection = projection;
			_world = world;
		}

		void SetUpCamera() {
			Vector2d center = Game.Engine.GameWindow.Center;
			_position = new Vector3d(center, 100);
			_focus = new Vector3d(center, 0);	
		}

		Vector3d _position = new Vector3d();
		public Vector3d Position {
			get {
				return _position;
			}
		}
		
		Vector3d _focus = new Vector3d();
		public Vector3d Target {
			get {
				return _focus;
			}
		}

		Matrix _view;
		public Matrix ViewMatrix { get { return _projection; } }
		
		Matrix _projection;
		public Matrix ProjectionMatrix { get { return _projection; } }

		Matrix _world;
		public Matrix WorldMatrix { get { return _world; } }
	
	}
}
