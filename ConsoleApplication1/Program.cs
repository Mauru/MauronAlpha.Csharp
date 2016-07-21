using MauronAlpha.FileSystem.Units;
using MauronAlpha.FontParser;
using MauronAlpha.FontParser.DataObjects;

using MauronAlpha.HandlingData;

namespace ConsoleApplication1 {
	class Program {
		static void Main(string[] args) {

			FileStructure sys = new FileStructure(System.AppDomain.CurrentDomain.BaseDirectory);

			System.Console.WriteLine(sys.Path);

			File parseThis = new File(sys.Root, "robotoLight.unaliased.16", "fnt");

			System.Console.WriteLine(parseThis.Path);

			FontDefinition f = new FontDefinition(parseThis);
			f.Parse();

			System.Console.WriteLine("Final report for font {" + f.FontName + "}");
			foreach(File ff in f.Files)
				System.Console.WriteLine(ff.FileName);

			System.Console.WriteLine("Testing charstream...");

			string test = "Test";

			MauronCode_dataList<PositionData> result = f.PositionData(test);

			System.Console.WriteLine("Result  length is " + result.Count);

			System.Console.ReadKey();
		}
	}
}
