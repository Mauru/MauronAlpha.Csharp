using MauronAlpha.HandlingData;

using MauronAlpha.Forms.Units;
using MauronAlpha.Text.Units;

public class Test {
	
	public static void Main ( ) {
		System.Console.WriteLine("-Program start-");

		FormUnit_textField field = new FormUnit_textField();

		TextUnit_text text = new TextUnit_text();
		text.SetText("Hi, I am a text");

		System.Console.WriteLine("-Program end-");
		System.Console.ReadKey();
	}

	



}
