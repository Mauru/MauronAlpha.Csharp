using System;

using MauronAlpha.GameEngine.Rendering.Textures;
using MauronAlpha.GameEngine.Rendering;
using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Text {

	//A character in a word
	public class TextComponent_char : GameTexture_selective, I_TextComponent {
		public char Character;
		public readonly bool IsDelimiter;

		//constructor
		public TextComponent_char (TextComponent_word parent, char c)
			: base(parent.Source.Font.Asset,parent,"chr."+Char.GetNumericValue(c)) {
			Character=c;
			IsDelimiter=TextDisplay.WordSplitCharacters.Contains(Character);
			SetBounds( new Rectangle2d(0,0,Source.Font.CharacterSize(Character).X, Source.Font.CharacterSize(Character).Y));
		}

		//return as string
		public string AsString {
			get {
				return ""+Character;
			}
		}

		public double Height {
			get { return Bounds.Height; } 
			set { throw new NotImplementedException(); }
		}
		public double Width {
			get { return Bounds.Width; } 
			set { throw new NotImplementedException(); }
		}

		//return RenderData.WithProperty("Mask", SpriteFont.CharacterMask(Character));

		/*
		public override I_GameTexture[] Textures {
			get {
				return new I_GameTexture[1] { Source.Font.Texture };
			}
		}
		 * */

		public override Rectangle2d GetPixelArea ( ) {
			return Source.Font.CharacterMask(Character);
		}
		public override Polygon2d Mask {
			get {
				return Source.Font.Texture.Bounds;
			}
		}

		// What Text Display are you attached to?
		public TextDisplay Source {
			get { return Parent.Source; }
		}

	}

}
