using System;
using System.Collections.Generic;


using MauronAlpha.GameEngine.Rendering;
using MauronAlpha.GameEngine.ObjectRelations;
using MauronAlpha.GameEngine.Text.Fonts.SpriteFonts;
using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Text {

/* The main Text displaying Object */

	//A drawable that displays text via a spritefont
	public class TextDisplay : Drawable_container {

		//The Font the display uses
		SpriteFont SF_font;
		public SpriteFont Font { 
			get {
				if(SF_font==null) {
					throw new GameCodeError("No SpriteFont set",this);
				}
				return SF_font;
			}
		}
		internal TextDisplay SetFont(SpriteFont font){
			SF_font=font;
			return this;
		}

		//Special Characters
		public static List<char> WordSplitCharacters=new List<char> { ' ', '-', '.', ':', ';', '\t', '\r', '\n' };
		public static List<char> LinebreakCharacters=new List<char> { '\r', '\n' };

		//constructor
		public TextDisplay (Drawable parent, string name, SpriteFont font)
			: base(parent, name) {
			SetFont(font);
		}

		#region Old File 
		/*
		private I_TextComponent D_text;

		//Required Methods and Properties (TODO)
		public double Width {
			get { return FirstChild.Bounds.Width; }
			set { throw new NotImplementedException(); }
		}
		public double Height {
			get { return FirstChild.Bounds.Height; }
			set { throw new NotImplementedException(); }
		}
		
		//specific properties
		public SpriteFont SpriteFont;
		public string Text="";
		public Rectangle2d MaxSize;


		public short LineHeight { get{ return SpriteFont.CharacterHeight; } }



		//The Render call string->texture
		public void Render(string text) {
			this.Text = text;
			//text->charstack
			Stack<Stack<char>> charstack=TextConvert.StringToCharStack(text);
			TextComponent_text d = new TextComponent_text(this,charstack,MaxSize,"Text."+Name);
			Height =d.Height;
			Width = d.Width;
			AddChild(d);
		}

/* Defines Rendering */
/*
		//apply markup if necessary
		public TextMarkup Markup {
			get{
				return TextMarkup_default.Instance;
			}
		}

		//apply Alignment if necessary
		public TextAlignment Alignment {
			get{ 
				return new TextAlignment_Left();
			}
		}


		public override void GenerateRenderData ( ) {
			throw new System.NotImplementedException();
		}

		public override Rectangle2d Bounds {
			get { throw new System.NotImplementedException(); }
		}*/
		#endregion
		public override void GenerateRenderData ( ) {
			throw new NotImplementedException();
		}
	}

}