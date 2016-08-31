namespace MauronAlpha.MonoGame.Rendering {

	using Microsoft.Xna.Framework.Graphics;

	public interface I_RenderResult {

		Texture2D Result { get; }

		bool HasResult { get; }

		void SetResult(Texture2D result, long time);

	}

}