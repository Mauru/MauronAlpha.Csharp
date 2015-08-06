using MauronAlpha.Files.Units;

namespace MauronAlpha.Files.DataObjects {
	
	public class FileSize:FileComponent {

		//constructor
		public FileSize(FileUnit_file file) : base() {
			UNIT_file = file;
		}

		private FileUnit_file UNIT_file;
		public FileUnit_file Source {
			get {
				return UNIT_file;
			}
		}

		public long AsBytes {
			get {
				return UNIT_file.Location.FileSystem.FileSize(UNIT_file);
			}
		}
	}
}
