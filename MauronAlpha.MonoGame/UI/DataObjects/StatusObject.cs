namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.MonoGame.Utility;
	using MauronAlpha.MonoGame.Geometry;
	using MauronAlpha.Geometry.Geometry2d.Units;

	public class StatusObject:UIElement {
		PolyShape _shape;

		public StatusObject(GameManager game) : base(game) {
			_shape = new PolyTriangle(Game, new Vector2d(0, 60));
		}

		public override GameRenderer.RenderMethod RenderMethod {
			get { return ShapeRenderer.RenderShapeInWorldSpace; }
		}

		public override Vector2d SizeAsVector2d {
			get {
				return _shape.Size;
			}
		}

		public override System.Type RenderPresetType {
			get { return typeof(StatusObject); }
		}

		public override MonoGame.Interfaces.I_MonoShape AsMonoShape() {
			return _shape;
		}
	}
}
