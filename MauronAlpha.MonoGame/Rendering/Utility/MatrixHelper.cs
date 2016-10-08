namespace MauronAlpha.MonoGame.Rendering {

	using Microsoft.Xna.Framework;
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry3d.Units;

	using MauronAlpha.MonoGame.Rendering.Interfaces;
	

	public class MatrixHelper:MonoGameComponent {

		public static Matrix MonoGameMatrix {
			get { return Matrix.Identity; }
		}

		//Helper functions
		public static Matrix PositionOfRenderableInA2dGameWindow(I_Renderable target) {
			Vector2d position = target.Position;
			Vector3 in3dSpace = ToVector3(position);

			return Matrix.CreateWorld(in3dSpace, new Vector3(1, 0, 0), new Vector3(0, 0, 1));
		}

		/// <summary> Assumes the Projection Matrix is the Size of the GameWindow </summary>
		public static Matrix CameraTo2dProjectionMatrix(Camera camera) {
			GameManager game = camera.Game;
		
			//we dont use this but its there if we need it 
			//#ProjectionParameters data = camera.CustomProjectionData;
			Vector2d windowSize = game.Engine.GameWindow.SizeAsVector2d;
			Matrix result = Matrix.CreateOrthographic(windowSize.FloatX,windowSize.FloatY,1f,1000f);
		
			return result;
		}
		/// <summary> Assumes the View Matrix is the Size of the GameWindow </summary>
		public static Matrix CameraTo2dViewMatrix(Camera camera) {
			GameManager game = camera.Game;
			//we dont use this but its there if we need it 
			//#ProjectionParameters data = camera.CustomProjectionData;
			Matrix result = Matrix.CreateLookAt(ToVector3(camera.Position), ToVector3(camera.Target), new Vector3(0, 0, 1));
			return result;
		}
		
		public static Vector3 ToVector3(Vector2d v) {
			return new Vector3(v.FloatX, v.FloatY, 0);
		}
		public static Vector3 ToVector3(Vector3d v) {
			return new Vector3(v.FloatX, v.FloatY, v.FloatZ);
		}

	}

}
