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

	public class Scene_BasicText:GameScene {
		public Scene_BasicText(GameManager game) : base(game) { }

		TextDisplay _text;

		public override void Initialize() {

			AssetManager assets = Game.Assets;
			GameFont font = assets.DefaultFont;
			Text txt = new Text("GameCycle : 0");
			
			TextDisplay text = new TextDisplay(Game,txt,font);

			SpriteBuffer _sprites = text.SpriteBuffer;
			base.SetSpriteBuffer(_sprites);
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
