using MauronAlpha.Geometry;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace MauronAlpha.GameEngine.MonoGame {

	//An object which contains render Data
	public class MonoGameRenderData : MauronCode_dataobject {
		public RenderTarget2D RenderTarget;
		public Texture2D Result;
		public MonoGameRenderData(RenderTarget2D renderTarget) {
			RenderTarget=renderTarget;
			Result = (Texture2D) renderTarget;
		}
		public MonoGameRenderData (Texture2D texture) {
			Result=texture;
		}
	}

	//Cut a texture
	public class MonoGameTextureRenderer : MauronCode_utility {
		public static KeyValuePair<SpriteBatch,Texture2D> CutTexture(MauronAlpha_MonoGame Engine, GameDrawData part, GameTime gametime, Texture2D monogametexture, Rectangle2d area) {
			//Is the Mask as large as area we want to cut?
			Rectangle2d texturesize=MonoGameConvert.Rectangle2Rectangle2d(monogametexture.Bounds);
			if(texturesize.Equals(area)){
				//cool, just return the texture
				return new KeyValuePair<SpriteBatch,Texture2D>(null,monogametexture);
			}
			//Bah We have to render
			RenderTarget2D renderTarget=MonoGameRenderer.CreateRenderTarget(Engine,part,gametime,area);

			//Step 2: Tell Gfx to render to rendertarget
			Engine.GraphicsDevice.SetRenderTarget(renderTarget);

			//Step 3 Set the options for the spritebatch and draw
			SpriteBatch spritebatch=MonoGameRenderer.SetupSpriteBatch(Engine, part, gametime, area);
			
			//step 4 draw the texture
			Rectangle where =new Rectangle(0, 0, (int) area.Width, (int) area.Height);
			Rectangle from = new Rectangle((int) area.X, (int) area.Y, (int) area.Width, (int) area.Height);
			spritebatch.Draw(monogametexture,where,from,Color.White);

			return new KeyValuePair<SpriteBatch,Texture2D>(spritebatch,renderTarget);
		}
		public static GameDrawData RenderTexture(MauronAlpha_MonoGame Engine,GameDrawData part,GameTime gametime, Drawable texture){
			//this is the parent object wanting to render the texture
			Drawable parent = part.Source;
			//convert gametime since we will need it a lot
			TimeSpan frame = gametime.ElapsedGameTime;

			//this is the renderer, a drawable which intends to be a texture
			I_GameTexture<Drawable_texture> renderer = (I_GameTexture<Drawable_texture>) texture;
			//figure out if our texture has drawdata
			GameDrawData texturedata;
			if (!renderer.HasGameDrawData(frame)) {
				texturedata = renderer.Draw(frame);
			}
			else{
				texturedata = renderer.GetGameDrawData(frame);
			}
			
			//so, if we are to render a file into a texture... 
			GameAsset[] assets = renderer.GetAssets();

			//... we need an asset of type image
			if(assets.Length<1){
				throw new GameCodeError("No assets to render",texture);
			}
			GameAsset asset=assets[0];
			if( asset==null ) {
				throw new GameCodeError("Texture has no asset (null)", renderer);
			}
			else if( !asset.Loaded ) {
				throw new GameCodeError("Texture Asset has not Loaded", asset);
			}else if( asset.AssetType.Name!=GameAssetType_image.Instance.Name ){
				throw new GameCodeError("Texture Asset is not an image!", asset);
			}

			//...ok we now know we got an asset... HERE IS THE TEXTURE
			Texture2D monogametexture = (Texture2D) asset.Result;
			if( monogametexture==null ) {
				throw new GameCodeError("There is no Load Result...", asset);
			}

			RenderTarget2D renderTarget=null;
			SpriteBatch spritebatch=null;
			Texture2D wip=monogametexture;

			//Hello Texture, I might have to cut you
			if(texturedata.ContainsKey("Mask")){
				KeyValuePair<SpriteBatch, Texture2D> result=MonoGameTextureRenderer.CutTexture(Engine, part, gametime, monogametexture, (Rectangle2d) texturedata["Mask"]);
				//The original texture will work, but we do not have a spritebatch yet, and no rendertarget
				if(result.Key!=null) {
					wip=(Texture2D) result.Value;
					spritebatch=result.Key;
					renderTarget=(RenderTarget2D) result.Value;
				}
			}

			//We now are in an active rendercycle FYI

			//Finalize
			if(spritebatch!=null){
				spritebatch.End();
				Engine.GraphicsDevice.SetRenderTarget(null);
				part.Result = new MonoGameRenderData(renderTarget);
			}
			//No Modification took place
			else{
				part.Result=new MonoGameRenderData(wip);
			}
			return part;
		}
	}

	//Stores rendersettings
	public sealed class MonoGameRenderSettings : MauronCode_singleton {
		#region Singleton
		private static volatile MonoGameRenderSettings instance=new MonoGameRenderSettings();
		private static object syncRoot = new Object();
		//constructor singleton multithread safe
		static MonoGameRenderSettings ( ) { }
		public static MonoGameRenderSettings Instance {
			get {
				if (instance == null)
				{
					lock (syncRoot)
					{
						instance=new MonoGameRenderSettings();
					}
				}
				return instance;
			}
		}
		#endregion		
		public static RenderTarget2D RenderTargetFromPart(MauronAlpha_MonoGame Engine, GameDrawData part, GameTime gametime){
			return MonoGameRenderSetting_Default.RenderTarget(Engine,new Rectangle2d(part.Source.Width,part.Source.Height),gametime);
		}
		public static RenderTarget2D RenderTargetFromArea(MauronAlpha_MonoGame Engine, GameDrawData part, GameTime gametime,Rectangle2d area){
			return MonoGameRenderSetting_Default.RenderTarget(Engine,area,gametime);
		}		
		public static SpriteBatch SpriteBatchFromPart(MauronAlpha_MonoGame Engine, GameDrawData part, GameTime gametime){
			return MonoGameRenderSetting_Default.SpriteBatch(Engine,part,gametime);
		}
		public static SpriteBatch SpriteBatchFromArea (MauronAlpha_MonoGame Engine, GameDrawData part, GameTime gametime, Rectangle2d area) {
			return MonoGameRenderSetting_Default.SpriteBatch(Engine, part, gametime);
		}

	}

	//Default rendersetting
	public class MonoGameRenderSetting_Default : MonoGameRenderSetting {
		public static SpriteBatch SpriteBatch(MauronAlpha_MonoGame Engine, GameDrawData part, GameTime gametime){
			SpriteBatch spritebatch = new SpriteBatch(Engine.GraphicsDevice);
			spritebatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone);
			return spritebatch;
		}
		public static RenderTarget2D RenderTarget(MauronAlpha_MonoGame Engine, Rectangle2d rectangle, GameTime gametime){
			//gfx device,
			//width, height,(graphicsDevice.PresentationParameters for screen)
			//usemipmap, (bool) mipmapchain (i.e. drawdistance performance)
			//surfaceformat, - prefered data format
			//depthformat, - Z-drawmode on pixels
			//multisamplecount, - how much antialias
			//rendertargetusage - what to do with the data after render		
			return new RenderTarget2D(
				Engine.GraphicsDevice,
				(int) rectangle.Width,
				(int) rectangle.Height,
				false,
				SurfaceFormat.Alpha8,
				DepthFormat.Depth16,
				0,
				RenderTargetUsage.PreserveContents
			);
		}
	}

	//stores a rendersetting
	public class MonoGameRenderSetting : GameRenderSetting {}

}