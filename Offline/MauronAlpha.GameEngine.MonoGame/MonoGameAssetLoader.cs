using MauronAlpha.GameEngine;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.IO;


namespace MauronAlpha.GameEngine.MonoGame {

//Handles content loading
public class MonoGameAssetLoader : MauronAlpha.GameEngine.GameAssetLoader {

	public override GameAssetLoader Instance {
		get {
			GameAssetLoader ret=new MonoGameAssetLoader();
			return ret;
		}
	}

	protected override GameAsset performLoad ( ) {
		if( Target.AssetType.Name==GameAssetType_image.Instance.Name ) {
			Debug("Asset is a image",this);
			Debug(Target.Name,Target);
			Texture2D result=GameEngine.ContentLoader.Load<Texture2D>(MonoGameTexture);
			if(result==null) {
				throw new GameCodeError("Result was null!", this);
			}
			Target.Result=result;
			Target.ResultType=result.GetType();
			Target.Loaded=true;
			base.Callback();
			return Target;
		}
		if( Target.AssetType.Name==GameAssetType_data.Instance.Name ) {
			Debug("Asset is a piece of data", this);
			DataResource d=(DataResource) Target;
			var fontFilePath=Path.Combine(GameEngine.ContentLoader.RootDirectory, Target.FileName);
			using( var stream=TitleContainer.OpenStream(fontFilePath) ) {
				// textRenderer initialization will go here
				d.Load(stream);
				stream.Close();
			}
			Target.Result=d.Data;
			Target.ResultType=d.Data.GetType();

			Target.Loaded=true;
			base.Callback();
			return Target;
		}

		//unknown asset type
		throw new GameCodeError("Unknown Asset Type "+Target.AssetType.Name,this);
	}
	private string MonoGameTexture { get { return Target.FileName; } }
	protected MauronAlpha_MonoGame GameEngine { get { return (MauronAlpha_MonoGame) base.AssetManager.GameEngine; } }
}

}