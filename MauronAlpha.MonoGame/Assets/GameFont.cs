namespace MauronAlpha.MonoGame.DataObjects {
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.FontParser.DataObjects;
	using MauronAlpha.FontParser.Events;


	using MauronAlpha.FileSystem.Units;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Assets;

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
		public GameFont(GameManager game)	: base() {
			DATA_game = game;
		}
		public GameFont(GameManager game, string name)	: base() {
			DATA_game = game;
			STR_name = name;
		}
		public GameFont(GameManager game, FontDefinition def)	: base() {
			_font = def;
			DATA_game = game;
		}

		FontDefinition _font;
		public void SetDefinition(FontDefinition font) {
			_font = font;
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
	}

}
