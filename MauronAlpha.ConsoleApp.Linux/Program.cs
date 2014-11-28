using System;
using MauronAlpha.ConsoleApp;
using MauronAlpha.Layout.Layout2d.Context;

namespace MauronAlpha.ConsoleApp.Linux
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Layout2d_context context = new Layout2d_context ();
			MauronConsole m = new MauronConsole ("Linux Version", context);
		}
	}
}
