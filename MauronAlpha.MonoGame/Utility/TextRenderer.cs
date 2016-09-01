﻿namespace MauronAlpha.MonoGame.Utility {
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

	public class TextRenderer:MonoGameComponent {

		public static GameRenderer.RenderMethod RenderMethod {
			get {
				return RenderText;
			}
		}

		public static I_RenderResult RenderText(RenderStage stage, I_Renderable target, long time) {

			TextDisplay text = (TextDisplay) target;
			SpriteBatch batch = stage.Caller;
			GameFont font = text.Font;

			batch.Begin();

			Lines ll = text.Lines;
			Rectangle o;
			Vector2 pos=new Vector2();

			foreach(Line l in ll) {
				Characters cc = l.Characters;
				foreach(Character c in cc) {

					PositionData p = font.FetchCharacterData(c.Symbol);
					MonoGameTexture t = font.TextureByPageIndex(p.FontPage);
					o = new Rectangle(p.X,p.Y,p.Width,p.Height);
					Color m = Color.Red;
					Vector2d v = text.Position;
					pos.X = v.FloatX;
					pos.Y = v.FloatY;
				
					batch.Draw(t.Texture, pos, o, m);
				}
				
			}

			batch.End();
			return new RenderResult(time, target, stage.AsTexture2D);

		}

		public static void RenderTextOld(TextDisplay text, ref RenderTarget2D scene, ref SpriteBatch batch, GameFont font) {



			
			
		}
	
	}
}