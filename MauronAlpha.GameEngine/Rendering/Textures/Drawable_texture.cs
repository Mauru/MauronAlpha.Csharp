using System;

using MauronAlpha.Geometry._2d;

namespace MauronAlpha.GameEngine.Rendering.Textures {

	//Drawable: No children, has content
	public abstract class Drawable_texture : Drawable, I_GameTexture {

#region I_GameTexture

		public readonly TextureType TextureType;
		public virtual GameAsset[] Assets {
			get {
				throw new NotImplementedException();
			}
		}
		public virtual GameAsset Asset {
			get {
				if (Assets.Length>1) {
					throw new GameCodeError("First time more than one asset is asked for",this);
				}
				if (Assets.Length<1){
					throw new GameCodeError("No asset availiable",this);
				}
				return Assets[0];
			}
		}
		public virtual I_GameTexture CreateFromAsset(GameAsset Asset, I_Drawable parent, string name) {
			return TextureType.CreateFromAsset(Asset, parent,name);
		}

#endregion



		public bool Loaded {
			get {
				if( Asset==null ) {
					return false;
				}
				return Asset.Loaded;
			}
		}

		public Drawable_texture (TextureType texturetype, I_Drawable parent, string name)
			: base(DrawableType_texture.Instance, parent,name) {
			TextureType=texturetype;

			if( GameTextureManager.HasTexture(name) ) {
				Debug("Duplicate Texture "+name, this);
			}
			else {
				GameTextureManager.Register(this, name);
			}
		}

		private GameTextureManager GameTextureManager { get { return GameTextureManager.Instance; } }
		
		public virtual Rectangle2d GetPixelArea ( ) {
			if( !Asset.Loaded ) {
				throw new GameCodeError("Asset has not loaded yet!", this);
			}
			return new Rectangle2d();
		}
		public virtual Polygon2d GetMask ( ) {
			return new Polygon2d();
		}


		public override void GenerateRenderData ( ) {
			throw new NotImplementedException();
		}

		public override Rectangle2d Bounds {
			get { throw new NotImplementedException(); }
		}

		TextureType I_GameTexture.TextureType {
			get { throw new NotImplementedException(); }
		}

		public virtual Polygon2d Mask {
			get { throw new NotImplementedException(); }
		}

		public Rectangle2d PixelArea {
			get { throw new NotImplementedException(); }
		}
	}

	//Type definition
	public sealed class DrawableType_texture : DrawableType {
		#region Singleton Definition
		private static volatile DrawableType_texture instance=new DrawableType_texture();
		private static object syncRoot=new Object();
		static DrawableType_texture ( ) { }
		public static DrawableType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new DrawableType_texture();
					}
				}
				return instance;
			}
		}
		#endregion
		public override string Name { get { return "texture"; } }
		public override bool IsContent { get { return true; } }
	}	

}