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
			text.InsertAfterWord(0, Characters.LineBreak);
			foreach (Line l in text.Lines)
				System.Console.WriteLine(l.AsVisualString);
			System.Console.WriteLine(text.CountAsContext.AsString);
			System.Console.ReadKey();



		}
	}

}
