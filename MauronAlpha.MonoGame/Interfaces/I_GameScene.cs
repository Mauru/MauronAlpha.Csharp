﻿namespace MauronAlpha.MonoGame.Interfaces {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	/// <summary> Represents a set of render-instructions for a draw cycle </summary>
	public interface I_GameScene {

		GameManager Game { get; }

		void RequestRender();

		void Initialize();

		bool IsInitialized { get; }

		List<I_Renderable> Children { get; }

		ShapeBuffer ShapeBuffer { get; }

		Camera Camera { get; }

		GameRenderer.DrawMethod DrawMethod { get; }
	}



}
