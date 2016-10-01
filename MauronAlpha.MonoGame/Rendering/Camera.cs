namespace MauronAlpha.MonoGame.Rendering {

	using MauronAlpha.Geometry.Geometry3d.Units;
	using MauronAlpha.Geometry.Geometry2d.Units;
	
	using Microsoft.Xna.Framework;

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
		public Matrix TopDownView {
			get {
				if(_view == null)
					_view = MatrixHelper.CameraTo2dViewMatrix(this);
				return _view;
			}
		}
		Matrix _projection;
		public Matrix TopDownProjection {
			get {
				if(_projection == null)
					_projection = MatrixHelper.CameraTo2dProjectionMatrix(this);
					return _view;
			}
		}
		public Matrix WorldMatrixOfRenderable(I_Renderable obj) {
			return MatrixHelper.PositionOfRenderableInA2dGameWindow(obj);
		}
	
	}
}
