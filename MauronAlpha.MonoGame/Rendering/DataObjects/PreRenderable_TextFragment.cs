namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.MonoGame.UI.DataObjects;

	using Microsoft.Xna.Framework;

	public class PreRenderable_TextFragment : PreRenderable {

		TextFragment _text;

		//constructor
		public PreRenderable_TextFragment(GameManager game, GameFont font, string text, Color color): base(game) {
			_text = new TextFragment(game, font, text);

			SpriteBuffer buffer = TextFragment.GenerateSpriteBuffer(_text);
			_bounds = SpriteBuffer.GenerateBounds(buffer);

			_orders = new RenderOrders() {
				new PreRenderProcess(game, "Text"+Id, buffer, color)
			};
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