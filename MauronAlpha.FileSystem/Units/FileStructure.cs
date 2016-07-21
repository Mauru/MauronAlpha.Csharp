using MauronAlpha.HandlingErrors;

namespace MauronAlpha.FileSystem.Units {

	public class FileStructure:FileSystem_component {

		private Directory root;
		public Directory Root {
			get {
				return root;
			}
		}

		public FileStructure(string path) : base() {
			root = new Directory(path);
		}

		public bool CreateFile(string name, string extension) {
			if (root == null)
				return false;
			File file = new File(root, name, extension);
			if (System.IO.File.Exists(file.Path))
				return true;
			try {
				using (System.IO.FileStream fs = System.IO.File.Create(file.Path)) {
					return true;
				}
			}
			catch (System.Exception ex) {
				return false;
			}
		}
		public File CreateFileAndReturn(string name, string extension) {
			if (root == null)
				throw new  MauronCode_error("No FileStructure present.", this, ErrorType_fileStructure.Instance);
			else {
				File file = new File(root, name, extension);
				if (System.IO.File.Exists(file.Path))
					return file;
				try {
					using (System.IO.FileStream fs = System.IO.File.Create(file.Path))
						return file;
				}
				catch (System.Exception ex) {
					throw new MauronCode_error("Could not create File.", file, ErrorType_fileStructure.Instance);
				}
			}

		}

		public bool CreateFile(File file) {
			try {
				if (System.IO.File.Exists(file.Path))
					return true;
				using (System.IO.FileStream fs = System.IO.File.Create(file.Path)) {
					return true;
				}
			}
			catch (System.Exception ex) {
				return false;
			}
		}

		public Directory CreateDirectoryAndReturn(string name) {
			if (root == null)
				throw new  FileSystemError("No FileStructure present.", this);

			else {
				Directory result = new Directory(Root,name);
				if (System.IO.File.Exists(result.Path))
					return result;
				try {
					System.IO.Directory.CreateDirectory(result.Path);
					return result;
					
				}
				catch (System.Exception ex) {
					throw new FileSystemError("Could not create Directory.", result);
				}
			}
		}

		public bool CreateDirectory(Directory directory) {

			string path = directory.Path;

			if (System.IO.Directory.Exists(path))
				return true;
			try {
				System.IO.Directory.CreateDirectory(path);
				return true;
			}
			catch (System.Exception ex) {
				return false;
			}
		}


		public string Path {
			get {
				return root.Path;
			}
		}



	}


}
