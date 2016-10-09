namespace MauronAlpha.MonoGame.Assets {
	using MauronAlpha.Events.Interfaces;
	using MauronAlpha.Events.Units;
	using MauronAlpha.Events.Collections;

	using MauronAlpha.FileSystem.Units;

	using MauronAlpha.MonoGame.DataObjects;

	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public class TextureLoader :MonoGameComponent, I_sender<TextureLoaderEvent> {

		MonoGameTexture _result;
		public MonoGameTexture Result {
			get {
				return _result;
			}
		}

		GameManager _game;
		File _file;

		bool B_isBusy = false;
		public bool IsBusy { get { return B_isBusy; } }


		string _name;
		public string Name { get { return _name; } }

		public TextureLoader(GameManager game, string name)	: base() {
			_game = game;
			_file = new File(game.Assets.TextureDirectory, name);
			_name = name;
		}

		public void Start() {
			if(B_isBusy)
				return;
			B_isBusy = true;
			Texture2D result;
			try { 
				using(System.IO.Stream stream = _file.Stream) {
					result = Texture2D.FromStream(_game.Engine.GraphicsDevice,stream);
				}
			} catch(MauronAlpha.FileSystem.FileSystemError error) {

				//the file does not exist
				throw new MauronAlpha.MonoGame.DataObjects.GameError("Texture file not found!, {"+_file.Path+"}", this);

			}

			_result = new MonoGameTexture(_game, result);
			_result.SetName(_name);
			B_isBusy = false;

			if(_subscriptions != null)
				_subscriptions.ReceiveEvent(new TextureLoaderEvent(this));

		}

		Subscriptions<TextureLoaderEvent> _subscriptions;
		public void Subscribe(I_subscriber<TextureLoaderEvent> s) {
			if(_subscriptions == null)
				_subscriptions = new Subscriptions<TextureLoaderEvent>();
			_subscriptions.Add(s);
		}
		public void UnSubscribe(I_subscriber<TextureLoaderEvent> s) {
			if(_subscriptions == null)
				return;
			_subscriptions.Remove(s);
		}
	}
	public class TextureLoaderEvent :EventUnit_event {
		TextureLoader _target;
		public TextureLoader Target { get { return _target; } }

		public TextureLoaderEvent(TextureLoader source)	: base("Loaded") {
			_target = source;
		}
	}

}
