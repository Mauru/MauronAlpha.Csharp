using MauronAlpha.Files.DataObjects;
using MauronAlpha.Files.Units;
using MauronAlpha.Files.Errors;
using MauronAlpha.Files.Interfaces;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using System;
using System.IO;

namespace MauronAlpha.Files.Structures {

	//A Wrapper for OS-based FileSystems
    public class FileSystem_win:FileSystem {

		public override FileLocation StringToLocation(string path) {
			FileLocation result = new FileLocation(this);
			return Parse_dirStringToLocationObject(path, result);	
		}

		public override FileLocation Parse_dirStringToLocationObject(string path, FileLocation location) {
			string[] locales = path.Split('\\');
			if (locales.Length < 1)
				throw Error("Invalid location!,(Parse_stringToLocationObject)", this, ErrorType_parse.Instance);

			//Directories
			for (int index = 0; index < locales.Length; index++) {
				string candidate = locales[index];
				FileUnit_directory dir = new FileUnit_directory(candidate, location.Instance);
				location.Add(dir);
			}

			return location;
		}
		
		public override string LocationToString(FileLocation location) { 
			string result = "";
			foreach (I_fileUnit thing in location.AsList)
				result += (!thing.UnitType.Equals(FileUnitType.File)) ? thing.Name + "\\" : thing.Name;
			return result;
		}
	
		public override long FileSize(FileUnit_file file) {
			if (!file.Exists)
				return 0;
			return new FileInfo(file.Path).Length;
		}

		//Booleans
		public override bool Exists(I_fileUnit unit) {
			return (!unit.UnitType.Equals(FileUnitType.File)) ? DirectoryExists(unit) : FileExists(unit);
		}

		public bool DirectoryExists(I_fileUnit dir) {
			if (dir.UnitType.Equals(FileUnitType.File))
				return DirectoryExists(dir.Location.Directory); 
			return Directory.Exists(LocationToString(dir.Location));
		}
		public bool FileExists(I_fileUnit file) {
			if (!file.UnitType.Equals(FileUnitType.File))
				return false;
			return File.Exists(LocationToString(file.Location));
		}
		public override bool CanWriteToDirectory(I_fileUnit unit) {
			if (unit.UnitType.Equals(FileUnitType.File))
				return CanWriteToDirectory(unit.Location.Directory);
			try {
				using (FileStream fs = File.Create(
					Path.Combine(
						unit.Location.AsString,
						Path.GetRandomFileName()
					),
					1,
					FileOptions.DeleteOnClose)
				) { }
				return true;
			}
			catch {
					return false;
			}
		}
		public override bool CanWriteToFile(I_fileUnit unit) {
			if (!unit.UnitType.Equals(FileUnitType.File))
				return false;
			return CanWriteToDirectory(unit.Location.Directory);
		}

		public override FileUnit_file CreateFile(string name, FileLocation location) {
			if (!CanWriteToDirectory(location.Directory))
				throw Error("Can not write to directory!,(CreateFile)", this, ErrorType_access.Instance);
			File.Create(location.AsString + "name").Dispose();
			return new FileUnit_file(name, location);
		}
		public override FileUnit_file AppendToFile(FileUnit_file file, string text, bool newLine) {
			using (StreamWriter stream = File.AppendText(file.Path)) {
				if (newLine)
					stream.WriteLine(text);
				else
					stream.Write(text);
			}
			return file;
		}
		public override FileUnit_file ClearFile(FileUnit_file file) {
			if (!file.Exists)
				throw Error("File does not exist!,(ClearFile)", this, ErrorType_file.Instance);
			File.WriteAllText(file.Path, string.Empty);
			return file;
		}
		public override FileUnit_file DeleteFile(FileUnit_file file) {
			if (!file.Exists)
				throw Error("File does not exist!,(DeleteFile)", this, ErrorType_file.Instance);
			File.Delete(file.Path);
			return file;
		}
		
		public static FileSystem_win Instance {
			get {
				return new FileSystem_win();
			}
		}

    }
}
