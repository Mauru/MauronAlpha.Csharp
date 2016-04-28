using System;

using MauronAlpha.FileSystem.Units;

namespace MauronAlpha.Testcases.FileSystem {
	class Program {
		static void Main(string[] args) {

			/**/
			string path = System.AppDomain.CurrentDomain.BaseDirectory;
			FileStructure sys = new FileStructure(path);
			FileManager manager = new FileManager(sys);
			Directory dir = manager.CreateDirectory("TestDir");

			File test = manager.CreateFile(dir, "test", "mau");
			test.Append("....Aaaaand another line", true);


			FileReporter f = new FileReporter(sys);
			System.Collections.Generic.IEnumerable<string> files = f.GetFilesInCompilePath();
			foreach (string file in files)
				System.Console.WriteLine(file);


			System.Console.ReadKey();
		}
	}
}

//still need to make sure that directries properly find names