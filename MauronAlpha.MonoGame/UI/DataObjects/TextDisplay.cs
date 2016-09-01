namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Rendering;

	using MauronAlpha.MonoGame.Utility;

	public class TextDisplay :UIElement,I_Renderable {

		public TextDisplay(GameManager game) : base(game) {
			_text = new GameText(game.Assets.DefaultFont);
		}
		public TextDisplay(GameManager game, GameFont font) : this(game) {
			_text = new GameText(font);
		}

		public GameFont Font {
			get {
				return _text.Font;
			}
		}

		GameText _text;
		public Lines Lines { get { return _text.Lines; } }

		VisualStyle _style = new VisualStyle();
		public VisualStyle Style { get { return _style; } }

		public void SetText(string text) {
			_text.SetText(text);
			NeedRenderUpdate(Game.Renderer.Time);
		}

		public override GameRenderer.RenderMethod RenderMethod {
			get { return TextRenderer.RenderMethod; }
		}

		public override System.Type RenderPresetType {
			get {	return typeof(TextDisplay); }
		}
	}
}

namespace MauronAlpha.MonoGame.UI.DataObjects {

	public class VisualStyle :UIComponent {

		Spacing _padding = new Spacing();
		public Spacing Padding {
			get { return _padding; }
		}

		Spacing _margin = new Spacing();
		public Spacing Margin {
			get { return _margin; }
		}

		public bool Equals(VisualStyle other) {
			return Padding.Equals(other.Padding) && Margin.Equals(other.Margin);
		}
	}


}

namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;

	public class Spacing :UIComponent {

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
