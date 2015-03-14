using System;
using MauronAlpha.ConsoleApp;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.ConsoleApp.Linux
{
	class MainClass
	{
		public static void Main (string[] args)	{

			Layout2d_context context = new Layout2d_context (
				System.Console.WindowLeft,
				System.Console.WindowTop,
				System.Console.WindowWidth,
				System.Console.WindowHeight
			);
			MauronConsole m = new MauronConsole ("Linux Version", context);
			while (!m.CanExit) {
				m.Idle ();
			}
		}
	}
}
