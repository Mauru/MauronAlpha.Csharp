using MauronAlpha.GameEngine;
using System.Collections.Generic;
using MauronAlpha.GameEngine.Text.Fonts;

namespace MauronAlpha.Games.PixelRPG {

	//List of assets to be preloaded for PixelRPG
	public class PixelRPG_Assets:GameAssetList {

		//return Assets
		public override Stack<GameAsset> List {
			get { return Assets; }
		}
	
		//List of Assets here
		public static Stack<GameAsset> Assets { 
			get {
				Stack<GameAsset> r= new Stack<GameAsset>();

			r.Push(new GameAsset(GameAssetType_image.Instance,"AnonymousPro.regular.15px.texture","anonymouspro_0.png"));
			r.Push(new FontFile("AnonymousPro.regular.15px.fontfile", "anonymouspro.fnt"));
			r.Push(new GameAsset(GameAssetType_image.Instance, "HexField.Outline", "hex_outline.png"));
			
				return r;			
			}
		}
	
	}
}