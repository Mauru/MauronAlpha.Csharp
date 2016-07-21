using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingErrors;

namespace MauronAlpha.FileSystem
{
    public class FileSystem_component:MauronCode_component {
    }


	public class FileSystemError :MauronCode_error {

		public FileSystemError(string message, FileSystem_component source) : base(message, source, ErrorType_fileStructure.Instance) { }
	}

	public class ErrorType_fileStructure : ErrorType {
		public static ErrorType_fileStructure Instance {
			get {
				return new ErrorType_fileStructure();
			}
		}

		public override string Name {
			get { return "FileStructure"; }
		}
	}
}
