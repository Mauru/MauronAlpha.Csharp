namespace MauronAlpha.GameEngine.Rendering.Textures {

	// Texture Types
	public abstract class TextureType : MauronCode_subtype {

		public TextureType():base() {}
		
		public abstract string Name { get; }

		// Create the Texture from an asset
		public abstract I_GameTexture CreateFromAsset (GameAsset asset, I_Drawable parent, string name);
		
		//Is the texture a static object which can simply be rendered?
		public virtual bool IsStatic { get { return false; } }
		
		//can the texture be recreated
		public virtual bool AllowOverwrite { get { return false; } }

		//can the texture be recreated
		public virtual bool NeedsRender { get { return false; } }

		// will the texture be a part of an image asset
		public virtual bool WillCut { get { return false; } }

	}

}