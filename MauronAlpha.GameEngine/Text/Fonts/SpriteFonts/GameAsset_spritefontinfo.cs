namespace MauronAlpha.GameEngine.Text.Fonts.SpriteFonts {
	public class GameAsset_spritefontinfo:GameAsset {
		public FontFile FontFile { get { return (FontFile) Result; } }
		public GameAsset_spritefontinfo():base(GameAssetType_data.Instance){}
	}
}
