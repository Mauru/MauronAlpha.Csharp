using MauronAlpha.GameEngine.Events;
using MauronAlpha.GameEngine.Rendering;

using System;
using System.Collections.Generic;

namespace MauronAlpha.GameEngine.Rendering.Textures {

	// Texture Manager
	public sealed class GameTextureManager : GameComponentManager, I_GameComponentManager {
		#region Singleton
		//setting up the singleton
		private static volatile GameTextureManager instance=new GameTextureManager();
		private static object syncRoot=new Object();

		//constructor singleton multithread safe
		static GameTextureManager ( ) { }
		public static GameTextureManager Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new GameTextureManager();
					}
				}
				return instance;
			}
		}
		#endregion

		//The Game
		MauronAlphaGame GameEngine;

		//Initialize
		public void Initialize (MauronAlphaGame gameengine) {
			GameEngine=gameengine;
		}

		//Handling textures
		private Dictionary<string, I_GameTexture> Textures = new Dictionary<string, I_GameTexture>();

		//Create a new texture
		public I_GameTexture MakeTexture (TextureType texturetype, GameAsset asset, I_Drawable parent, string name) {
			if( HasTexture(name)&&!GetTexture(name).TextureType.AllowOverwrite ) {
				return GetTexture(name);
			}
			I_GameTexture g=texturetype.CreateFromAsset(asset, parent, name);

			Textures[name]=g;
			return g;
		}

		//does this texture exist
		public bool HasTexture (string name) {
			return Textures.ContainsKey(name);
		}

		//return a texture
		public I_GameTexture GetTexture (string name) {
			if( !Textures.ContainsKey(name) ) {
				throw new GameCodeError("Texture ["+name+"] does not Exist!", this);
			}
			return Textures[name];
		}

		//Register a texture
		public void Register (I_GameTexture texture, string name) {
			if( Textures.ContainsKey(name) ) {
				throw new GameCodeError("Texture ["+name+"] allready exists", this);
			}
			Textures[name]=texture;
		}

		#region I_GameEventSender
		public override void SendEvent (GameEvent ge) {
			throw new NotImplementedException();
		}
		public override GameEventShedule EventShedule {
			get { throw new NotImplementedException(); }
		}
		public override bool CheckEvent (I_GameEventSender d) {
			throw new System.NotImplementedException();
		}
		#endregion

		#region I_GameEventListener
		public override void ReceiveEvent (GameEvent ge) {
			throw new NotImplementedException();
		}
		public override void IsEventCondition (GameEvent ge) {
			throw new NotImplementedException();
		}
		#endregion

	}

}