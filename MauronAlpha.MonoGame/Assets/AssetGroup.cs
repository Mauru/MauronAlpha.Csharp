namespace MauronAlpha.MonoGame.Assets {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	/// <summary> A collection of assets </summary>
	public class AssetGroup :MonoGameComponent, I_sender<AssetLoadEvent>, I_subscriber<FontLoaderEvent>, I_subscriber<TextureLoaderEvent> {

		//constructor
		public AssetGroup(GameManager game, string name):base() {
			_game = game;
			_name = name;
		}

		bool _busy = false;
		public bool IsBusy {
			get { return _busy; }
		}
		public bool HasFonts {
			get {
				return !_fonts.IsEmpty;
			}
		}
		
		GameManager _game;
		string _name;
		public string Name { get { return _name; } }

		Stack<LoadRequest> _queue = new Stack<LoadRequest>();
		int _count = 0;
		int _loaded = 0;

		Registry<MonoGameTexture> _textures = new Registry<MonoGameTexture>();
		Registry<GameFont> _fonts = new Registry<GameFont>();

		public bool TryFont(string name, ref GameFont font) {

			_game.LogFile.LogOnce(this,_fonts.Keys.AsArray);

			return _fonts.TryGet(name, ref font);

		}

		public void Load() {

			if(_busy)
				return;

			_busy = true;

			CycleQueue();

		}

		void QueueComplete() {
			_busy = false;
			if(_subscriptions == null)
				return;
			_subscriptions.ReceiveEvent(new AssetLoadEvent(this));
		}
		void CycleQueue() {

			if(_queue == null || _queue.IsEmpty) { 
				QueueComplete();
				return;
			}

			LoadRequest request = _queue.Pop;
			if(request.IsFont) {

				FontLoader font = new FontLoader(_game, request.Name);
				font.Subscribe(this);
				font.Start();
				return;

			} else if(request.IsTexture) {

				TextureLoader texture = new TextureLoader(_game, request.Name);
				texture.Subscribe(this);
				texture.Start();
				return;

			}

			throw new GameError("Unknown AssetType!", request);		

		}

		/// <summary> Returns all fonts in format ASSETGROUPNAME:FONTNAME</summary>
		public List<string> GetListOfFontNames() {
			List<string> result = new List<string>();
			foreach(GameFont f in _fonts)
				result.Add(f.Name);
			return result;
		}
		public List<string> GetListOfFontNames(bool addGroupName) {
			List<string> result = new List<string>();
			foreach(GameFont f in _fonts) {
				if(addGroupName)
					result.Add(Name + "." + f.Name);
				else
					result.Add((f.Name));
			}
			return result;
		}


		public void Add(LoadRequest request) {
			_queue.Add(request);
		}
		public void Add(Stack<LoadRequest> requests) {
			while(!requests.IsEmpty) {
				
				LoadRequest r = requests.Pop;
				_count++;
				_queue.Add(r);

			}
		}

		Subscriptions<AssetLoadEvent> _subscriptions;
		public void Subscribe(I_subscriber<AssetLoadEvent> s) {
			if(_subscriptions == null)
				_subscriptions = new Subscriptions<AssetLoadEvent>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<AssetLoadEvent> s) {
			if(_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}

		public GameFont Font(string font) {
			throw new MauronAlpha.MonoGame.Debug.DebugException(this,"Loading font "+font+"#"+_fonts.KeyCount);
			return _fonts.Value(font);
		}

		public long FontCount {
			get {
				return _fonts.KeyCount;
			}
		}

		public bool ReceiveEvent(FontLoaderEvent e) {
			FontLoader loader = e.Target;
			loader.UnSubscribe(this);
			GameFont font = loader.Result;
			_fonts.SetValue(font.Name, font);
			_loaded++;
			CycleQueue();
			return true;
		}
		public bool Equals(I_subscriber<FontLoaderEvent> other) {
			return Id.Equals(other.Id);
		}

		public bool ReceiveEvent(TextureLoaderEvent e) {
			TextureLoader loader = e.Target;
			loader.UnSubscribe(this);
			MonoGameTexture texture = loader.Result;
			_textures.SetValue(texture.Name, texture);
			_loaded++;
			CycleQueue();
			return true;
		}
		public bool Equals(I_subscriber<TextureLoaderEvent> other) {
			return Id.Equals(other.Id);
		}
	}

}
