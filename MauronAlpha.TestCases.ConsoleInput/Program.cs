using System;
using MauronAlpha.Forms.Units;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.HandlingData;
using MauronAlpha.Console;
using MauronAlpha.FileSystem.Units;

namespace MauronAlpha.TestCases.Console {
	
	public class Program {
		
		static void Main(string[] args) {
			//FileStructure WriteHere = new FileStructure(@"C:\Users\mauru\Documents\MauronAlpha.Inbox");
			//File file = WriteHere.CreateFileAndReturn("mauruTest","mau");
			/*
			System.Console.WriteLine(WriteHere.Path);
			 * */
			System.Console.WriteLine("Starting ConsoleApp...");
			System.Console.ReadKey();


			ConsoleApp  helloWorld = new ConsoleApp("Ok, es geht auch mehrmals.");
			helloWorld.Start();

			System.Console.WriteLine("Application {"+ helloWorld.Name+"} terminated successfully.");
			System.Console.WriteLine("Press any key to Exit.");
			System.Console.ReadKey();



		}
	}

}
