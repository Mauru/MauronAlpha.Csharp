namespace MauronAlpha.MonoGame.Rendering {
	using MauronAlpha.MonoGame.Collections;
	
	public interface I_Renderable {

		Bounds Bounds { get; }
		Vector Position { get; }

		I_RenderResult Outline { get; }

		RenderOrders Orders { get; }
		void SetRenderResult(I_RenderResult result);

		T TargetAs<T>() where T:I_Renderable;
	}

}