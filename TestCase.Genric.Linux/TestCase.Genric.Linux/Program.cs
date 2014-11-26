using System;
using MauronAlpha.HandlingData;

namespace TestCase.Genric.Linux
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MauronCode_dataMap<int> map = new MauronCode_dataMap<int> ();
			System.Console.WriteLine ("123");
			map.SetValue ("START", 2);
			System.Console.WriteLine ("TESTEND:"+map.Value("START"));

		}
	}
}
