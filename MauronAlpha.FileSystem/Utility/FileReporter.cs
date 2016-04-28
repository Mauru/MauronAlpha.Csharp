using MauronAlpha.FileSystem.Units;
using MauronAlpha.FileSystem.Collections;
using MauronAlpha.HandlingData;

namespace MauronAlpha.FileSystem {


	public class FileReporter:MauronAlpha.FileSystem.FileSystem_component {

		private FileStructure FileSystem;

		public FileReporter(FileStructure f) {
			FileSystem = f;
		}

		public MauronCode_dataList<string> GetFilesInCompilePath() {
			DirectoryContent dir = new DirectoryContent(FileSystem.Root);
			DataPile<string> result = ContentAsMap(dir);
			string[] keys = new string[]{"Directories","Files"};
			return result.Combined(keys);
		}

		public DataPile<string> ContentAsMap(DirectoryContent content) {
			MauronCode_dataList<Directory> dirs = content.Directories;
			MauronCode_dataList<File> files = content.Files;

			DataPile<string> result = new DataPile<string>();
			MauronCode_dataList<string> strDir = new MauronCode_dataList<string>();
			MauronCode_dataList<string> strFile = new MauronCode_dataList<string>();
			foreach(Directory d in dirs)
				strDir.Add(d.Name);
			foreach(File f in files)
				strFile.Add(f.Name+f.Extension);
			result.SetValue("Directories",strDir);
			result.SetValue("Files",strFile);
			return result;
		}
	}
}
