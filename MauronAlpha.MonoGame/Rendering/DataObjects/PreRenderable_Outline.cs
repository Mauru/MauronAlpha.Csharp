namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	
	using MauronAlpha.Geometry.Geometry2d.Interfaces;
	using MauronAlpha.Geometry.Geometry2d.Collections;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework;

	using MauronAlpha.MonoGame.Rendering.Collections;

	public class PreRenderable_Outline:PreRenderable {

		Color _color;
		LineBuffer _lines;

		public PreRenderable_Outline(GameManager game, I_polygonShape2d shape, Color color)	: base(game) {
			Segment2dList segments = shape.Segments;
			_lines = new LineBuffer(segments);
			_color = color;

			_orders = new RenderOrders() {
				new PreRenderOrder(_lines, color),
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
