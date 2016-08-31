namespace MauronAlpha.MonoGame.Interfaces {
	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	public interface I_TextInterpreter: I_Renderable {

		RenderOrders RenderOrders { get; }
		List<MonoGameTexture> Textures { get; }

	}
}
