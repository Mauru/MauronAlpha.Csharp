using MauronAlpha.Input.Keyboard;
using MauronAlpha.HandlingData;
using System;

namespace MauronAlpha.ConsoleApp {


	public class KeyboardMap_mauronConsole:KeyboardMap {
		public SpecialKeys SpecialKeys=new SpecialKeys();

		public KeyboardMap_mauronConsole() {
			SpecialKeys.Add("Enter", new SpecialKey("Enter",false,false,false,true,true,false))
		}
	}

}
