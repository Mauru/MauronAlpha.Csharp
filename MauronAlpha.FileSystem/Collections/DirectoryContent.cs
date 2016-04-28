using MauronAlpha.FileSystem.Units;

using MauronAlpha.HandlingData;

namespace MauronAlpha.FileSystem.Collections {
	
	public class DirectoryContent:FileSystem_component {

		public DirectoryContent(Directory directory) {
			DATA_directory = directory;
		}

		private Directory DATA_directory;
		public Directory Directory {
			get {
				return DATA_directory;
			}
		}

		private MauronCode_dataList<File> DATA_files;
		public MauronCode_dataList<File> Files {
			get {
				if (DATA_files != null)
					return DATA_files;
				string[] result = System.IO.Directory.GetFiles(Directory.Path);
				MauronCode_dataList<File> files = new MauronCode_dataList<File>();
				foreach(string str in result) {
					string fileName = System.IO.Path.GetFileNameWithoutExtension(str);
					string extension = System.IO.Path.GetExtension(str);
					File f = new File(Directory,fileName,extension);
					files.Add(f);
				}
				DATA_files = files;
				return files;
			}
		}

		private MauronCode_dataList<Directory> DATA_directories;
		public MauronCode_dataList<Directory> Directories {
			get {
				if (DATA_directories != null)
					return DATA_directories;
				string[] result = System.IO.Directory.GetDirectories(Directory.Path);
				MauronCode_dataList<Directory> files = new MauronCode_dataList<Directory>();
				foreach (string str in result) {
					string fileName = Directory.FormNameFromPath(str);
					Directory f = new Directory(Directory, fileName);
					files.Add(f);
				}
				DATA_directories = files;
				return files;
			}
		}

		public DirectoryContent Reload() {
			DATA_files = null;
			DATA_directories = null;
			return this;
		}
	
	}

}
