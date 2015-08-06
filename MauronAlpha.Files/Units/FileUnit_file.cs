using System;
using MauronAlpha.HandlingErrors;
using MauronAlpha.ExplainingCode;

using MauronAlpha.Files.Interfaces;
using MauronAlpha.Files.DataObjects;
using MauronAlpha.Files.Structures;

namespace MauronAlpha.Files.Units {

	public class FileUnit_file : FileUnit {
		
		public FileUnit_file ( string name, FileSystem structure) : base(name, structure) {}
		public FileUnit_file(string name, FileLocation location) : base(name, location.FileSystem) {
			SetLocation(location);
		}

		public bool Exists {
			get {
				return Location.FileSystem.Exists(this);
			}
		}

		public string Path { get {
			return Location.AsString + Name;
		} }
		public FileSize Size { get {
			return new FileSize(this);
		} }
		public override FileUnitType UnitType { get { return FileUnitType.File; } }

		public FileUnit_file Append(string text, bool newLine) {
			if (IsReadOnly)
				throw Error("File is protected!,(Append)", this, ErrorType_protected.Instance);
			Location.FileSystem.AppendToFile(this, text, true);
			return this;
		}

	}

	public class FileUnitType_file : FileUnitType {

		public override string Name {
			get { return "file"; }
		}

		public static FileUnitType_file Instance { get {
			return new FileUnitType_file();
		} }
	}

}
