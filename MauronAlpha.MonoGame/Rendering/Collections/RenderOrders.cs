namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Collections;
	
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	public class RenderOrders:List<PreRenderOrder> {

		public static RenderOrders Empty { get { return new RenderOrders(); } }

	}

}
