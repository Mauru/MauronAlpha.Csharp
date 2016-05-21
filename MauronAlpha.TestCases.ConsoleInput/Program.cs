using System;
using MauronAlpha.Forms.Units;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.HandlingData;

namespace MauronAlpha.TestCases.Console {
	
	public class Program {
		
		static void Main(string[] args) {

			FormUnit_textField text = new FormUnit_textField();

			text.SetText("Test and stuff.");
			text.InsertAfterWord(0, Characters.ParagraphBreak);
			foreach (Line l in text.Lines)
				System.Console.WriteLine(l.AsVisualString);
			System.Console.WriteLine(text.CountAsContext.AsString + " total.");
			System.Console.WriteLine(text.FirstLine.CountAsContext.AsString + " on first line.");

			System.Console.ReadKey();



		}
	}

}
