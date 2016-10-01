namespace MauronAlpha.MonoGame.Rendering {
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;
	using MauronAlpha.Geometry.Geometry2d.Units;

	/// <summary> Wrapper for RenderTarget2d </summary>
	public class RenderStage:RenderTarget2D {

		Vector2d _size;
		public Vector2d Size { get { return _size; } }

		TextureBatch _batch;
		public TextureBatch Caller {
			get { return _batch; }
		}

		//Constructors
		public RenderStage(GraphicsDevice device, int width,int height):base(
			device,
			width,
			height,
			false,
			SurfaceFormat.Color,
			DepthFormat.None,
			0,
			RenderTargetUsage.PreserveContents
		) {
			_size = new Vector2d(width, height);
			_batch = new TextureBatch(device);
		}
		public RenderStage(GraphicsDevice device, Vector2d size) : base(
			device,
			size.IntX,
			size.IntY,
			false,
			SurfaceFormat.Color,
			DepthFormat.None,
			0,
			RenderTargetUsage.PreserveContents
		) {
			_size = size;
			_batch = new TextureBatch(device);
		}

		public Texture2D AsTexture2D {
			get {
				Texture2D result = new Texture2D(base.GraphicsDevice,_size.IntX,_size.IntY);
				Color[] colorData = new Color[_size.IntX*_size.IntY];
				base.GetData<Color>(colorData);
				result.SetData<Color>(colorData);
				return result;
			}
		}

		public void SetAsRenderTarget() {
			GraphicsDevice.SetRenderTarget(this);
		}

	}

	public class TextureBatch :SpriteBatch {
		public TextureBatch(GraphicsDevice device) : base(device) { }

		public void Draw(I_RenderResult r, I_Renderable t) {
			if(!r.HasResult)
				return;
			Vector2d v = t.Position;
			base.Draw(r.Result, new Vector2(v.FloatX, v.FloatY), Color.White);
		}
	}
}