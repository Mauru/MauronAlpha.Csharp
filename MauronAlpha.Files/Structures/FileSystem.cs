using System;

using MauronAlpha.Files.DataObjects;
using MauronAlpha.Files.Interfaces;
using MauronAlpha.Files.Units;

using MauronAlpha.HandlingData;

namespace MauronAlpha.Files.Structures {
	
	public abstract class FileSystem:FileComponent {

		public abstract string LocationToString(FileLocation unit);
		public abstract FileLocation StringToLocation(string path);
		public abstract FileLocation Parse_dirStringToLocationObject(string path, FileLocation location);

		public abstract bool Exists(I_fileUnit unit);
		public abstract bool CanWriteToDirectory(I_fileUnit unit);
		public abstract bool CanWriteToFile(I_fileUnit unit);

		public abstract long FileSize(FileUnit_file file);

		public static FileSystem Win {
			get {
				return FileSystem_win.Instance;
			}
		}

		public abstract FileUnit_file CreateFile(string name, FileLocation location);
		public abstract FileUnit_file AppendToFile(FileUnit_file file, string text, bool NewLine);
		public abstract FileUnit_file ClearFile(FileUnit_file file);
		public abstract FileUnit_file DeleteFile(FileUnit_file file);
	}
}
