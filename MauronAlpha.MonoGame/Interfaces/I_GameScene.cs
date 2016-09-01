namespace MauronAlpha.MonoGame.Interfaces {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	/// <summary> Represents a set of render-instructions for a draw cycle </summary>
	public interface I_GameScene {

		GameManager Game { get; }

		void RequestRender();

	}



}
