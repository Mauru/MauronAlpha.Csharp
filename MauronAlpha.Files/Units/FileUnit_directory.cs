using System;
using MauronAlpha.Files.DataObjects;
using MauronAlpha.Files.Interfaces;
using MauronAlpha.Files.Structures;

namespace MauronAlpha.Files.Units {

	public class FileUnit_directory:FileUnit {
		//constructor
		public FileUnit_directory(string name, FileSystem structure) : base(name, structure) {}
		public FileUnit_directory(string name, FileLocation location) : base(name, location.FileSystem) {
			SetLocation(location);	
		}
		public override FileUnitType UnitType {
			get { return FileUnitType.Directory; }
		}

		public bool Exists {
			get {
				return Location.FileSystem.Exists(this);
			}
		}
	}

	public class FileUnitType_directory : FileUnitType {
		public override string Name {
			get { return "directory";  }
		}
		public static FileUnitType_directory Instance {
			get {
				return new FileUnitType_directory();
			}
		}
	}

}
