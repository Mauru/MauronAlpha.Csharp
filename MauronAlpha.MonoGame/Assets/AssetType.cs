namespace MauronAlpha.MonoGame.Assets {

	public abstract class AssetType :MonoGameComponent {

		public abstract string Name { get; }

		public bool Equals(AssetType other) {
			return Name.Equals(other.Name);
		}
		public abstract string FileExtension { get; }
	}

}
