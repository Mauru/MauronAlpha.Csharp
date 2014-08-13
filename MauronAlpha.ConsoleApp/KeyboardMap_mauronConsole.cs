using MauronAlpha.Input.Keyboard;
using MauronAlpha.HandlingData;
using System;


namespace MauronAlpha.ConsoleApp {


	public class KeyboardMap_mauronConsole:KeyboardMap {			

		//Constructor
		public KeyboardMap_mauronConsole() {
			//Set defaults
			SpecialKeys.SetValue("Enter", new SpecialKey("Enter",false,false,false,true,false,false));
			SpecialKeys.SetValue("ArrowLeft", new SpecialKey("LeftArrow", false, false, false, false, false, true));
			SpecialKeys.SetValue("ArrowRight", new SpecialKey("RightArrow", false, false, false, false, false, true));
		}
	}

}
