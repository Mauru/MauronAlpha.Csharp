using System;
using MauronAlpha.Geometry;
using MauronAlpha.GameEngine.Rendering;

using Microsoft.Xna.Framework.Graphics;

namespace MauronAlpha.GameEngine.MonoGame.Rendering {

	//The drawing interface between MonoGame and Mauron Alpha
	public class MonoGameRenderer : MauronCode_utility {

		//draw a part
		public static GameDrawData RenderPart (MauronAlpha_MonoGame Engine, GameDrawData part) {
			if( part==null ) {
				throw new GameCodeError("Can't render, part is null!", Engine);
			}
			//Get the source oof the data object
			if( part.Source==null ) {
				throw new GameCodeError("That part does not have a source item", part);
			}
			Drawable drawable=part.Source;

			Debug("Drawing "+drawable.Name+" : "+drawable.DrawableType.Name, drawable);

			//Does the drawable have children?
			if( drawable.DrawableType.IsDrawParent ) {

				Debug("Is container with ("+drawable.Children.Count+") Children", Engine);

				foreach( Drawable child in drawable.Children ) {

					child.GameDrawData=RenderPart(Engine, child.GameDrawData);
				}

				part=RenderFromChildren(Engine, part);
			}

			if( drawable.DrawableType.IsContent ) {

				Debug(drawable.DrawableType.Name+" is container with content", drawable);

				part=RenderFromGameTexture(Engine, part);

			}

			Debug("Object is now rendered "+part.Source.Name, part.Source);


			return part;
		}

		//Render a image file (GameTexture)
		public static GameDrawData RenderFromGameTexture (MauronAlpha_MonoGame Engine, GameDrawData part) {
			Drawable source=part.Source;
			Drawable_texture[] textures=source.Textures;
			Drawable_texture texture;

			Debug("Texture Source is "+source.Name, source);

			//step 0: do we have any textures to work with
			if( textures.Length<1 ) {
				throw new GameCodeError("No textures to render", source);
			}
			else {
				texture=textures[0];
			}

			//step 1: Check if the texture needs to be rendered
			Debug("Texturetype is "+texture.TextureType.Name, source);

			if( texture.TextureType.NeedsRender ) {
				GameDrawData texturedata=MonoGameTextureRenderer.RenderTexture(Engine, part, texture);
			}

			return part;
		}

		//Render a part's texture from its children
		public static GameDrawData RenderFromChildren (MauronAlpha_MonoGame Engine, GameDrawData part) {
			Drawable source=part.Source;
			RenderTarget2D renderTarget;
			MonoGameRenderData renderData;

			//Step 1: Is there allready a RenderTarget?
			if( part.Result!=null ) {
				Debug("RenderData exists", part);
				renderData=(MonoGameRenderData) part.Result;
				renderTarget=renderData.RenderTarget;
			}
			//Step 1.1: no, create one
			else {
				//! Surface format: is set to GIFlike - Depthformat (for depthbuffer, i.e. Normal maps) is set to None)
				renderTarget=CreateRenderTarget(Engine, part, gametime);
				renderData=new MonoGameRenderData(renderTarget);
			}
			Debug("Rendering children of "+source.Name, source);
			//Step 2: Tell Gfx to render to rendertarget
			Engine.GraphicsDevice.SetRenderTarget(renderTarget);

			//Step 3 Set the options for the spritebatch and draw
			SpriteBatch spritebatch=SetupSpriteBatch(Engine, part, gametime);

			//Step 4: Cycle through all children and render them
			foreach( Drawable child in source.Children ) {
				spritebatch=DrawObject(Engine, part, gametime, spritebatch, child);
			}

			//Step 5: End Rendering
			spritebatch.End();

			//Step 6: Reset RenderTarget
			//(supposedly do this before getting texture)
			Engine.GraphicsDevice.SetRenderTarget(null);

			//Step 7: Get RenderData of container
			renderData.RenderTarget=renderTarget;
			part.SetResult(renderData, gametime);
			part.LastDrawCycle=MonoGameConvert.GameTime2TimeSpan(gametime);

			//step 8: Return
			return part;
		}

		//Draw an object's renderdata onto a spritebatch
		//! This does not actually render the object's data!
		public static SpriteBatch DrawObject (MauronAlpha_MonoGame Engine, GameDrawData part, Drawable child) {
			MonoGameRenderData renderdata=(MonoGameRenderData) child.GameDrawData.Result;

			if( renderdata==null ) {
				throw new GameCodeError("Renderdata is not set", child);
			}

			Debug("Drawing object "+child.Name, child);
			//Step 4.2: draw the texture 
			//this is missing some parameters:
			// (float) rotation,
			// (Vector2) origin,
			// (Vector2) scale,
			// (SpriteEffects) effects,
			// (single) layerDepth)
			// NULL in this case is (Rectangle2) SourceRectangle for clipping (use this for spritefonts)!
			spritebatch.Draw(renderdata.Result, MonoGameConvert.Vector(child.Position), null, Color.White);

			return spritebatch;
		}

		//setup a spritebatch for drawing to it
		public static SpriteBatch SetupSpriteBatch (MauronAlpha_MonoGame Engine, GameDrawData part) {
			return MonoGameRenderSettings.SpriteBatchFromPart(Engine, part);
		}
		public static SpriteBatch SetupSpriteBatch (MauronAlpha_MonoGame Engine, GameDrawData part, Rectangle2d area) {
			return MonoGameRenderSettings.SpriteBatchFromArea(Engine, part, area);
		}

		//Create a target (texture) to render into
		public static RenderTarget2D CreateRenderTarget (MauronAlpha_MonoGame Engine, GameDrawData part) {
			return MonoGameRenderSettings.RenderTargetFromPart(Engine, part);
		}
		public static RenderTarget2D CreateRenderTarget (MauronAlpha_MonoGame Engine, GameDrawData part, Rectangle2d area) {
			return MonoGameRenderSettings.RenderTargetFromArea(Engine, part, area);
		}

	}


}
