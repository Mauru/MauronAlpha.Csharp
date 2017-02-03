namespace MauronAlpha.MonoGame.UI.DataObjects {

	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.Geometry.Geometry2d.Shapes;
	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework;

	public class CompositeHexagonShape : MonoGameComponent, I_RenderComposite {

		PreRenderedTextures _resources = new PreRenderResources();
		public PreRenderedTextures Textures {
			get { return Resources; }
		}

		//Constructor
		public CompositeHexagonShape(double width, int outline): base() {
			Polygon2d p = Ngon2d.CreateAlignedTopLeft(6, 512);
			ShapeBuffer drawData = new ShapeBuffer(p);
			drawData.SetBounds(p.Bounds);

			PreRenderOrders preRender = new PreRenderOrders(QueueComposite, this) {
				new PreRenderProcess("hex", drawData, RenderTypes.Shape)
			};

			double innerWidth = width - outline*2;

			double factor = width/innerWidth;

			CompositePreRenderProcess process = new CompositePreRenderProcess(this, FinalizeComposite, "outline") {
				new CompositePreRenderComponent("hex", Color.Red, BlendModes.Alpha),
				new CompositePreRenderComponent("hex", Color.Yellow, BlendModes.SubtractNonAlpha, TransformationData2D.CreateScale(factor))
			};
		}
		public void PreRenderComplete(GameManager game, PreRenderProcess process, I_RenderComposite component) {
			component.Textures.Add(process.Name, process.RenderResult);
		}

		public void FinalizeComposite(GameManager game, CompositePreRenderProcess process, I_RenderComposite component) {
			component.Textures.Add(process.Name, process.RenderResult);
		}

	}



}

namespace MauronAlpha.MonoGame.Rendering.Interfaces {
	using MauronAlpha.MonoGame.Rendering.Collections;

	public interface I_RenderComposite {

		PreRenderedTextures Textures;

	}

}

namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Collections;

	public class PreRenderedTextures : Registry<I_RenderResult> { }

}
