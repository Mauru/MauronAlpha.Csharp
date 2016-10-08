namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Collections;

	public class RenderOrders:List<RenderOrder> {

		public static RenderOrders Empty { get { return new RenderOrders(); } }
	}

}
