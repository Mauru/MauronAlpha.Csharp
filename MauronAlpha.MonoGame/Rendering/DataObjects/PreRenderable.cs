﻿namespace MauronAlpha.MonoGame.Rendering.DataObjects {
	using MauronAlpha.Geometry.Geometry2d.Transformation;

	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using Microsoft.Xna.Framework.Graphics;

	/// <summary> Abstract baseClass for preRenderable items </summary>
	public abstract class PreRenderable : MonoGameComponent, I_PreRenderable {

		GameManager _game;
		public GameManager Game {
			get {
				return _game;
			}
		}

		Matrix2d _matrix;

		public PreRenderable(GameManager game)	: base() {
			_game = game;
		}

		I_RenderResult _result;
		public I_RenderResult RenderResult { get { return _result; } }
		public bool TryRenderResult(ref I_RenderResult result) {
			if (_result == null)
				return false;
			result = _result;
			return true;
		}
		public void SetRenderResult(I_RenderResult result) {
			_result = result;
		}
		public void SetRenderResult(Texture2D texture, long time) {
			if (_result == null)
				_result = new RenderResult(time, texture);
			else
				_result.SetResult(texture, time);
		}

		public virtual RenderOrders RenderOrders {
			get {
				return RenderOrders.Empty;
			}
		}

		public abstract Polygon2dBounds Bounds { get; }




	}
}