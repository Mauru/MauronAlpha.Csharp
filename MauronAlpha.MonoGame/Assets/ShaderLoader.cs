namespace MauronAlpha.MonoGame.Assets {
	using MauronAlpha.FileSystem.Units;
	using MauronAlpha.MonoGame.Rendering;

	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Collections;

	public class ShaderLoader:MonoGameAsset, I_sender<ShaderLoadEvent> {
		GameManager _game;
		GameManager Game {
			get {
				return _game;
			}
		}

		File _file;

		string _name;
		public string Name {
			get {
			return _name;
			}
		}
		
		CustomShader _result;
		public CustomShader Result {
			get {
				return _result;
			}
		}

		public ShaderLoader(GameManager game, string name) : base() {
			_game = game;
			_name = name;
			_file = new File(game.Assets.ShaderDirectory, name, AssetType_Shader.Instance.FileExtension);
		}

		Subscriptions<ShaderLoadEvent> _subscriptions;
		public void Subscribe(I_subscriber<ShaderLoadEvent> s) {
			if(_subscriptions == null)
				_subscriptions = new Subscriptions<ShaderLoadEvent>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<ShaderLoadEvent> s) {
			if(_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}

		bool _busy = false;
		public bool IsBusy { get { return _busy; } }

		bool HasLoaded {
			get {
				return _result != null;
			}
		}

		public void Start() {
			if(_busy)
				return;
			_busy = true;
			CustomShader progress = new CustomShader(Game, _name, _file.ContentAsByteArray);
			_result = progress;

			_busy = false;
			_subscriptions.ReceiveEvent(new ShaderLoadEvent(this));
		}
	}
}

namespace MauronAlpha.MonoGame.Assets {

	using MauronAlpha.Events.Units;

	public class ShaderLoadEvent :EventUnit_event {
		ShaderLoader _shader;
		public ShaderLoader Target {
			get {
			return _shader;
			}
		}
		public ShaderLoadEvent(ShaderLoader def) : base("Loaded") {
			_shader = def;
		}
	}

}