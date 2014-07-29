using System;
using System.Collections.Generic;

using MauronAlpha.Geometry;

namespace MauronAlpha.GameEngine.Rendering.Textures {

	//A texture that draws only parts of itself
	public class GameTexture_selective : Drawable_texture {
		public I_GameTexture Texture;
		public GameTexture_selective (GameAsset asset, I_Drawable parent, string name): base(TextureType_selective.Instance, parent, name) {
			
		}
	}
	//a texture which can select WHAT to draw of its source
	public class TextureType_selective : TextureType {
		#region Singleton
		private static volatile TextureType_selective instance=new TextureType_selective();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static TextureType_selective ( ) { }
		public static TextureType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new TextureType_selective();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "selective"; } }

		public override I_GameTexture CreateFromAsset (GameAsset asset, I_Drawable parent, string name) {
			return new GameTexture_selective(asset, parent, name);
		}

		public override bool WillCut { get { return true; } }
		public override bool NeedsRender { get { return true; } }

	}

}
