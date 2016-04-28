using MauronAlpha.FileSystem.DataObjects;
using MauronAlpha.FileSystem.Units;
namespace MauronAlpha.FileSystem.Collections {

	public class FileTypes:FileSystem_component {

		public static FileType_generic Generic {
			get {
				return FileType_generic.Instance;
			}
		}

		public static FileType_directory Directory {
			get {
				return FileType_directory.Instance;
			}
		}

	}
}
