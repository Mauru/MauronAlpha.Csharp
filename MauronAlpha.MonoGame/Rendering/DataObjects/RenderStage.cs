namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public class RenderStage : MonoGameComponent {

		GameManager _game;
		public GameManager Game { get { return _game; } }

		Vector2d _size;

		RenderTarget2D _target;
		public RenderTarget2D RenderTarget { 
			get { 
				return _target;
			} 
		}

		//Constructors
		public RenderStage(GameManager game, Vector2d size): base() {
			_game = game;
			_size = size;
			_target = new RenderTarget2D(game.Engine.GraphicsDevice, _size.IntX, _size.IntY);
		}
		public RenderStage(GameManager game, double x, double y): base() {
			_size = new Vector2d(x, y);
			_target = new RenderTarget2D(game.Engine.GraphicsDevice, _size.IntX, _size.IntY);
		}

		//Tentative methods
		public MonoGameTexture AsTexture {
			get {
				Texture2D texture = new Texture2D(Game.Engine.GraphicsDevice,_size.IntX,_size.IntY);
				int count = texture.Width*texture.Height;
				Color[] data = new Color[texture.Width * texture.Height];
				//Rectangle r = new Rectangle(0,0,_size.IntX,_size.IntY);
				//base.GetData<Color>(0, r, data, 0, count);
				_target.GetData<Color>(data);
				texture.SetData<Color>(data);
				MonoGameTexture result = new MonoGameTexture(_game, texture);
				return result;
			}
		}
	}

}