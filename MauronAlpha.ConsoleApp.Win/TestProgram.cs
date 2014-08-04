using System;

using MauronAlpha.ConsoleApp;
using MauronAlpha.Events;

namespace MauronAlpha.ConsoleApp.Win {
	class TestProgram {
		static void Main (string[] args) {
			
			//Set up the timer
			MauronCode_eventClock MasterClock = new MauronCode_eventClock();

			//Set up the console
			MauronConsole M = new MauronConsole("MauronConsole (Windows)");
			M.SetEventClock(MasterClock);

		}
	}
}
