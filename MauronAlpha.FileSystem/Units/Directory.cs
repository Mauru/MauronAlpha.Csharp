using MauronAlpha.FileSystem.Interfaces;
using MauronAlpha.FileSystem.DataObjects;
using MauronAlpha.FileSystem.Collections;

namespace MauronAlpha.FileSystem.Units {
	
	
	public class Directory:FileSystem_component, I_file {

		public Directory() : base() { }
		public Directory(string path) : this() {
			STR_path = path;

			try {
				path = System.IO.Path.GetDirectoryName(Path);
				STR_name = Directory.FormNameFromPath(path);
			}
			catch (System.Exception ex) {
				return;
			}
		}
		public Directory(Directory directory, string name) {
			DATA_directory = directory;
			STR_name = FormNameFromPath(name);
		}

		private string STR_path;
		public string Path { 
			get {
				if (DATA_directory == null)
					return STR_path;
				return DATA_directory.Path + Name;
			} 
		}

		private string STR_name;
		public string Name {
			get {
				if (STR_name == null)
					STR_name = Directory.FormNameFromPath(STR_path);
				return STR_name;
			}
		}

		private Directory DATA_directory;
		public Directory Parent {
			get {
				return DATA_directory;
			}
		}

		public DataObjects.FileType FileType {
			get { return FileTypes.Directory; }
		}

		public AccessRights Rights {
			get { return new AccessRights(this); }
		}

		public bool IsReadOnly {
			get { return Rights.CanRead; }
		}

		public DirectoryContent Content {
			get {
				return new DirectoryContent(this);
			}
		}

		public static string FormNameFromPath(string path) {
			char[] findThese = new char[] { System.IO.Path.AltDirectorySeparatorChar, System.IO.Path.DirectorySeparatorChar };
			string name = path.TrimEnd(findThese);
			int index = name.LastIndexOfAny(findThese);
			if (index >= 0)
				name = name.Substring(index);
			name = name.TrimStart(findThese);
			return name;
		}
	}

	public class FileType_directory : FileType {
		public override string Name {
			get { return "Directory"; }
		}

		public override bool IsDirectory {
			get { return true; }
		}

		public static FileType_directory Instance { get { return new FileType_directory(); } }
	}


}
