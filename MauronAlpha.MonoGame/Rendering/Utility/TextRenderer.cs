namespace MauronAlpha.MonoGame.Rendering.Utility {
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;

	using MauronAlpha.TextProcessing.Collections;
	using MauronAlpha.TextProcessing.Units;

	using MauronAlpha.FontParser.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.UI.DataObjects;
	using MauronAlpha.MonoGame.Interfaces;

	using MauronAlpha.MonoGame.Rendering;
	using MauronAlpha.MonoGame.Rendering.DataObjects;
	using MauronAlpha.MonoGame.Rendering.Interfaces;
	using MauronAlpha.MonoGame.Rendering.Collections;

	using MauronAlpha.MonoGame.Assets.DataObjects;

	public class TextRenderer:MonoGameComponent, I_Renderer {

		GameRenderer.RenderMethod I_Renderer.RenderMethod {
			get { return RenderMethod; }
		}

		public static GameRenderer.RenderMethod RenderMethod {
			get {
				return RenderTextToTexture;
			}
		}
		public static I_RenderResult RenderTextToTexture(RenderStage stage, I_Renderable target, long time) {
			stage.SetAsRenderTarget();
			TextFragment text = (TextFragment) target;
			SpriteBatch batch = stage.Caller;
			GameFont font = text.Font;

			batch.Begin();

			Lines ll = text.Lines;
			Rectangle o;
			Vector2 pos = new Vector2();

			Vector2d finalSize = new Vector2d();
			Vector2d lineSize = new Vector2d();
			foreach(Line l in ll) {
				lineSize.Set(0,text.LineHeight);

				Characters cc = l.Characters;
				foreach(Character c in cc) {
					
					PositionData p = font.PositionData(c.Symbol);
					MonoGameTexture t = font.TextureByPageIndex(p.FontPage);
					o = new Rectangle(p.X,p.Y,p.Width,p.Height);
					Color m = Color.Red;
					Vector2d v = text.Position;
					pos.X = v.FloatX;
					pos.Y = v.FloatY;
					lineSize.Add(p.Width);
				
					batch.Draw(t.AsTexture2d, pos, o, m);
				}
				if(lineSize.X>finalSize.X)
					finalSize.SetX(lineSize.X);
				
				finalSize.Add(0,lineSize.Y);				
			}

			batch.End();
			
			return new RenderResult(time, target, stage.AsTexture2D);

		}


		public static void DrawMethod(GameRenderer renderer, long time) {
			I_GameScene scene = renderer.CurrentScene;

			GraphicsDevice device = renderer.GraphicsDevice;
			SpriteBatch batch = renderer.DefaultSpriteBatch;

			SpriteBuffer buffer = scene.SpriteBuffer;

			int index = 0;
			device.Clear(Color.Purple);
			batch.Begin();
			foreach (SpriteData data in buffer) {
				Rectangle position = data.PositionAsRectangle;
				Rectangle mask = data.Mask;
				batch.Draw(data.Texture.AsTexture2d, position, mask, Color.White);
				index++;

			}
			batch.End();
		}

		///<summary> Generates SpriteData.Mask using GameFont.PositionData</summary>
		public static Rectangle GenerateMaskFromPositionData(PositionData data) {
			return new Rectangle(data.X, data.Y, data.Width, data.Height);
		}
	
	}
}
