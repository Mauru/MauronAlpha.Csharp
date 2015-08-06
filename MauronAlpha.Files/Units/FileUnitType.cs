namespace MauronAlpha.Files.Units {

	public abstract class FileUnitType:FileComponent {

		public abstract string Name { get; }
		public bool Equals(FileUnitType other) {
			return Name.Equals(other.Name);
		}
		public static FileUnitType_directory Directory {
			get { return FileUnitType_directory.Instance; }
		}
		public static FileUnitType_file File {
			get { return FileUnitType_file.Instance; }
		}
	}

}
