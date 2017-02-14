namespace MauronAlpha.MonoGame.Rendering.Collections {
	using MauronAlpha.MonoGame.DataObjects;
	using MauronAlpha.MonoGame.Collections;
	
	using MauronAlpha.MonoGame.Rendering.DataObjects;

	/// <summary> A  list of PreRenderProcesses </summary>
	public class PreRenderOrders:List<PreRenderProcess> {

		public PreRenderOrders(): base() {}

		public static PreRenderOrders Empty { get { return new PreRenderOrders(); } }

	}

}
