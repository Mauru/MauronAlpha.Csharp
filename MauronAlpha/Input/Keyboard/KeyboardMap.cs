using MauronAlpha.HandlingData;
using System.Collections.Generic;

namespace MauronAlpha.Input.Keyboard {

	//A class that keeps information on KeyboardMappings
	public class KeyboardMap:MauronCode_dataObject {

		//The special keys
		private SpecialKeys DATA_specialKeys;
		public SpecialKeys SpecialKeys {
			get {
				if (DATA_specialKeys == null) {
					SetSpecialKeys (new SpecialKeys ());
				}
				return DATA_specialKeys;
			}
		}
		public KeyboardMap SetSpecialKeys(SpecialKeys keys){
			DATA_specialKeys = keys;
			return this;
		}

		//constructor
		public KeyboardMap():base(DataType_dispenser.Instance){
		
		}
	}
}
