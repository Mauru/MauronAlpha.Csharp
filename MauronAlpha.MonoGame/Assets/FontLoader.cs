namespace MauronAlpha.MonoGame.Assets {
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Collections;

	using MauronAlpha.FontParser.DataObjects;
	using MauronAlpha.FontParser.Events;

	using MauronAlpha.FileSystem.Units;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;
	using MauronAlpha.Events.Units;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public class FontLoader :MonoGameComponent, I_subscriber<FontLoadEvent>, I_sender<FontLoaderEvent>, I_subscriber<TextureLoaderEvent> {
		GameFont _result;
		public GameFont Result {
			get {
				return _result;
			}
		}
		FontDefinition _font;
		GameManager _game;
		string _name;

		bool B_isBusy = false;
		public bool IsBusy { get { return B_isBusy; } }

		public FontLoader(GameManager game, string name)	: base() {
			_game = game;
			_name = name;
			File file = new File(game.Assets.ContentDirectory, name,"fnt");
			_font = new FontDefinition(file);
		}

		public void Start() {
			if(B_isBusy)
				return;
			B_isBusy = true;
			_font.Subscribe(this);
			_font.Parse();
		}

		Stack<TextureLoader> _textures = new Stack<TextureLoader>();

		//Received when the fontdefinition file has loaded
		public bool ReceiveEvent(FontLoadEvent e) {

			FontDefinition def = e.Font;
			def.UnSubscribe(this);
			_result = new GameFont(_game, _name, def);
			TextureLoader loader;
			foreach(FontPage p in def.FontPages) {

				loader = new TextureLoader(_game, p.FileName);
				_textures.Add(loader);

			}
			CycleTextures();
			return true;
			
		}

		void LoadCycleComplete() {
			if(_subscriptions == null)
				return;
			B_isBusy = false;
			_subscriptions.ReceiveEvent(new FontLoaderEvent(this));
		}

		void CycleTextures() {
			if(_textures.IsEmpty) {
				LoadCycleComplete();
				return;
			}
			TextureLoader loader = _textures.Pop;
			loader.Subscribe(this);
			loader.Start();		
		}

		public bool Equals(I_subscriber<FontLoadEvent> other) {
			return Id.Equals(other.Id);
		}

		Subscriptions<FontLoaderEvent> _subscriptions;
		public void Subscribe(I_subscriber<FontLoaderEvent> s) {
			if(_subscriptions == null)
				_subscriptions = new Subscriptions<FontLoaderEvent>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<FontLoaderEvent> s) {
			if(_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}

		public bool ReceiveEvent(TextureLoaderEvent e) {
			TextureLoader loader = e.Target;
			loader.UnSubscribe(this);
			_result.SetTexture(loader.Result, loader.Name);
			CycleTextures();
			return true;
		}
		public bool Equals(I_subscriber<TextureLoaderEvent> other) {
			return Id.Equals(other.Id);
		}
	}
	public class FontLoaderEvent :EventUnit_event {

		FontLoader _target;
		public FontLoader Target { get { return _target; } }
		public FontLoaderEvent(FontLoader target): base("Loaded") {
			_target = target;
		}
	
	}

}