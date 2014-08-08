using MauronAlpha.HandlingData;
using MauronAlpha.Events;

namespace MauronAlpha.Input.Keyboard {

	public class KeyPress:MauronCode_dataObject, I_eventSender, I_eventReceiver {
		
		//constructor
		public KeyPress():base(DataType_object.Instance){}

		//Alt key
		private bool B_isAltKeyDown=false;
		public bool IsAltKeyDown { get {return B_isAltKeyDown; } }
		public KeyPress SetIsAltKeyDown(bool status){
			B_isAltKeyDown=status;
			return this;
		}

		//Shift key
		private bool B_isShiftKeyDown=false;
		public bool IsShiftKeyDown { get { return B_isShiftKeyDown; } }
		public KeyPress SetIsShiftKeyDown(bool status){
			B_isShiftKeyDown=status;
			return this;
		}

		//CTRL key
		private bool B_isCtrlKeyDown=false;
		public bool IsCtrlKeyDown { get { return B_isCtrlKeyDown; } }
		public KeyPress SetIsCtrlKeyDown (bool status) {
			B_isCtrlKeyDown=status;
			return this;
		}

		//Character
		private char CHAR_key;
		public char Key {
			get {
				return CHAR_key;
			}
		}
		public KeyPress SetKey(char key){
			CHAR_key=key;
			return this;
		}

		//Special Keys
		private bool B_IsSpecialKey=false;
		public bool IsSpecialKey { get { return B_IsSpecialKey; } }
		public KeyPress SetIsSpecialKey(bool status) {
			B_IsSpecialKey=status;
			return this;
		}

		//Special Key
		private SpecialKey KEY_specialKey;
		public SpecialKey SpecialKey { get { return KEY_specialKey; } }
		public KeyPress SetSpecialKey(SpecialKey key) { KEY_specialKey=key; return this; }

		//Figure out if a Keypress is a special Key
		public static bool IsSpecialAction(KeyboardMap map, KeyPress input){
			foreach (KeyPress key in map) {

			}
			return false;
		}
	}

}