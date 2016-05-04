using System;
using MauronAlpha.Console;

namespace MauronAlpha.TestCases.Console {
	
	public class Program {
		
		static void Main(string[] args) {

			ConsoleInputManager tool = new ConsoleInputManager();
			tool.Start();
			while (!tool.CanExit) {
				tool.DoNothing();
			}

			/*
			//tests
			TestText test = new TestText();
			test.Start();
			while(test.IsRunning) {
				//nada
			}*/

		}
	}

}
