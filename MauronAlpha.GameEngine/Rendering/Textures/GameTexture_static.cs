using System;

namespace MauronAlpha.GameEngine.Rendering.Textures {

	// A static texture
	public class GameTexture_static : Drawable_texture {
		
		private GameAsset GA_asset;
		public override GameAsset Asset { get { return GA_asset; } }
		public GameTexture_static (GameAsset asset, I_Drawable parent, string name) : base(TextureType_static.Instance,parent ,name) { 
			GA_asset=asset;
		}

	}
	//a generic single drawcall texture
	public sealed class TextureType_static : TextureType {
		#region Singleton
		private static volatile TextureType_static instance=new TextureType_static();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static TextureType_static ( ) { }
		public static TextureType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new TextureType_static();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "static"; } }

		public override I_GameTexture CreateFromAsset (GameAsset asset, I_Drawable parent, string name) {
			throw new NotImplementedException();
		}
	}

}