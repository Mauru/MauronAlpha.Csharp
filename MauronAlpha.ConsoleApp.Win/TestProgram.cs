using System;

using MauronAlpha.ConsoleApp;

using MauronAlpha.Events;
using MauronAlpha.Events.Singletons;

using MauronAlpha.HandlingData;

using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Geometry.Geometry2d.Units;



namespace MauronAlpha.ConsoleApp.Win {


	class TestProgram {

		static void Main (string[] args) {
				
				SharedEventSystem synchronizer = SharedEventSystem.Instance;

				Vector2d size = new Vector2d(System.Console.LargestWindowWidth,System.Console.LargestWindowHeight);
				Vector2d position = new Vector2d(System.Console.WindowLeft,System.Console.WindowTop);

				Layout2d_context windowContext=new Layout2d_context(position, size, true);

				//Set up the console
				MauronConsole M = new MauronConsole("MauronConsole (Windows)",windowContext);

				//Keep Console open
				System.Console.ReadKey();
		
		}
	
	
	}
}
