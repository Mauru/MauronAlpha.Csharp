using MauronAlpha.HandlingData;

using MauronAlpha.Forms.Units;
using MauronAlpha.Text.Units;

public class Test {
	
	public static void Main ( ) {
		System.Console.WriteLine("-Program start-");


		FormUnit_textField field = new FormUnit_textField();
		field.SetText("Hi, I am a textField");
		
		System.Console.WriteLine(field.LineByIndex(0).AsString);

		System.Console.WriteLine("-Program end-");
		System.Console.ReadKey();
	}

	



}
