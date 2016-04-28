using System;
using MauronAlpha.Forms.Units;
using MauronAlpha.TextProcessing.Units;
using MauronAlpha.TextProcessing.Collections;

namespace MauronAlpha.TestCases.Console {
	public class TestText {

		FormUnit_textField text = new FormUnit_textField();
		public bool IsRunning = true;

		public void Start() {

			//text.SetText("Hi, I am a text.");
			/*Test 1
			Characters chars = new Characters(" as a case of something.");
			if (chars.FirstElement.IsWhiteSpace)
				System.Console.WriteLine("Is Utility");
			Words words = new Words(chars);
			Lines lines = new Lines(words);*/

			/*//test 2
			Line line = new Line();
			Words w = new Words() { "this", " ", "is" };
			Lines lines = new Lines(w);*/

			//test 3
			Lines lines = new Lines();
			lines.Add(new Line("Line 1"));
			lines.Add(new Line("Line 2"));
			lines.Add(new Line("Line 3"));
			lines.EndAll();
			int offset = 0;
			foreach (Line l in lines) {
				System.Console.SetCursorPosition(0, offset);
				System.Console.Write(l.AsVisualString);
				offset++;
			}
		}
	}
}
