namespace MauronAlpha.MonoGame.Rendering.Collections {

	using Microsoft.Xna.Framework;

	/// <summary> ColorLibrary representing the status of the GameRenderer </summary>
	public class RenderStatusColors:MonoGameComponent {

		public static Color Undefined {
			get {
				return Color.Blue;
			}
		}

		public static Color Busy {
			get {
				return Color.Yellow;
			}
		}

		public static Color Failed {
			get {
				return Color.Red;
			}
		}

		public static Color Ready {
			get { return Color.Green; }
		}

	}
}
