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

		string _fileName;
		public string FileName {
			get { return _fileName; }
		}

		public LoadRequest(string assetName, string fileName, AssetType type) : base() {
			_name = assetName;
			_type = type;
			_fileName = fileName;
		}

		public bool IsFont {
			get {  return _type.Equals(AssetTypes.Font);}
		}
		public bool IsTexture {
			get {  return _type.Equals(AssetTypes.Texture);}
		}
		public bool IsShader {
			get {
				return _type.Equals(AssetTypes.Shader);
			}
		}
	}
}
