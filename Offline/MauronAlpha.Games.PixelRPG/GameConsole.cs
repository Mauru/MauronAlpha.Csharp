using MauronAlpha.Geometry;
using MauronAlpha.GameEngine;
using MauronAlpha.GameEngine.Text.Fonts;
using MauronAlpha.GameEngine.Text;

using System.Collections.Generic;
using System;

namespace MauronAlpha.Games.PixelRPG {

	//the Game Console
	public class GameConsole : Drawable_container {
		private GameLogic Logic;
		private SpriteFont SpriteFont;
		private Rectangle2d TextArea=new Rectangle2d();

		public Stack<TextDisplay> Messages = new Stack<TextDisplay>();

/* inheriting GameComponent_placeable */

		public override double Height {
			get { 
				return D_height;
			}
			set { throw new NotImplementedException(); }
		}

		public override double Width {
			get { 
				return D_width;
			}
			set { throw new NotImplementedException(); }
		}

/* Independent Code */

		//constructor
		public GameConsole(Drawable parent, GameLogic logic):base(parent,"GameConsole") {
			Logic = logic;
			//create raw fontmap
			Drawable_texture texture=TextureManager.Instance.MakeTexture(
				TextureType_selective.Instance,
				GameAssetManager.Instance.Assets["AnonymousPro.regular.15px.texture"],
				"ConsoleFont"
			);

			//fontfile
			GameAsset fontfile=GameAssetManager.Instance.Assets["AnonymousPro.regular.15px.fontfile"];
			SpriteFont=new SpriteFont("ConsoleFont", "AnonymousPro.regular.15px.fontfile","ConsoleFont");
		}

		//Write text to console
		public void Write(string text) {
			TextDisplay textobject=new TextDisplay(this, SpriteFont,TextArea,"ConsoleMessage");
			textobject.Render(text);
			Messages.Push(textobject);
			AddChild(textobject);
			SheduleRedraw();
		}

		public override GameDrawData Draw (System.TimeSpan drawcycle) {
			GameDrawData = new GameDrawData(this,drawcycle);
			return GameDrawData;
		}

		public override void E_HasRendered ( ) {
			throw new System.NotImplementedException();
		}
	}

}