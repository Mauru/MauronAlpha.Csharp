namespace MauronAlpha.Files.FileTypes {

    public abstract class FileType:FileComponent {
		public abstract string Name { get; }
		public abstract string Extension { get; }
		public abstract string Description { get; }
    }

}
