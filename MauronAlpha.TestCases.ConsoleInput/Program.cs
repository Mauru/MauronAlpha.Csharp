using System;
using MauronAlpha.Forms.Units;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;

namespace MauronAlpha.TestCases.Console {
	
	public class Program {
		
		static void Main(string[] args) {


			System.Console.WriteLine(Characters.ParagraphBreak.AsVisualString);
			System.Console.ReadKey();

			FormUnit_textField text = new FormUnit_textField();

			text.SetText("Test and stuff.");
			text.Add(Characters.ParagraphBreak);
			foreach (Line l in text.Lines)
				System.Console.WriteLine(l.AsVisualString);
			System.Console.WriteLine(text.CountAsContext.AsString);
			System.Console.ReadKey();

		}
	}

}
