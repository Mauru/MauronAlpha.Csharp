namespace MauronAlpha.FileSystem.DataObjects {
	
	public abstract class FileType:FileSystem_component {

		public abstract string Name { get; }
		public abstract bool IsDirectory { get; }

	}

}
