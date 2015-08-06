using System;
using MauronAlpha.Interfaces;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Files.Interfaces;
using MauronAlpha.Files.Structures;
using MauronAlpha.Files.Units;

using MauronAlpha.HandlingData;

namespace MauronAlpha.Files.DataObjects {
	
	public class FileLocation:FileComponent, I_protectable, I_instantiable<FileLocation> {

		//constructors
		public FileLocation(FileSystem wrapper) : base() {
			SYSTEM_type = wrapper;
		}
		public FileLocation(FileUnit_directory unit)
			: this(unit.Location.FileSystem) {
			DATA_location.Add(unit);
		}
		public FileLocation(string path, FileSystem structure) : this(structure) {
			structure.Parse_dirStringToLocationObject(path, this);
		}
		public FileLocation(FileLocation location) : this(location.FileSystem) {

			foreach (FileUnit_directory unit in location.AsList)
				Add(unit);

		}

		private MauronCode_dataList<FileUnit_directory> DATA_location = new MauronCode_dataList<FileUnit_directory>();
		public MauronCode_dataList<FileUnit_directory> AsList {
			get {
				return DATA_location.SetIsReadOnly(true);
			}
		}

		//Booleans
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		public bool HasFileSystem { 
			get {
				return (SYSTEM_type != null);
			}
		}
		public bool Exists {
			get {
				return FileSystem.Exists(DATA_location.LastElement);
			}
		}
		public bool CanWrite { get {
			if (DATA_location.IsEmpty)
				return false;
			return FileSystem.CanWriteToDirectory(Directory);
		} }

		public FileUnit_directory Directory {
			get {
				if (DATA_location.IsEmpty)
					throw Error("Location is undefined!", this, ErrorType_index.Instance);
				if (DATA_location.LastElement.UnitType.Equals(FileUnitType.File))
					return (FileUnit_directory) DATA_location.Value(DATA_location.Count - 2);
				return (FileUnit_directory) DATA_location.LastElement;
			}
		}

		//Methods
		public FileLocation Clear() {
			if (IsReadOnly)
				throw Error("Is protected!,(Clear)", this, ErrorType_protected.Instance);
			DATA_location.Clear();
			return this;
		}
		public FileLocation Add(FileUnit_directory unit) {
			if (IsReadOnly)
				throw Error("Is protected!,(Add)", this, ErrorType_protected.Instance);
			DATA_location.SetIsReadOnly(false).Add(unit);
			return this;
		}

		private FileSystem SYSTEM_type;
		public FileSystem FileSystem {
			get {
				if (!HasFileSystem)
					throw NullError("FileSystem can not be null!,(FileStructure)", this, typeof(FileSystem));
				return SYSTEM_type;
			}
		}

		public string AsString { 
			get {
				string result = "";
				if (DATA_location.IsEmpty)
					return result;
				if (!HasFileSystem)
					return "UNKOWN FILESYSTEM! " + FileSystem_win.Instance.LocationToString(this);
				return FileSystem.LocationToString(this);

			}
		}

		public FileLocation Instance { 
			get {
				return new FileLocation(this);
			}
		}
	}

}
