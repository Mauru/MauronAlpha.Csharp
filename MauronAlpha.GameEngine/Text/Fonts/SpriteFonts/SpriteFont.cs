// Originally By DeadlyDan @ deadlydan@gmail.com , but heavyily modified
using System.Collections.Generic;
using System;

using MauronAlpha.GameEngine.Rendering.Textures;
using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Text.Fonts.SpriteFonts {

	/* Defines a Spritefont */
	public class SpriteFont:GameResource {
		public I_GameTexture Texture;
		public FontFile Source;
		public string Name="SpriteFont";
		public Dictionary<char,FontChar>FontMap=new Dictionary<char,FontChar>();
		
		// constructors
		public SpriteFont(I_GameTexture texture, GameAsset sourcefile, string name){
			Texture=texture;
			Name=name;

			Asset= (GameAsset_spritefontinfo) sourcefile;
			Source=Asset.FontFile;

			foreach(FontChar fc in Source.Chars){
				char c=(char)fc.ID;
				FontMap[c]=fc;
			}
		}
		public SpriteFont(string texture, string fontfile, string name) {
			Texture = GameTextureManager.Instance.GetTexture(texture);
			Name=name;
			Asset = (GameAsset_spritefontinfo) GameAssetManager.Instance.Assets[fontfile];
			Source = Asset.FontFile;

			foreach( FontChar f in Source.Chars ) {
				char c=(char) f.ID;
				FontMap[c]=f;
			}
		}

		// Get your Asset
		public GameAsset_spritefontinfo Asset;


		public Rectangle2d CharacterMask(char c){
			return new Rectangle2d(FontMap[c].X,FontMap[c].Y,FontMap[c].Width,FontMap[c].Height);
		}

		public short CharacterHeight { 
			get{
				return (short) Source.Common.LineHeight;
			}
		}

		public Vector2d CharacterSize(char c){
			return new Vector2d(FontMap[c].Width,Source.Common.LineHeight);
		}
 	}

}