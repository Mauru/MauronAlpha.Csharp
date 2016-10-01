namespace MauronAlpha.MonoGame.Assets {

	public class AssetTypes:MonoGameComponent {
		public static AssetType_Font Font { 
			get {
				return AssetType_Font.Instance;
			}
		}
		public static AssetType_Texture Texture {
			get {
				return AssetType_Texture.Instance;
			}
		}
		public static AssetType_Shader Shader {
			get {
				return AssetType_Shader.Instance;
			}
		}
	}

	public sealed class AssetType_Shader :AssetType {
		public override string Name { get { return "Shader"; } }
		public override string FileExtension {
			get { return "mgfxo"; }
		}
		
		static object _sync= new System.Object();
		static volatile AssetType_Shader _instance;

		AssetType_Shader() : base() { }

		public static AssetType_Shader Instance {
			get {
				if(_instance == null) {
					lock(_sync) {
						_instance = new AssetType_Shader();
					}
				}

				return _instance;
			}
		}
	}
	public sealed class AssetType_Texture :AssetType {
		public override string Name { get { return "Texture"; } }
		public override string FileExtension {
			get { return "png"; }
		}
		
		static object _sync= new System.Object();
		static volatile AssetType_Texture _instance;

		AssetType_Texture() : base() { }

		public static AssetType_Texture Instance {
			get {
				if(_instance == null) {
					lock(_sync) {
						_instance = new AssetType_Texture();
					}
				}

				return _instance;
			}
		}
	}
	public sealed class AssetType_Font :AssetType {
		public override string Name { get { return "Font"; } }
		public override string FileExtension {
			get { return "fnt"; }
		}

		static volatile AssetType_Font _instance;
		static object _sync= new System.Object();

		AssetType_Font() : base() { }
		public static AssetType_Font Instance {
			get {
				if(_instance == null) {
					lock(_sync) {
						_instance = new AssetType_Font();
					}
				}
				return _instance;
			}
		}
	}
}
