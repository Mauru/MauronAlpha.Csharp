namespace MauronAlpha.MonoGame.UI.DataObjects {

	using MauronAlpha.Geometry.Geometry2d.Units;

	public class Spacing : UIComponent {

		Vector2d _topLeft = new Vector2d();
		public Vector2d TopLeft {
			get {
				return _topLeft;
			}
		}
		Vector2d _bottomRight = new Vector2d();
		public Vector2d BottomRight {
			get {
				return _bottomRight;
			}
		}

		float Top { get { return _topLeft.FloatY; } }
		float Bottom { get { return _bottomRight.FloatY; } }
		float Left { get { return _topLeft.FloatX; } }
		float Right { get { return _bottomRight.FloatX; } }

		public bool Equals(Spacing other) {
			return BottomRight.Equals(other.BottomRight) && TopLeft.Equals(other.TopLeft);
		}

	}

}