using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Positioning {
	public interface I_Positionable {
		Rectangle2d Bounds {get;} 
		void SetPosition(Vector2d v);
		Vector2d Position { get; }
		void SetBounds(Rectangle2d rect);
	}
}
