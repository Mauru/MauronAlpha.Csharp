using System;
using MauronAlpha.HandlingData;
using MauronAlpha.Events;

namespace MauronAlpha.ConsoleApp.Linux
{
	class MainClass {
		//Testing MauronCode_datalist
		public static void Main (string[] args)
		{
			//Create environment
			MauronConsole m = new MauronConsole("Testing MauronCode_eventClock");
			MauronCode_eventClock MasterClock = new MauronCode_eventClock ();
			MauronCode_eventShedule UpdateTimer = new MauronCode_eventShedule (MasterClock);
	
		}
	}
}
