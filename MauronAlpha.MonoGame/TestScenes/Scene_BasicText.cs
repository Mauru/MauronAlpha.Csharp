namespace MauronAlpha.MonoGame {
	using MauronAlpha.MonoGame.UI.DataObjects;
	
	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Utility;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.TextProcessing.Units;
	using MauronAlpha.TextProcessing.Collections;
	using MauronAlpha.TextProcessing.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Units;

	public class Scene_BasicText:GameScene {
		public Scene_BasicText(GameManager game) : base(game) {
			_txt = new Text("GameCycle : 0");
		}
		public Scene_BasicText(GameManager game, string text, bool isAnimated):base(game) {
			_txt = new Text(text);
			_isAnimated = isAnimated;
		}

		Text _txt;
		TextFragment _text;

		bool _isAnimated = true;

		public override void Initialize() {
			AssetManager assets = Game.Assets;
			GameFont font = assets.DefaultFont;
			
			TextFragment text = new TextFragment(Game,font,_txt);

			SpriteBuffer _sprites = text.SpriteBuffer;
			//_sprites = SpriteBuffer.OffsetPosition(ref _sprites, Game.Renderer.CenterOfScreen);
			SetSpriteBuffer(_sprites);

			_text = text;
			Game.Renderer.SetCurrentScene(this);
			Game.Renderer.SetDrawMethod(TextRenderer.DrawMethod);

			base.Initialize();
		}

		bool _isBusy = false;
		long _time = 0;
		public bool IsBusy { get { return _isBusy; } }

		long _lastCycle = -10;
		long _sleepTime  = 100000;
		public override void RunLogicCycle(long time) {

			if (!_isAnimated)
				return;

			long current = _time;

			if (_lastCycle + _sleepTime < current) {
				_time++;
				return;
			}

			if (IsBusy) {
				_time++;
				return;
			}

			_isBusy = true;
			Line l = _text.Text.Text.LastLine;
			l.Words.RemoveLastElement();
			Word w = new Word(new Characters(""+current/10).Reverse());
			l.Add(w);
			SpriteBuffer buffer = _text.RegenerateSpriteBuffer(_time);
			buffer = SpriteBuffer.OffsetPosition(ref buffer, Game.Renderer.CenterOfScreen);
			Game.Renderer.CurrentScene.SetSpriteBuffer(buffer);
			_lastCycle = current;
			_time++;
			_isBusy = false;			
		}

		public override GameRenderer.DrawMethod DrawMethod {
			get { return TextRenderer.DrawMethod; }
		}
	}
}
