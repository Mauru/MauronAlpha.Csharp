using System;
using MauronAlpha.HandlingData;

namespace TestCases.Windows {
	class Program {
		static void Main (string[] args) {
			string[] KEYS_regions=new string[4] {"header","content","input","footer"};
			MauronCode_dataTree<string,object> tests = new MauronCode_dataTree<string,object>(KEYS_regions);

			System.Console.ReadKey();
			
		}
	}
}
