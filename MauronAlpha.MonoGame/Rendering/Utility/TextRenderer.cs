namespace MauronAlpha.MonoGame.Utility {
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

	public class TextRenderer:MonoGameComponent, I_Renderer {

		public static GameRenderer.RenderMethod RenderMethod {
			get {
				return RenderTextToTexture;
			}
		}

		public static I_RenderResult RenderTextToTexture(RenderStage stage, I_Renderable target, long time) {
			stage.SetAsRenderTarget();
			TextDisplay text = (TextDisplay) target;
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
					
					PositionData p = font.FetchCharacterData(c.Symbol);
					MonoGameTexture t = font.TextureByPageIndex(p.FontPage);
					o = new Rectangle(p.X,p.Y,p.Width,p.Height);
					Color m = Color.Red;
					Vector2d v = text.Position;
					pos.X = v.FloatX;
					pos.Y = v.FloatY;
					lineSize.Add(p.Width);
				
					batch.Draw(t.Texture, pos, o, m);
				}
				if(lineSize.X>finalSize.X)
					finalSize.SetX(lineSize.X);
				
				finalSize.Add(0,lineSize.Y);				
			}

			batch.End();
			
			return new RenderResult(time, target, stage.AsTexture2D);

		}

		GameRenderer.RenderMethod I_Renderer.RenderMethod {
			get { return RenderMethod; }
		}
	
	}
}
