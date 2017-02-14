namespace MauronAlpha.MonoGame.Interfaces {
	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public interface I_TextInterpreter: I_Renderable {

		List<MonoGameTexture> Textures { get; }

	}
}
