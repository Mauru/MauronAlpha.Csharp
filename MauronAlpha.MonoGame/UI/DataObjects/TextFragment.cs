namespace MauronAlpha.MonoGame.UI.DataObjects {
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.FontParser.DataObjects;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Utility;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Utility;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public class TextFragment :UIElement,I_Renderable {

		public TextFragment(GameManager game, GameFont font) : base(game) {
			_text = new GameText(font);
		}
		public TextFragment(GameManager game, string text, GameFont font) : base(game) {
			_text = new GameText(font, new Text(text));
		}
		public TextFragment(GameManager game, Text text, GameFont font) : base(game) {
			_text = new GameText(font, text);
		}

		public bool IsEmpty {
			get {
				if (_text == null)
					return true;
				return _text.IsEmpty;
			}
		}

		public GameFont Font {
			get {
				return _text.Font;
			}
		}
		public bool HasFont {
			get {
				if (_text == null)
					return false;
				return _text.HasFont;
			}
		}

		GameText _text;
		public GameText Text { get { return _text; } }
		public void SetText(string text) {
			_text.SetText(text);
			_buffer = null;
		}
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
					_sizeAsVector2d = MeasureText(this);
				
				return _sizeAsVector2d;	

			}
		}

		public Vector2d ParagraphBreakAsVector2d {
			get {
				return new Vector2d(0, LineHeight + TextFormat.ParagraphSpacing);
			}
		}

		public override bool IsPolygon {
			get { return false; }
		}
		public override ShapeBuffer ShapeBuffer {
			get {
				return ShapeBuffer.Empty;
			}
		}

		public double LineHeight {
			get {
				return Font.LineHeight;
			}
		}

		TextFormat _textFormat;
		public TextFormat TextFormat {
			get {
				if (_textFormat == null)
					_textFormat = GenerateDefaultTextFormat(this);
				return _textFormat;
			}
		}

		VisualStyle _style = new VisualStyle();
		public VisualStyle Style { get { return _style; } }

		public override GameRenderer.RenderMethod RenderMethod {
			get { return TextRenderer.RenderMethod; }
		}
		public override System.Type RenderPresetType {
			get {	return typeof(TextFragment); }
		}

		SpriteBuffer _buffer;
		public SpriteBuffer SpriteBuffer {
			get {
				if (_buffer != null)
					return _buffer;
				_buffer = GenerateSpriteBuffer(this);
				return _buffer;
			}
		}

		/// <summary> Measures the size of a Text in a Textdisplay using Font, GameText and TextFormat.</summary>
		public static Vector2d MeasureText(TextFragment display) {
			Text text = display.Text.Text;
			GameFont font = display.Text.Font;
			Lines ll = text.Lines;

			Vector2d result = Vector2d.Zero;



			int index = 0;
			int count = ll.Count;

			Vector2d size;

			foreach (Line l in ll) {

				size = MeasureLine(l, display);
				result.Add(0, size.Y);

				if (index < count - 1 && count > 0)
					size.Add(0, display.TextFormat.LineSpacing);

				if (size.X > result.X)
					result.SetX(size.X);
			}
			return result;
		}
		/// <summary> Returns size of a line using GameFont, GameText and TextFormat (whitespace does not use TextFormat.WordSpacing).</summary>
		public static Vector2d MeasureLine(Line l, TextFragment text) {
			Vector2d result = Vector2d.Zero;
			Words ww = l.Words;

			int index = 0;
			int count = l.Count;

			if (count == 0)
				return Vector2d.Zero;

			if (l.IsParagraphBreak)
				return text.ParagraphBreakAsVector2d;

			foreach (Word w in ww) {
				Vector2d size = MeasureWord(w, text);
				if (index > 0 && index < count - 1 && !w.IsUtility)
					size.Add(text.TextFormat.WordSpacing, 0);
				result.Add(size.X, 0);
				index++;
			}
			return result;
		}
		/// <summary> Returns size of a word using GameFont, GameText and TextFormat.</summary>
		public static Vector2d MeasureWord(Word w, TextFragment d) {
			Vector2d result = Vector2d.Zero;
			Vector2d size;
			int index = 0;
			int count = w.Count;
			foreach (Character c in w.Characters) {
				size = MeasureCharacter(c, d);
				result.Add(size.X, 0);
				//letter spacing
				if (index > 0 && index < count - 1)
					result.Add(d.TextFormat.LetterSpacing);
				index++;
			}
			result.SetY(d.LineHeight);
			return result;
		}
		/// <summary>Returns size of a character using GameFont, GameText and TextFormat (unknown returns size of GameFont.UnknownCharacter).</summary>
		public static Vector2d MeasureCharacter(Character c, TextFragment d) {
			PositionData data = null;
			if (c.IsWhiteSpace) {
				data = d.Font.PositionData(c);
				return new Vector2d(data.XAdvance, d.Font.LineHeight);
			}
			if (c.IsTab) {
				data = d.Font.PositionData(Characters.WhiteSpace);
				return new Vector2d(data.XAdvance, d.Font.LineHeight).Multiply(d.TextFormat.TabLength, 1);
			}

			// what if a character is not in the sprites?
			if (!d.Font.TryPositionData(c, ref data))
				data = d.Font.PositionData(GameFont.UnknownCharacter);

			if (data == null)
				return Vector2d.Zero;

			return new Vector2d(data.Width, data.Height);
		}

		/// <summary> Generates a Default TextFormat </summary>
		public static TextFormat GenerateDefaultTextFormat(TextFragment text) {
			if (text.IsEmpty)
				return GameFont.DefaultTextFormat;
			if (text.HasFont && text.Font.HasLoaded)
				return text.Font.TextFormat;
			return TextFormat.Default;
		}

		public SpriteBuffer RegenerateSpriteBuffer(long time) {
			SpriteBuffer buffer = GenerateSpriteBuffer(this);
			return buffer;
		}

		/// <summary> Generates a SpriteBuffer </summary>
		public static SpriteBuffer GenerateSpriteBuffer(TextFragment text) {

			SpriteBuffer result = new SpriteBuffer();

			Lines ll = text.Lines;

			int index = 0;
			int count = ll.Count;

			Vector2d offset = Vector2d.Zero;

			foreach (Line l in ll) {
				SpriteBuffer sprite = GenerateSpriteBufferOfLine(l, text);
				SpriteBuffer.OffsetPosition(ref sprite, offset);
				result.AddValuesFrom(sprite);

				if (!sprite.IsEmpty)
					offset.Add(0,text.LineHeight);

				if (index < count - 1 && count > 1)
					offset.Add(0, text.TextFormat.LineSpacing);

				index++;
			}

			return result;

		}
		public static SpriteBuffer GenerateSpriteBufferOfLine(Line l, TextFragment text) {
			SpriteBuffer result = new SpriteBuffer();
			Words ww = l.Words;

			int index = 0;
			int count = l.Count;

			Vector2d offset = Vector2d.Zero; //current position while measuring
			double width = 0; // measured with of a member


			foreach (Word w in ww) {
				Vector2d wordStart = offset.Copy;
				SpriteBuffer sprite = GenerateSpriteBufferOfWord(w, text);
				SpriteBuffer.OffsetPosition(ref sprite, offset);

				width = SpriteBuffer.WidthByLastMemberAndOffset(sprite, wordStart);

				offset.Add(width,0);
				if (w.IsWhiteSpace)
					offset.Add(WidthOfWhiteSpace(text),0);

				result.AddValuesFrom(sprite);

				if (index < count - 1 && count > 1)
					offset.Add(text.TextFormat.WordSpacing, 0);

				index++;
			}

			return result;
		}
		public static SpriteBuffer GenerateSpriteBufferOfWord(Word w, TextFragment text) {
			if (w.IsVirtual)
				return SpriteBuffer.Empty;

			SpriteBuffer result = new SpriteBuffer();

			int index = 0;
			int count = w.Count;

			Vector2d offset = Vector2d.Zero;
			PositionData data = null;
			SpriteData sprite;

			GameFont font = text.Font;

			foreach (Character c in w.Characters) {
				if (!text.Font.TryPositionData(c, ref data))
					data = font.PositionData(GameFont.UnknownCharacter);

				sprite = new SpriteData(
					font.TextureByPageIndex(data.FontPage),
					TextRenderer.GenerateMaskFromPositionData(data)
				);
				sprite.SetPosition(offset.X,offset.Y);

				double width;
				if (c.IsWhiteSpace)
					width = WidthOfWhiteSpace(text);
				else
					width = sprite.Width;

				offset.Add(width,0);

				if(index<count-1&&count>1)
					offset.Add(text.TextFormat.LetterSpacing, 0);

				result.Add(sprite);
				index++;

			}

			return result;
		}

		public static double WidthOfWhiteSpace(TextFragment text) {
			if (!text.HasFont || !text.Font.HasLoaded)
				return 0;
			GameFont font = text.Font;

			PositionData data = null;
			if (!font.TryPositionData(' ', ref data))
				return 0;

			double result = data.Width;
			return result;
		}
	}

}