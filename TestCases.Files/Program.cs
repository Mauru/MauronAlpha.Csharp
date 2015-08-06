using System;

using System.IO;
using System.Reflection;

using MauronAlpha.Files.DataObjects;
using MauronAlpha.Files.Structures;
using MauronAlpha.Files.Units;

namespace TestCases.Files {
	
	class Program {

		static void Main(string[] args) {

			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			FileLocation location = new FileLocation(path, FileSystem.Win);
			FileUnit_file file = location.FileSystem.CreateFile("test.mau", location);
			file.Append("This is a test comment", true);
			if (location.Exists)
				System.Console.Write(location.AsString);
			if (location.CanWrite)
				System.Console.WriteLine("Can write to location!");
			System.Console.ReadKey();

		}

	}
}
