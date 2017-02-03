namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Interfaces;

	using MauronAlpha.MonoGame.Rendering.Collections;

	using Microsoft.Xna.Framework;

	public class PreRenderable_Shape : PreRenderable {

		I_polygonShape2d _shape;

		public PreRenderable_Shape(GameManager game, I_polygonShape2d shape, I_Shader shader, Color color): base(game) {
			_shape = shape;
			_orders = new RenderOrders() {
				new PreRenderProcess(game,"Shape"+Id,shape,shader,color),
			};
		}

		RenderOrders _orders;
		public override RenderOrders RenderOrders {
			get {
				return _orders;
			}
		}

		public override Polygon2dBounds Bounds {
			get { return _shape.Bounds; }
		}

	}
}