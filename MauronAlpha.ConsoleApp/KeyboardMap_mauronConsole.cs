using MauronAlpha.Input.Keyboard;
using MauronAlpha.HandlingData;
using System;


namespace MauronAlpha.ConsoleApp {


	public class KeyboardMap_mauronConsole:KeyboardMap {			

		//Constructor
		public KeyboardMap_mauronConsole() {
			
			Add("Enter", new SpecialKey("Enter",false,false,false,true,false,false));
			Add("ArrowLeft",new SpecialKey("ArrowLeft", false,false,false,false,false,true));
			Add("ArrowRight",new SpecialKey("ArrowRight",false,false,false,false,false,true));

		}
	}

}
