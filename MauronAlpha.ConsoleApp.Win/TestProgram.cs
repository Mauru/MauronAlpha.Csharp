using System;

using MauronAlpha.ConsoleApp;

using MauronAlpha.Events;
using MauronAlpha.Events.Singletons;

using MauronAlpha.HandlingData;



namespace MauronAlpha.ConsoleApp.Win {


	class TestProgram {

		static void Main (string[] args) {
				
				SharedEventSystem synchronizer = SharedEventSystem.Instance;

				

				//Set up the console
				MauronConsole M = new MauronConsole("MauronConsole (Windows)");

				//Keep Console open
				System.Console.ReadKey();
		
		}
	
	
	}
}
