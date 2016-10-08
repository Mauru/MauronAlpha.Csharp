namespace MauronAlpha.MonoGame.Rendering {
	using Microsoft.Xna.Framework.Graphics;
	using MauronAlpha.MonoGame.Interfaces;

	public interface I_Shader {

		GameManager Game { get; }

		string Name { get; }

		void Apply();

		System.Collections.Generic.IEnumerable<Microsoft.Xna.Framework.Graphics.EffectPass> ShaderPasses { get; }
	}

}