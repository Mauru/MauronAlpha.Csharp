using MauronAlpha.HandlingData;
using MauronAlpha.Text;
using MauronAlpha.Input.Keyboard.Units;
using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha;
using MauronAlpha.ExplainingCode;

namespace TestCases.Generic {
	class Program {
		static void Main (string[] args) {

			KeyPress a2 = new KeyPress("Backspace");
			KeyPressSequence a3 = new KeyPressSequence(a2);

			while (1 ==1) {

				System.ConsoleKeyInfo key = System.Console.ReadKey(true);
				KeyPress a1 = new KeyPress(key.Key.ToString());

				if (a3.Equals(a1))
					System.Console.Write("YES!" + a1.KeyName);
				else
					System.Console.WriteLine(a1.KeyName);

			}

		}
	}
}
