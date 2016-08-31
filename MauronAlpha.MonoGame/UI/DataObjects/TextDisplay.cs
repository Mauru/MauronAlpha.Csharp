namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

	using MauronAlpha.MonoGame.DataObjects;

	public class TextDisplay :UIElement {

		public TextDisplay(GameManager game) : base(game) {
			_text = new GameText(game.Assets.DefaultFont);
		}

		GameText _text;
		public Lines Lines { get { return _text.Lines; } }

		public override MauronAlpha.Geometry.Geometry2d.Units.Polygon2dBounds Bounds {
			get { throw new System.NotImplementedException(); }
		}


	}
}

namespace MauronAlpha.MonoGame.UI.DataObjects {

	public class VisualStyle :UIComponent {

		Spacing _padding;
		int margin;

	}

	public class Spacing :UIComponent {

		Vector _topLeft = new Vector();
		Vector _bottomRight = new Vector();

	}
}
