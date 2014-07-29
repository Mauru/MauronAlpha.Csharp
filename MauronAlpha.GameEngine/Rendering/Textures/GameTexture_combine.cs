using System;
using System.Collections.Generic;

using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Rendering.Textures {

	//A Texture that combines several other textures
	public class GameTexture_combine : Drawable_texture {
		public Dictionary<I_GameTexture, Vector2d> Offsets=new Dictionary<I_GameTexture, Vector2d>();

		public GameTexture_combine (GameAsset asset, I_Drawable parent, string name) : base(TextureType_combine.Instance, parent, name) { }

		public override GameAsset Asset {
			get { throw new NotImplementedException(); }
		}
	}
	//a texture type that can combine other textures
	public class TextureType_combine : TextureType {
		#region Singleton
		private static volatile TextureType_combine instance=new TextureType_combine();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static TextureType_combine ( ) { }
		public static TextureType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new TextureType_combine();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "combine"; } }

		public override I_GameTexture CreateFromAsset (GameAsset asset, I_Drawable parent, string name) {
			throw new NotImplementedException();
		}
	}

}
