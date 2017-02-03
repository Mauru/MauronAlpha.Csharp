namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	
	using MauronAlpha.Geometry.Geometry2d.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework;

	using MauronAlpha.MonoGame.Rendering.Collections;

	public class PreRenderable_Outline:PreRenderable {
		public PreRenderable_Outline(GameManager game, I_polygonShape2d shape, Color color)	: base(game) {
			_orders = new RenderOrders() {
				new PreRenderProcess(game,"Lines"+Id, shape, color, RenderTypes.Lines),
			};
			_bounds = shape.Bounds;
		}
		RenderOrders _orders;
		public override RenderOrders RenderOrders {
			get {
				return _orders;
			}
		}
		Polygon2dBounds _bounds;
		public override Polygon2dBounds Bounds {
			get { return _bounds; }
		}

	}

}
