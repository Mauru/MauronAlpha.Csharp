using MauronAlpha.Files.Units;
using MauronAlpha.Files.DataObjects;
using MauronAlpha.Files.Structures;

namespace MauronAlpha.Files.Logging {
	
	public class StaticLogFile {

		public static void DebugToFile(string msg) {
			FileLocation location = new FileLocation(".",FileSystem.Win);
			FileUnit_file file = new FileUnit_file("LogFile.log",location);
		}

		public static StaticLogFile Instance {
			get { return new StaticLogFile(); }
		}
	}
}
