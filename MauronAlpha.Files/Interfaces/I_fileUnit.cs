using MauronAlpha.HandlingData;
using MauronAlpha.Files.DataObjects;
using MauronAlpha.Files.Units;

namespace MauronAlpha.Files.Interfaces {
	public interface I_fileUnit {
		string Name { get; }

		MauronCode_dataList<FileUnit_directory> PathStructure { get; }
		FileUnitType UnitType { get; }
		FileLocation Location { get; }
	}
}
