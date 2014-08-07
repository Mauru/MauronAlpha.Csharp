using MauronAlpha.HandlingData;
using System.Collections.Generic;

namespace MauronAlpha.Input.Keyboard {

	//A class that keeps information on KeyboardMappings
	public class KeyboardMap:MauronCode_dataMap<SpecialKey> {
		public KeyboardMap():base(){
		
		}

		//Enumerator
		public IEnumerator<SpecialKey> GetEnumerator ( ) {
			return this.Values.GetEnumerator();
		}

	}
}
