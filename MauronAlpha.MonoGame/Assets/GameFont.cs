namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.FontParser.DataObjects;
	using MauronAlpha.FontParser.Events;


	using MauronAlpha.FileSystem.Units;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Assets;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

	/// <summary> A SpriteFont </summary>
	public class GameFont :MonoGameComponent {

		string STR_name;
		public string Name {
			get {
				return STR_name;
			}
		}

		GameManager DATA_game;

		public bool IsNull {
			get { return STR_name == null; }
		}

		//constructor
		public GameFont(GameManager game, string name)	: base() {
			DATA_game = game;
			STR_name = name;
		}
		public GameFont(GameManager game,string name, FontDefinition def)	: base() {
			_font = def;
			DATA_game = game;
			STR_name = name;
		}

		FontDefinition _font;
		public void SetDefinition(FontDefinition font) {
			_font = font;
		}
		public bool HasDefinition {
			get {
				return _font != null;
			}
		}

		Index<MonoGameTexture> _textures = new Index<MonoGameTexture>();
		public void SetTexture(MonoGameTexture texture) {
			int index = _font.IndexOfTexture(texture.Name);
			_textures.SetValue(index, texture);			
		}
		public MonoGameTexture TextureByPageIndex(int index) {
			return _textures.Value(index);
		}

		AssetManager Assets { 
			get {
				return DATA_game.Assets; 
			}
		}

		public bool HasLoaded {
			get {
				if(_font == null)
					return false;
				if(!_font.IsParsed)
					return false;
				return (_textures.CountKeys >= _font.FontPages.Count);
			}
		}

		public PositionData FetchCharacterData(char c) {
			return _font.PositionData(c);
		}
		public double CharacterHeight {
			get {
				if(_font == null || !_font.IsParsed)
					return 0;
				return _font.FontInfo.BaseHeight;
			}
		}

		public double LineHeight {
			get {
				if(_font == null || !_font.IsParsed)
					return 0;
				return _font.FontInfo.LineHeight;
			}
		}

		public Vector2d MeasureText(Text text) {
			Vector2d result = new Vector2d();
			Lines ll = text.Lines;
			foreach (Line l in ll) {
				Vector2d size = MeasureLine(l);
				result.Add(size);
			}
			return result;
		}
		public Vector2d MeasureLine(Line line) {
			Vector2d minMax = new Vector2d();
			Characters cc = line.Characters;
			Vector2d result = new Vector2d(0, LineHeight);
			foreach (Character c in cc) {
				Vector2d charSize = MeasureCharacter(c);
				if (charSize.Y > result.Y)
					result.SetY(charSize.Y);
				result.Add(charSize.X, 0);
			}
			return result;
		}
		public Vector2d MeasureCharacter(Character c) {
			if (c.IsEmpty)
				return Vector2d.Zero;
			if (c.IsVirtual)
				return Vector2d.Zero;
			PositionData p = this.FetchCharacterData(c.Symbol);
			return new Vector2d(LineHeight + p.Width);
		}
	}

}
