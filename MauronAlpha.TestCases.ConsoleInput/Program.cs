using System;
using MauronAlpha.Forms.Units;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;
using MauronAlpha.HandlingData;

namespace MauronAlpha.TestCases.Console {
	
	public class Program {
		
		static void Main(string[] args) {
			/*
			FormUnit_textField text = new FormUnit_textField();

			text.SetText("Test and stuff.");
			text.InsertAfterContext(Characters.ParagraphBreak);
			foreach (Line l in text.Lines)
				System.Console.WriteLine(l.AsVisualString);
			System.Console.WriteLine(text.CountAsContext.AsString);
			System.Console.ReadKey();*/

			MauronCode_dataList<int> test = new MauronCode_dataList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

			MauronCode_dataList<int> removed = test.ExtractByRange(0);

			foreach (int n in removed) {
				System.Console.Write(n + " ");
			}
			System.Console.ReadKey();



		}
	}

}
