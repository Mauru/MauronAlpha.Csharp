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
