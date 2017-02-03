namespace MauronAlpha.MonoGame.Rendering.DataObjects {

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	using MauronAlpha.MonoGame.Rendering.Interfaces;

	/// <summary> Wrapper for SpriteBatch</summary>
	public class SpriteDrawManager:SpriteBatch {

		public SpriteDrawManager(GraphicsDevice device) : base(device) { }

		public void DrawSprite(I_RenderResult obj, I_SpriteDrawInfo info) {

			Texture2D texture = obj.Result;

			base.Begin(SpriteSortMode.Deferred, GetBlendState(info), GetSamplerState(info), GetDepthStencilState(info), GetRasterizerState(info), GetEffect(info), GetTransformMatrix(info));

		}

		BlendState GetBlendState(I_SpriteDrawInfo info) {
			return BlendState.AlphaBlend;
		}
		SamplerState GetSamplerState(I_SpriteDrawInfo info) {
			return null;
		}
		DepthStencilState GetDepthStencilState(I_SpriteDrawInfo info) {
			return null;
		}
		RasterizerState GetRasterizerState(I_SpriteDrawInfo info) {
			return null;
		}
		SpriteEffect GetEffect(I_SpriteDrawInfo info) {
			return null;
		}
		System.Nullable<Matrix> GetTransformMatrix(I_SpriteDrawInfo info) {
			return null;
		}
	}

}
