namespace MauronAlpha.MonoGame.Utility {
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework;


	using MauronAlpha.TextProcessing.Collections;
	using MauronAlpha.TextProcessing.Units;

	using MauronAlpha.FontParser.DataObjects;

	using MauronAlpha.Geometry.Geometry2d.Units;

	using MauronAlpha.MonoGame.DataObjects;

	using MauronAlpha.MonoGame.UI.DataObjects;

	public class TextRenderer:MonoGameComponent {


		public void RenderText(TextDisplay text, RenderTarget2D scene, SpriteBatch batch, GameFont font) {

			Lines ll = text.Lines;


			Rectangle o;

			foreach(Line l in ll) {
				Characters cc = l.Characters;
				foreach(Character c in cc) {

					PositionData p = font.FetchCharacterData(c.Symbol);
					MonoGameTexture t = font.TextureByPageIndex(p.FontPage);
					o = new Rectangle(p.X,p.Y,p.Width,p.Height);
					Color m = Color.Red;
					batch.Draw(t, text.PositionAsVector2, o, m);
				}
				
			}

			
			
		}
	}
}
