namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.FontParser.DataObjects;

	/// <summary> Represents a text-object. </summary>
	public class GameText:MonoGameComponent {

		public GameText() : base() { }
		public GameText(GameFont font) : this() {
			_font = font;
		}
		public GameText(GameFont font, Text text) : this() {
			_text = text;
			_font = font;
		}

		GameFont _font;
		public GameFont Font { get { return _font; } }
		public void SetFont(GameFont font) {
			_font = font;
		}
		public bool HasFont { get { return _font != null; } }
		public bool FontHasLoaded {
			get {
			if(_font == null)
				return false;
			return _font.HasLoaded;
			}
		}

		public Lines Lines {
			get {
				if(_text == null)
					return new Lines();
				return _text.Lines;
			}
		}

		Text _text;
		public Text Text {
			get { return _text; }
		}
		public void SetText(Text text) {
			_text = text;
		}
		public bool HasText { get { return _text != null; } }

		public void SetText(string text) {
			_text = new Text(text);
		}
		
		double LineWidth(Line l) {
			if(_font == null)
				return 0;

			double result = 0;
			Characters cc = l.Characters;
			foreach(Character c in cc)
				result += MeasureCharacterWidth(c);
			return result;
		}
		Vector2d CalculateSize() {
			if(_text == null || _font == null)
				return new Vector2d();

			Vector2d v = new Vector2d();
			Lines ll = _text.Lines;
			int lcount = 0;
			foreach(Line l in ll) {
				lcount++;
				Characters cc = l.Characters;
				foreach(Character c in cc)
					v.Add(MeasureCharacterWidth(c), 0);
			}

			v.SetY(lcount * (CharacterHeight+_lineSpacing));
			return v;
		}

		int _lineSpacing = 1;
		double CharacterHeight {
			get {
				if(_font == null)
					return 0;
				return _font.CharacterHeight;
			}
		}
		double MeasureCharacterWidth(Character c) {
			if(c.IsLineBreak || c.IsParagraphBreak)
				return 0;
			PositionData d = _font.FetchCharacterData(c.Symbol);

			return d.Width;
		}
		
	}

}
