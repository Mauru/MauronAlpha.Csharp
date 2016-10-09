namespace MauronAlpha.MonoGame.Assets {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Rendering;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	/// <summary> A collection of assets </summary>
	public class AssetGroup :MonoGameComponent, I_sender<AssetLoadEvent>, I_subscriber<FontLoaderEvent>, I_subscriber<TextureLoaderEvent>, I_subscriber<ShaderLoadEvent> {

		//constructor
		public AssetGroup(GameManager game, string name):base() {
			_game = game;
			_name = name;
		}

		//basic properties
		bool _busy = false;
		public bool IsBusy {
			get { return _busy; }
		}
		
		GameManager _game;
		string _name;
		public string Name { get { return _name; } }
		
		//Load Request Handling
		Stack<LoadRequest> _queue = new Stack<LoadRequest>();
		int _count = 0;
		int _loaded = 0;

		/// <summary> start the load cycle </summary>
		public void Load() {
			if (_busy)
				return;

			_busy = true;

			CycleQueue();
		}
		void QueueComplete() {
			_busy = false;
			if (_subscriptions == null)
				return;
			_subscriptions.ReceiveEvent(new AssetLoadEvent(this));
		}
		void CycleQueue() {

			if (_queue == null || _queue.IsEmpty) {
				QueueComplete();
				return;
			}

			LoadRequest request = _queue.Pop;
			if (request.IsFont) {

				FontLoader font = new FontLoader(_game, request.Name);
				font.Subscribe(this);
				font.Start();
				return;

			}
			else if (request.IsTexture) {

				TextureLoader texture = new TextureLoader(_game, request.Name);
				texture.Subscribe(this);
				texture.Start();
				return;

			}
			else if (request.IsShader) {

				ShaderLoader shader = new ShaderLoader(_game, request.Name);
				shader.Subscribe(this);
				shader.Start();
				return;

			}

			throw new GameError("Unknown AssetType!", request);

		}

		public void Add(LoadRequest request) {
			_queue.Add(request);
		}
		public void Add(Stack<LoadRequest> requests) {
			while (!requests.IsEmpty) {

				LoadRequest r = requests.Pop;
				_count++;
				_queue.Add(r);

			}
		}

		Subscriptions<AssetLoadEvent> _subscriptions;
		public void Subscribe(I_subscriber<AssetLoadEvent> s) {
			if (_subscriptions == null)
				_subscriptions = new Subscriptions<AssetLoadEvent>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<AssetLoadEvent> s) {
			if (_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}


		//Textures
		Registry<MonoGameTexture> _textures = new Registry<MonoGameTexture>();
		public Registry<MonoGameTexture> Textures { get { return _textures; } }
		public bool TryTexture(string name, ref MonoGameTexture texture) {
			return _textures.TryGet(name, ref texture);
		}
		public List<string> ListOfTextureNames() {
			List<string> result = new List<string>();
			foreach (MonoGameTexture t in _textures)
				result.Add(t.Name);
			return result;
		}
		public List<string> ListOfTextureNames(bool includeGroupName) {
			if (!includeGroupName)
				return ListOfTextureNames();

			List<string> result = new List<string>();
			foreach (MonoGameTexture t in _textures)
				result.Add(Name+"."+t.Name);
			return result;
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


		//Fonts
		Registry<GameFont> _fonts = new Registry<GameFont>();
		public Registry<GameFont> Fonts { get { return _fonts; } }
		public GameFont Font(string font) {
			return _fonts.Value(font);
		}
		public bool TryFont(string name, ref GameFont font) {
			return _fonts.TryGet(name, ref font);
		}
		public List<string> ListOfFontNames() {
			List<string> result = new List<string>();
			foreach (GameFont f in _fonts)
				result.Add(f.Name);
			return result;
		}
		public List<string> ListOfFontNames(bool addGroupName) {
			if (!addGroupName)
				return ListOfFontNames();

			List<string> result = new List<string>();
			foreach (GameFont f in _fonts) {
				if (addGroupName)
					result.Add(Name + "." + f.Name);
				else
					result.Add((f.Name));
			}
			return result;
		}
		public long FontCount {
			get {
				return _fonts.KeyCount;
			}
		}
		public bool HasFonts {
			get {
				return !_fonts.IsEmpty;
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


		//Shaders
		Registry<CustomShader> _shaders = new Registry<CustomShader>();
		public Registry<CustomShader> Shaders { get { return _shaders; } }
		public bool TryShader(string name, ref CustomShader shader) {
			return _shaders.TryGet(name, ref shader);
		}
		public List<string> ListOfShaderNames() {
			List<string> result = new List<string>();
			foreach (CustomShader t in _shaders)
				result.Add(t.Name);
			return result;
		}
		public List<string> ListOfShaderNames(bool includeGroupName) {
			if (!includeGroupName)
				return ListOfShaderNames();
			List<string> result = new List<string>();
			foreach (CustomShader t in _shaders)
				result.Add(Name+"."+t.Name);
			return result;
		}

		public bool ReceiveEvent(ShaderLoadEvent e) {
			ShaderLoader loader = e.Target;
			loader.UnSubscribe(this);
			CustomShader shader = loader.Result;
			_shaders.SetValue(shader.Name, shader);
			_loaded++;
			CycleQueue();
			return true;
		}
		public bool Equals(I_subscriber<ShaderLoadEvent> other) {
			return Id.Equals(other.Id);
		}

	}

}
