using MauronAlpha.Files.Interfaces;
using MauronAlpha.Files.DataObjects;
using MauronAlpha.Files.Structures;

using MauronAlpha.HandlingErrors;

using MauronAlpha.HandlingData;

namespace MauronAlpha.Files.Units {

	public abstract class FileUnit:FileComponent,I_fileUnit, I_protectable {
		internal string STR_name;
		public virtual string Name { get { return STR_name; } }

		public FileUnit(string name, FileSystem structure) {
			STR_name = name;
			DATA_location = new FileLocation(structure);
		}
		public MauronCode_dataList<FileUnit_directory> PathStructure { get { return DATA_location.AsList; } }
		internal FileLocation DATA_location;
		public virtual FileLocation Location {
			get {
				if (DATA_location == null)
					throw NullError("Location can not be null!,(Location)", this, typeof(FileLocation));
				return DATA_location;
			}
		}
		public virtual FileUnit SetLocation(FileLocation pathStructure) {
			DATA_location = pathStructure;
			return this;
		}	

		public abstract FileUnitType UnitType { get; }

		private bool B_isReadOnly = false;
		public FileUnit SetIsReadOnly(bool status) {
			B_isReadOnly = status;
			return this;
		}
		public bool IsReadOnly {
			get { return B_isReadOnly; }
		}
	}

}
