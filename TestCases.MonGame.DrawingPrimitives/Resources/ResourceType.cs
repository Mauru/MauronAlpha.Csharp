namespace MauronAlpha.MonoGame.Resources {
	public abstract class ResourceType : MonoGameComponent {
		public abstract string Name { get; }

		public static ResourceType_gameFont GameFont {
			get {
				return ResourceType_gameFont.Instance;
			}
		}

		public static ResourceType_gameTexture GameTexture {
			get {
				return ResourceType_gameTexture.Instance;
			}
		}

		public bool Equals(ResourceType other) {
			return Name.Equals(other.Name);
		}
	}
}
