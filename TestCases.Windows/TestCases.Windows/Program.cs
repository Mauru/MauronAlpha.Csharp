using MauronAlpha.HandlingData;

using MauronAlpha.Forms.Units;
using MauronAlpha.Text.Units;

public class Test {
	
	public static void Main ( ) {
		System.Console.WriteLine("-Program start-");


		MauronCode_dataIndex<string> list=new MauronCode_dataIndex<string>();

		 list.SetValue(1, "cake");
		 list.SetValue(5, "fire");
		 list.SetValue(8, "git");


		 foreach(string f in list) {
			System.Console.WriteLine(f);
		 }

		/*FormUnit_textField field = new FormUnit_textField();
		TextUnit_text text = new TextUnit_text();
		text.SetText("Hi, I am a text");
		System.Console.WriteLine(text.CountAsContext.AsString);
		System.Console.WriteLine(text.AsString);*/
		System.Console.WriteLine("-Program end-");
		System.Console.ReadKey();
	}

	



}
