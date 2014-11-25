using MauronAlpha.HandlingData;

namespace TestCases.Generic {
	class Program {
		static void Main (string[] args) {

			MauronCode_dataMap<int> map = new MauronCode_dataMap<int>();
			map.AddKey("test");
			map.SetValue("test", 1);

			System.Console.WriteLine(map.Id);

			System.Console.ReadKey();

		}
	}
}
