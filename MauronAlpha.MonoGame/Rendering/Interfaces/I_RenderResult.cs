namespace MauronAlpha.MonoGame.Rendering {

	using Microsoft.Xna.Framework.Graphics;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public interface I_RenderResult {

		Texture2D Result { get; }

		bool HasResult { get; }

		void SetResult(Texture2D result, long time);

		long Time { get; }

	}

}