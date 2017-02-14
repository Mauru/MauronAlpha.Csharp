namespace MauronAlpha.MonoGame.Rendering.DataObjects {

	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.Rendering.Interfaces;

	/// <summary> Wrapper for SpriteBatch</summary>
	public class SpriteDrawManager:SpriteBatch {

		public SpriteDrawManager(GraphicsDevice device) : base(device) { }

		public void DrawSingleSprite(I_RenderResult obj, I_SpriteDrawCall info) {

			Texture2D texture = obj.Result;

			base.Begin(
				SpriteSortMode.Deferred,
				GetBlendState(info), //controls how colorData of the target is manipulated
				GetSamplerState(info), //controls how textures are resized (mstly when shrinking)
				GetDepthStencilState(info), 
				GetRasterizerState(info), 
				GetEffect(info), 
				GetTransformMatrix(info)
			);
			base.Draw(
				GetTexture(obj),
				GetTopLeftPosition(info),
				GetMask(info), 
				GetTint(info), 
				GetRotationRad(info), 
				GetOrigin(info), 
				GetScale(info), 
				SpriteEffects.None, 
				GetZIndex(info)
			);
			base.End();
		}

		public void DrawContinous(List<I_SpriteObject> obj, I_SpriteDrawCall info) {



		}

		//Parameters for begin
		BlendState GetBlendState(I_SpriteDrawCall info) {
			return BlendState.AlphaBlend;
		}
		SamplerState GetSamplerState(I_SpriteDrawCall info) {
			return null;
		}
		DepthStencilState GetDepthStencilState(I_SpriteDrawCall info) {
			return null;
		}
		RasterizerState GetRasterizerState(I_SpriteDrawCall info) {
			return null;
		}
		SpriteEffect GetEffect(I_SpriteDrawCall info) {
			return null;
		}
		System.Nullable<Matrix> GetTransformMatrix(I_SpriteDrawCall info) {
			return null;
		}
	
		//Parameters for Draw
		Texture2D GetTexture(I_RenderResult result) {
			return result.Result;
		}
		System.Nullable<Rectangle> GetMask(I_SpriteDrawCall info) {
			return null;
		}

		Color GetTint(I_SpriteDrawCall info) {
			return Color.White;
		}

		Vector2 GetTopLeftPosition(I_SpriteDrawCall info) {
			return Vector2.Zero;
		}
		Vector2 GetOrigin(I_SpriteDrawCall info) {
			return Vector2.Zero;
		}
		float GetRotationRad(I_SpriteDrawCall info) {
			return 0f;
		}
		Vector2 GetScale(I_SpriteDrawCall info) {
			return Vector2.One;
		}
		float GetZIndex(I_SpriteDrawCall info) {
			return 0f;
		}
	}

}
