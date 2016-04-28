using MauronAlpha.FileSystem;
using MauronAlpha.FileSystem.Units;


namespace MauronAlpha.Testcases.FileSystem {

	public class FileManager:FileSystem_component {

		public FileManager(FileStructure system): base() {
			this.DATA_system = system;
		}

		private FileStructure DATA_system;

		public Directory CreateDirectory(string name) {
			return CreateDirectory(CurrentDir, name);
		}
		public Directory CreateDirectory(Directory parent, string name) {
			Directory newDir = new Directory(parent, name);
			bool result = DATA_system.CreateDirectory(newDir);
			if (!result)
				throw new System.Exception("Could not create Directory at specified location.");
			return newDir;
		}

		private Directory DATA_active;
		public Directory CurrentDir {
			get {
				if (DATA_active == null)
					DATA_active = DATA_system.Root;
				return DATA_active;
			}
		}

		public File CreateFile(Directory parent, string name, string extension) {
			File f = new File(parent, name, extension);
			bool result = DATA_system.CreateFile(f);
			return f; 
		}

	}

}
