namespace MauronAlpha.MonoGame.Assets {

	/// <summary> A request to load a game-asset</summary>
	public class LoadRequest:MonoGameComponent {

		AssetType _type;
		string _name;
		public string Name {
			get {
				return _name;
			}
		}

		public LoadRequest(string name, AssetType type) : base() {
			_name = name;
			_type = type;
		}

		public bool IsFont {
			get {  return _type.Equals(AssetTypes.Font);}
		}
		public bool IsTexture {
			get {  return _type.Equals(AssetTypes.Texture);}
		}

	}
}
