using System;
using MauronAlpha.Forms.Units;
using MauronAlpha.TextProcessing.Units;

namespace MauronAlpha.TestCases.Console {
	
	public class Program {
		
		static void Main(string[] args) {

			FormUnit_textField text = new FormUnit_textField();

			text.SetText("Test and stuff");
			text.AddAsParagraph("Some more text.");
			text.InsertAtWord(3, "INTERRUPT");
			foreach (Line l in text.Lines)
				System.Console.WriteLine(l.AsString);
			System.Console.WriteLine(text.CountAsContext.AsString);
			System.Console.ReadKey();

		}
	}

}
