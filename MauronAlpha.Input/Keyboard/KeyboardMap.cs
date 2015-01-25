using System.Collections.Generic;
using MauronAlpha.HandlingData;

using MauronAlpha.Input.Keyboard.Collections;
using MauronAlpha.Input.Keyboard.Units;

namespace MauronAlpha.Input.Keyboard {

	//A class that keeps information on KeyboardMappings
	public class KeyMap:MauronCode_dataObject {

		//constructor
		public KeyMap ( )
			: base(DataType_dispenser.Instance) {

		}

		//The special keys
		private SpecialKeyMap DATA_specialKeys;
		public SpecialKeyMap SpecialKeys {
			get {
				if (DATA_specialKeys == null) {
					SetSpecialKeys (new SpecialKeyMap ());
				}
				return DATA_specialKeys;
			}
		}
		public KeyMap SetSpecialKeys (SpecialKeyMap keys) {
			DATA_specialKeys = keys;
			return this;
		}

		//The KeySequences
		private KeyPressSequenceMap DATA_KeyPressSequences;
		public KeyPressSequenceMap KeyPressSequences{
			get {
				if(DATA_KeyPressSequences==null) {
					SetKeyPressSequences(new KeyPressSequenceMap());
				}
				return DATA_KeyPressSequences;
			}
		}
		public KeyMap SetKeyPressSequences (KeyPressSequenceMap map) {
			DATA_KeyPressSequences=map;
			return this;
		}

		//Enumerators
		public IEnumerator<SpecialKey> GetEnumerator(){
			return SpecialKeys.Values.GetEnumerator();
		}
		public IEnumerator<KeyPressSequence> GetEnumerator<KeyPressSequenceMap>() {
			return KeyPressSequences.Values.GetEnumerator();
		}

	}
}
