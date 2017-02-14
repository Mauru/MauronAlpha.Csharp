namespace MauronAlpha.MonoGame.UI.DataObjects {

	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.Events.Interfaces;

	using Microsoft.Xna.Framework;

	public class CompositeHexagonShape : MonoGameComponent, I_RenderComposite, I_subscriber<PreRenderProcessComplete> {

		PreRenderedTextures _resources = new PreRenderedTextures();
		public PreRenderedTextures Textures {
			get { return _resources; }
		}

		//Constructor
		public CompositeHexagonShape(double width, int outline): base() {
			Polygon2d p = Ngon2d.CreateAlignedTopLeft(6, 512);
			ShapeBuffer drawData = new ShapeBuffer(p);
			drawData.SetBounds(p.Bounds);

			PreRenderOrders preRender = new PreRenderOrders() {
				new PreRenderProcess(this, "hex", drawData)
			};

			double innerWidth = width - outline*2;

			double factor = width/innerWidth;

			Vector2d scaleVector = new Vector2d(factor, factor);

			CompositePreRenderProcess process = new CompositePreRenderProcess("outline") {
				new CompositePreRenderComponent("hex", Color.Red, BlendModes.Alpha),
				new CompositePreRenderComponent("hex", Color.Yellow, BlendModes.SubtractNonAlpha, TransformationData2D.CreateScale(scaleVector))
			};
		}
		public bool ReceiveEvent(PreRenderProcessComplete e) {
			PreRenderProcess process = e.Target;
			_resources.Add(process.Name, process.RenderResult);
			return true;
		}
		public bool Equals(I_subscriber<PreRenderProcessComplete> other) {
			return Id.Equals(other.Id);
		}
	}

}
