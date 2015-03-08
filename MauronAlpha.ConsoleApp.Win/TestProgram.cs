using System;

using MauronAlpha.ConsoleApp;
using MauronAlpha.Projects;

using MauronAlpha.Events;

using MauronAlpha.HandlingData;

using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Geometry.Geometry2d.Units;

namespace MauronAlpha.ConsoleApp.Win {

	class TestProgram {

		static void Main (string[] args) {
				
				//1: We start by grabbing the size and position of the Context Window
				Vector2d size = new Vector2d(
					System.Console.WindowWidth,
					System.Console.WindowHeight
				);
				Vector2d position = new Vector2d(
					System.Console.WindowLeft,
					System.Console.WindowTop
				);

				//2: We create a layout context
				Layout2d_context context = new Layout2d_context( position, size );

				//3: We initiate the mauronConsole
				MauronConsole console = new MauronConsole( "MauronConsole (Windows)", context );

				//4: Start the FrameWork, create an activity-loop
				ProjectComponent_statusCode statusCode = new ProjectComponent_statusCode( console );
				while ( !statusCode.CanExit ) {
					statusCode = console.Idle();
				}
				
		}
	
	}

}
