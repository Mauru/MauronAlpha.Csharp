using MauronAlpha.FileSystem.Collections;
using MauronAlpha.FileSystem.DataObjects;

namespace MauronAlpha.FileSystem.Interfaces {
	
	public interface I_file {

		FileType FileType { get; }

		AccessRights Rights { get; }

		bool IsReadOnly { get; }

	}

}
