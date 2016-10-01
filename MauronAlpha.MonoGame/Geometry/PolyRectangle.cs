namespace MauronAlpha.MonoGame.Geometry {
	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Collections;

	public class PolyRectangle : PolyShape {
		public PolyRectangle(GameManager game,float width, float height) : base(game) {
			Rectangle2d rectangle = new Rectangle2d();
			base.SetPoints(new Vector2dList(rectangle.Points));
		}
		public PolyRectangle(GameManager game, Vector2dList points): base(game) {
			base.SetPoints(points);
		}
	}
}