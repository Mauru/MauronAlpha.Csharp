namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Rendering;

	using MauronAlpha.MonoGame.Utility;
	using MauronAlpha.Geometry.Geometry2d.Units;

	public class TextDisplay :UIElement,I_Renderable {

		public TextDisplay(GameManager game, GameFont font) : base(game) {
			_text = new GameText(font);
		}
		public TextDisplay(GameManager game, string text, GameFont font) : base(game) {
			_text = new GameText(font, new Text(text));
		}
		public TextDisplay(GameManager game, Text text, GameFont font) : base(game) {
			_text = new GameText(font, text);
		}

		public GameFont Font {
			get {
				return _text.Font;
			}
		}

		GameText _text;
		public GameText Text { get { return _text; } }
		public Lines Lines { get { return _text.Lines; } }

		Vector2d _sizeAsVector2d;
		public override Vector2d SizeAsVector2d {
			get {
				if(_text == null)
					return Vector2d.Zero;
				if(!_text.HasFont)
					return Vector2d.Zero;
				if(!_text.Font.HasDefinition)
					return Vector2d.Zero;
				if(!NeedsRenderUpdate)
					_sizeAsVector2d = RenderResult.ActualObjectSize;
				else
					_sizeAsVector2d = MeasureTextSizeAsVector2d(this);
				
				return _sizeAsVector2d;	

			}
		}

		public static Vector2d MeasureTextSizeAsVector2d(TextDisplay display) {

			Text text = display.Text.Text;
			GameFont font = display.Text.Font;
			Lines ll = text.Lines;

			Vector2d finalSize = Vector2d.Zero;

			Vector2d lineSize;
			foreach(Line l in ll) {
				finalSize.Add(0,font.LineHeight + display.VerticalSpaceBetweenLines);
				 lineSize = font.MeasureLine(l);
				 if(lineSize.X > finalSize.X)
					 finalSize.SetX(lineSize.X);
			}
			return finalSize;

		}



		public double LineHeight {
			get {
				return Font.LineHeight + VerticalSpaceBetweenLines;
			}
		}

		double _verticalSpaceBetweenLines = 0;
		double VerticalSpaceBetweenLines { get { return _verticalSpaceBetweenLines; } }

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
