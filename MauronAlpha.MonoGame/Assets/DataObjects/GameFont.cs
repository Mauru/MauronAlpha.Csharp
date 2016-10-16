namespace MauronAlpha.MonoGame.Assets.DataObjects {
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.FontParser.DataObjects;
	using MauronAlpha.FontParser.Events;

	using MauronAlpha.FileSystem.Units;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.Assets;
	using MauronAlpha.MonoGame.Assets.Interfaces;

	using MauronAlpha.Geometry.Geometry2d.Units;
	using MauronAlpha.Geometry.Geometry2d.Shapes;

	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;

	using MauronAlpha.MonoGame.Rendering.DataObjects;

	using Microsoft.Xna.Framework;

	/// <summary> A SpriteFont </summary>
	public class GameFont :MonoGameComponent, I_GameFont {
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
		public bool HasLoaded {
			get {
				if (_font == null)
					return false;
				if (!_font.IsParsed)
					return false;
				return (_textures.CountKeys >= _font.FontPages.Count);
			}
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

		//FontDefinition
		FontDefinition _font;
		public void SetDefinition(FontDefinition font) {
			_font = font;
		}
		public bool HasDefinition {
			get {
				return _font != null;
			}
		}

		//Texture Atlas
		Index<MonoGameTexture> _textures = new Index<MonoGameTexture>();
		public void SetTexture(MonoGameTexture texture, string name) {
			if (texture == null)
				throw new GameError("Texture can not be null!", this);
			int index = _font.IndexOfTexture(name);
			if (index < 0)
				throw new GameError("Could not index font-texture {" + texture.Name + "}!", this);
			_textures.SetValue(index, texture);			
		}
		public MonoGameTexture TextureByPageIndex(int index) {
			return _textures.Value(index);
		}
		public bool TryTextureByPageIndex(int index, ref MonoGameTexture result) {
			return _textures.TryIndex(index, ref result);
		}
		public List<string> TextureNames() {
			List<string> result = new List<string>();
			foreach (MonoGameTexture t in _textures)
				result.Add(t.Name);

			return result;
		}

		AssetManager Assets { 
			get {
				return DATA_game.Assets; 
			}
		}

		public PositionData PositionData(char c) {
			return _font.PositionData(c);
		}
		public PositionData PositionData(Character c) {
			return _font.PositionData(c.Symbol);
		}
		public bool TryPositionData(Character c, ref PositionData result) {
			return _font.TryPositionData(c.Symbol, ref result);
		}
		public bool TryPositionData(char c, ref PositionData result) {
			return _font.TryPositionData(c, ref result);
		}

		public double BaseHeight {
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

		//Some basic defaults
		public static Character UnknownCharacter {
			get {
				return new Character('\uFFFD');
			}
		}

		TextFormat _textFormat;
		public TextFormat TextFormat {
			get {
				if (_textFormat == null)
					_textFormat = DefaultTextFormat;
				return _textFormat;
			}
		}

		public static TextFormat DefaultTextFormat {
			get { return TextFormat.Default; }
		}

		//Debug
		public string DebugFontPages() {
			string result = "";
			foreach (MonoGameTexture texture in _textures) {
				result += "[" + _font.IndexOfTexture(texture.Name) +":" + texture.Name + "]";
			}
			return result;

		}
	}

}
