using MauronAlpha.Geometry._2d;
using MauronAlpha.GameEngine.Rendering;

namespace MauronAlpha.GameEngine.Positioning {
	
	public class GamePositionData : MauronCode_dataobject,I_GameComponent {
		public I_Drawable Source;

		public GamePositionData(I_Drawable source) {
			Source=source;
		}

		public Vector2d TopLeft { get { return Source.Bounds.Points[0]; } }
		public Vector2d Center { get { return Source.Bounds.Center; } }
		public Vector2d TopRight { get { return Source.Bounds.Points[2]; } }
		public Vector2d BottomLeft { get { return Source.Bounds.Points[3]; } }
		public Vector2d BottomRight { get { return Source.Bounds.Points[4]; } }

	}

}
