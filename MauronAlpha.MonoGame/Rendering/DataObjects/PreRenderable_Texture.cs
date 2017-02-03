namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public class PreRenderable_Texture : PreRenderable {

		MonoGameTexture _texture;

		public PreRenderable_Texture(GameManager game, string assetGroup, string name): base(game) {
			AssetManager assets = game.Assets;

			_texture = null;

			if (!assets.TryTexture(assetGroup, name, ref _texture))
				throw new AssetError("Unnown texture {" + assetGroup + "," + name + "}!", this);

			_bounds = MonoGameTexture.GenerateBounds(_texture);

			_orders = new RenderOrders() {
				new PreRenderProcess(Game, name, _texture,_bounds)
			};
		}

		RenderOrders _orders;
		public override RenderOrders RenderOrders {
			get {
				return _orders;
			}
		}

		Polygon2dBounds _bounds;
		public override Polygon2dBounds Bounds {
			get { return _bounds; }
		}
	}
}